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

    public sealed class CLAsyncLoadSource : ALBehaviour
    {

        /// <summary>
        /// 构造函数私有化
        /// 
        /// 单例模式需要
        /// </summary>
        private CLAsyncLoadSource()
        {

        }

        /// <summary>
        /// 当前异步加载的数据
        /// </summary>
        private AssetBundleCreateRequest AsyncAsset = null;

        /// <summary>
        /// 当前加载的实体
        /// </summary>
        private LoadSourceEntity CurrentEntity = null;

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
            if (bundleRequest == null)
            {
                SLDebugHelper.WriteError("异步加载 AssetBundleCreateRequest 不存在!, bundleRequest = null");
                Destroy();
                yield return 0;
            }
            yield return bundleRequest;
            AssetBundle assetBundle = bundleRequest.assetBundle;
            if (assetBundle == null)
            {
                SLDebugHelper.WriteError("创建资源 AssetBundle 失败!");
                Destroy();
                yield return 0;
            }
            UnityEngine.Object retobj = assetBundle.Load(entity.ResName);
            if (retobj == null)
            {
                SLDebugHelper.WriteError("资源 AssetBundle 中不存在 resName = " + entity.ResName);
                Destroy();
                yield return 0;
            }
            entity.LoadObj = retobj;
            entity.Bundle = assetBundle;
            entity.Progress = 1;
            entity.IsDone = true;
            if (callback != null) callback(entity);
            Destroy();
        }

        /// <summary>
        /// 支持多个加载
        /// </summary>
        public static CLAsyncLoadSource Instance
        {
            get
            {
                GameObject create = SLCompHelper.Create("_async load");
                return SLCompHelper.FindComponet<CLAsyncLoadSource>(create); ;
            }
        }

        /// <summary>
        /// 加载 assetbundle 资源文件
        /// </summary>
        /// <param name="entity">加载资源后实体</param>
        /// <param name="callback">加载完成后回调</param>
        /// <returns></returns>
        public LoadSourceEntity AsyncLoadAssetSource(LoadSourceEntity entity, Action<LoadSourceEntity> callback)
        {
            if (entity == null)
            {
                SLDebugHelper.WriteError("资源加载实体数据为空，不能加载！");
                Destroy();
                return null;
            }

            if (string.IsNullOrEmpty(entity.ResName))
            {
                SLDebugHelper.WriteError("导入资源名字为空, resName = string.Empty");
                Destroy();
                return null;
            }

            if (string.IsNullOrEmpty(entity.BundlePath))
            {
                SLDebugHelper.WriteError("导入 AssetBundle 路径为空, bundPath = string.Empty");
                Destroy();
                return null;
            }

            string realPath = SLPathHelper.UnityLoadSourcePath() + entity.BundlePath;

            if (string.IsNullOrEmpty(realPath))
            {
                SLDebugHelper.WriteError("导入 AssetBundle 真实路径为空, realPath = string.Empty");
                Destroy();
                return null;
            }

            byte[] bytes = SLManageSource.GetLoadFileBytes(realPath);
            if (bytes == null || bytes.Length <= 0)
            {
                Destroy();
                return null;
            }
            
            AssetBundleCreateRequest request = AssetBundle.CreateFromMemory(bytes);
            CurrentEntity = entity;
            AsyncAsset = request;
            // 开始异步加载
            StartCoroutine(StartLoad(request, entity, callback));
            return entity;
        }

        public override void OnClear()
        {
            CurrentEntity = null;
            AsyncAsset = null;
        }

        public override void OnUpdate(float deltaTime)
        {
            if (CurrentEntity == null || AsyncAsset == null) return;

            if (!AsyncAsset.isDone) CurrentEntity.Progress = AsyncAsset.progress;
        }

    }

}