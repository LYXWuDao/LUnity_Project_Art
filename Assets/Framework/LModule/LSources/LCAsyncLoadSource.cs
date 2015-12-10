using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LGame.LBehaviour;
using LGame.LCommon;
using LGame.LDebug;
using UnityEngine;

namespace LGame.LSource
{

    /****
     * 
     * 
     * 异步加载资源  
     * 
     * 使用文件流，二进制方式进行加载
     * 
     * 例如： 模型，界面，图片，声音，特效 等.
     * 
     * 使用单例模式
     * 
     */

    public sealed class LCAsyncLoadSource
    {

        /// <summary>
        /// 构造函数私有化
        /// 
        /// 单例模式需要
        /// </summary>
        private LCAsyncLoadSource()
        {

        }

        private static LCAsyncLoadSource _instance;

        /// <summary>
        /// 用于 lock 
        /// </summary>
        private static object _lock = new object();

        /// <summary>
        /// 开始异步加载
        /// </summary>
        /// <param name="bundleRequest">异步AssetBundle </param>
        /// <param name="entity">加载资源后实体</param>
        /// <param name="callback">资源加载完成回调</param>
        /// <returns></returns>
        private IEnumerator StartLoad(AssetBundleCreateRequest bundleRequest, LoadSourceEntity entity,
            Action<LoadSourceEntity> callback)
        {
            if (entity == null) yield return 0;
            if (bundleRequest == null)
            {
                LCSConsole.WriteError("异步加载 AssetBundleCreateRequest 不存在!, bundleRequest = null");
                yield return 0;
            }
            yield return bundleRequest;
            AssetBundle assetBundle = bundleRequest.assetBundle;
            if (assetBundle == null)
            {
                LCSConsole.WriteError("创建资源 AssetBundle 失败!");
                yield return 0;
            }
            UnityEngine.Object retobj = assetBundle.Load(entity.ResName);
            if (retobj == null)
            {
                LCSConsole.WriteError("资源 AssetBundle 中不存在 resName = " + entity.ResName);
                yield return 0;
            }
            if (callback == null) yield return 0;
            entity.LoadObj = retobj;
            entity.Bundle = assetBundle;
            callback(entity);
        }

        /// <summary>
        /// 直接从 Resources 文件夹下加载资源
        /// </summary>
        /// <param name="request"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        private IEnumerator StartLoad(ResourceRequest request, LoadSourceEntity entity, Action<LoadSourceEntity> callback)
        {
            if (entity == null) yield return 0;
            if (request == null)
            {
                LCSConsole.WriteError("异步加载 ResourceRequest 不存在!, request = null");
                yield return 0;
            }
            yield return request;
            if (callback == null) yield return 0;
            entity.LoadObj = request.asset;
            callback(entity);
        }

        /// <summary>
        /// 使用二进制方式加载资源
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="loadPath">真实的加载路径</param>
        /// <param name="callback"></param>
        private void LoadBinarySources(LoadSourceEntity entity, string loadPath, Action<LoadSourceEntity> callback)
        {
            if (entity == null)
            {
                LCSConsole.WriteError("资源加载实体数据为空，不能加载！");
                return;
            }

            if (string.IsNullOrEmpty(entity.ResName))
            {
                LCSConsole.WriteError("导入资源名字为空, resName = string.Empty");
                return;
            }

            if (string.IsNullOrEmpty(entity.BundlePath))
            {
                LCSConsole.WriteError("导入 AssetBundle 路径为空, bundPath = string.Empty");
                return;
            }

            if (string.IsNullOrEmpty(loadPath))
            {
                LCSConsole.WriteError("导入 AssetBundle 真实路径为空, loadPath = string.Empty");
                return;
            }

            if (!File.Exists(loadPath))
            {
                LCSConsole.WriteError("导入 AssetBundle 路径不存在, loadPath = " + loadPath);
                return;
            }

            byte[] bytes = File.ReadAllBytes(loadPath);
            AssetBundleCreateRequest request = AssetBundle.CreateFromMemory(bytes);
            // 开始异步加载
            LCCoroutine.Instance.StartCoroutine(StartLoad(request, entity, callback));
        }

        /// <summary>
        /// 加载打包资源文件夹 
        /// 
        /// SourceAssets 下的文件
        /// 
        /// entity.BundlePath  相对于 SourceAssets 文件夹路径
        /// </summary>
        /// <param name="entity">资源管理实体</param>
        /// <param name="callback">资源管理类的回调函数</param>
        private void LoadBuildSources(LoadSourceEntity entity, Action<LoadSourceEntity> callback)
        {
            if (entity == null) return;
            if (string.IsNullOrEmpty(entity.BundlePath)) entity.BundlePath = entity.ResName;
            string loadPath = LCSPathHelper.UnityBuildRootPath() + entity.BundlePath;
            LoadBinarySources(entity, loadPath, callback);
        }

        /// <summary>
        /// 加载 streamingAssets 文件夹下的文件
        /// 
        /// entity.BundlePath 相对于 streamingAssets 文件夹的路径
        /// </summary>
        /// <param name="entity">资源管理实体</param>
        /// <param name="callback">资源管理类的回调函数</param>
        private void LoadStreamingSources(LoadSourceEntity entity, Action<LoadSourceEntity> callback)
        {
            if (entity == null) return;
            if (string.IsNullOrEmpty(entity.BundlePath)) entity.BundlePath = entity.ResName;
            string loadPath = LCSPathHelper.UnityStreamingAssets() + entity.BundlePath;
            LoadBinarySources(entity, loadPath, callback);
        }

        /// <summary>
        /// 
        /// 直接在 Resources 文件夹下加载文件
        /// 
        /// </summary>
        /// <param name="entity">
        /// 
        /// 资源管理实体
        /// 
        /// LoadSourceEntity 资源实体的 BundlePath 为相对于 Resources 路径
        /// 
        /// </param>
        /// <param name="callback">资源管理类的回调函数</param>
        private void LoadResources(LoadSourceEntity entity, Action<LoadSourceEntity> callback)
        {
            if (entity == null)
            {
                LCSConsole.WriteError("资源加载实体数据为空，不能加载！");
                return;
            }

            if (string.IsNullOrEmpty(entity.ResName))
            {
                LCSConsole.WriteError("加载资源的名字为空, resName = string.Empty");
                return;
            }

            if (string.IsNullOrEmpty(entity.BundlePath))
            {
                LCSConsole.WriteError("加载资源的路径, BundlePath = string.Empty");
                return;
            }

            ResourceRequest request = Resources.LoadAsync(entity.BundlePath);
            LCCoroutine.Instance.StartCoroutine(StartLoad(request, entity, callback));
        }

        /// <summary>
        /// 单例模式实例话
        /// </summary>
        public static LCAsyncLoadSource Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new LCAsyncLoadSource();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 导入资源
        /// </summary>
        /// <param name="entity">加载资源后实体</param>
        /// <param name="callback">加载完成后回调</param>
        public void LoadSource(LoadSourceEntity entity, Action<LoadSourceEntity> callback)
        {
            // LoadBuildSources(entity, callback);
            LoadResources(entity, callback);
        }

    }

}