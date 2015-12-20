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

    public sealed class CLAsyncLoadSource
    {

        /// <summary>
        /// 构造函数私有化
        /// 
        /// 单例模式需要
        /// </summary>
        private CLAsyncLoadSource()
        {

        }

        private static CLAsyncLoadSource _instance;

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
                SLDebugHelper.WriteError("异步加载 AssetBundleCreateRequest 不存在!, bundleRequest = null");
                yield return 0;
            }
            yield return bundleRequest;
            AssetBundle assetBundle = bundleRequest.assetBundle;
            if (assetBundle == null)
            {
                SLDebugHelper.WriteError("创建资源 AssetBundle 失败!");
                yield return 0;
            }
            UnityEngine.Object retobj = assetBundle.Load(entity.ResName);
            if (retobj == null)
            {
                SLDebugHelper.WriteError("资源 AssetBundle 中不存在 resName = " + entity.ResName);
                yield return 0;
            }
            if (callback == null) yield return 0;
            entity.LoadObj = retobj;
            entity.Bundle = assetBundle;
            callback(entity);
        }

        /// <summary>
        /// 使用二进制方式加载资源
        /// </summary>
        /// <param name="entity">异步加载资源实体类</param>
        /// <param name="loadPath">真实的加载路径</param>
        /// <param name="callback">加载换成回调</param>
        private void LoadBinarySources(LoadSourceEntity entity, string loadPath, Action<LoadSourceEntity> callback)
        {
            if (entity == null)
            {
                SLDebugHelper.WriteError("资源加载实体数据为空，不能加载！");
                return;
            }

            if (string.IsNullOrEmpty(entity.ResName))
            {
                SLDebugHelper.WriteError("导入资源名字为空, resName = string.Empty");
                return;
            }

            if (string.IsNullOrEmpty(entity.BundlePath))
            {
                SLDebugHelper.WriteError("导入 AssetBundle 路径为空, bundPath = string.Empty");
                return;
            }

            if (string.IsNullOrEmpty(loadPath))
            {
                SLDebugHelper.WriteError("导入 AssetBundle 真实路径为空, loadPath = string.Empty");
                return;
            }

            byte[] bytes = SLManageSource.GetLoadFileBytes(loadPath);
            AssetBundleCreateRequest request = AssetBundle.CreateFromMemory(bytes);
            // 开始异步加载
            CLCoroutine.Instance.StartCoroutine(StartLoad(request, entity, callback));
        }

        /// <summary>
        /// 单例模式实例话
        /// </summary>
        public static CLAsyncLoadSource Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CLAsyncLoadSource();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 加载 assetbundle 资源文件
        /// </summary>
        /// <param name="entity">加载资源后实体</param>
        /// <param name="callback">加载完成后回调</param>
        public void LoadAssetSource(LoadSourceEntity entity, Action<LoadSourceEntity> callback)
        {
            if (entity == null || callback == null) return;
            if (string.IsNullOrEmpty(entity.BundlePath)) entity.BundlePath = entity.ResName;
            string filePath = SLPathHelper.UnityLoadSourcePath() + entity.BundlePath;
            LoadBinarySources(entity, filePath, callback);
        }

    }

}