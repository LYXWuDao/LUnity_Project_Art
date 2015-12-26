using System;
using System.Collections.Generic;
using LGame.LBase;
using LGame.LCommon;
using LGame.LDebug;
using LGame.LAndroid;
using UnityEngine;
using System.IO;

namespace LGame.LSource
{

    /***
     * 
     * 
     * 所有资源加载卸载管理类型
     * 
     * 包括图片，界面，模型，声音等
     * 
     * 
     */

    public sealed class SLManageSource : ATLManager<LoadSourceEntity>
    {
        /// <summary>
        /// 资源加载方式
        /// </summary>
        public enum LoadWay
        {
            SyncAsset = 1,

            AsyncAsset = 2,

            TextSource = 3,

            Resource = 4,
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="resName"></param>
        /// <param name="bundPath"></param>
        /// <param name="ltype"></param>
        /// <param name="way">加载资源的方式</param>
        /// <returns></returns>
        private static LoadSourceEntity LoadSource(string resName, string bundPath, LoadType ltype, LoadWay way, Action<LoadSourceEntity> finish = null)
        {
            LoadSourceEntity entity = null;
            if (TryFind<SLManageSource>(resName, out entity)) return entity;
            entity = new LoadSourceEntity { ResName = resName, BundlePath = bundPath, Type = ltype };
            LoadSourceEntity load = null;

            switch (way)
            {
                case LoadWay.SyncAsset:
                    load = SLLoadSource.LoadAssetSource(entity);
                    break;
                case LoadWay.AsyncAsset:
                    load = CLAsyncLoadSource.Instance.AsyncLoadAssetSource(entity, finish);
                    break;
                case LoadWay.TextSource:
                    load = SLLoadSource.LoadTextSource(entity);
                    break;
                case LoadWay.Resource:
                    load = SLLoadSource.LoadResources(entity);
                    break;
            }

            if (load == null)
            {
                // 资源加载失败，就表示这个资源不需要在加载
                entity.Progress = 1;
                entity.IsDone = true;
            }

            Add<SLManageSource>(resName, entity);
            return entity;
        }

        /// <summary>
        /// 获取各平台文件的字节流
        /// </summary>
        /// <param name="filePath">文件的路径</param>
        /// <returns></returns>
        public static byte[] GetLoadFileBytes(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return null;
#if !UNITY_EDITOR && UNITY_ANDROID
            return SLAndroidJava.GetStaticBytes("getFileBytes", filePath);
#endif
            if (!File.Exists(filePath))
            {
                SLDebugHelper.WriteError("文件路径不存在,bundPath = " + filePath);
                return null;
            }
            return File.ReadAllBytes(filePath);
        }

        /// <summary>
        /// 同步加载打包的资源
        /// </summary>
        /// <param name="resName">资源的名字</param>
        /// <param name="bundPath">资源的路径</param>
        /// <param name="ltype">资源的类型</param>
        /// <returns></returns>
        public static LoadSourceEntity LoadAssetSource(string resName, string bundPath, LoadType ltype)
        {
            return LoadSource(resName, bundPath, ltype, LoadWay.SyncAsset);
        }

        /// <summary>
        /// 异步加载打包的资源
        /// </summary>
        /// <param name="resName">资源的名字</param>
        /// <param name="bundPath">资源的路径</param>
        /// <param name="ltype">资源的类型</param>
        /// <param name="finish"></param>
        /// <returns></returns>
        public static LoadSourceEntity AsyncLoadAssetSource(string resName, string bundPath, LoadType ltype, Action<LoadSourceEntity> finish)
        {
            return LoadSource(resName, bundPath, ltype, LoadWay.SyncAsset, finish);
        }

        /// <summary>
        /// 加载没有打包的资源
        /// 
        /// 一般用来加载文本文件
        /// 
        /// </summary>
        /// <param name="resName">资源的名字</param>
        /// <param name="bundPath">资源的路径</param>
        /// <returns></returns>
        public static LoadSourceEntity LoadTextSource(string resName, string bundPath)
        {
            return LoadSource(resName, bundPath, LoadType.TextContent, LoadWay.TextSource);
        }

        /// <summary>
        /// 加载 Resource 下面的文件
        /// </summary>
        /// <param name="resName">资源的名字</param>
        /// <param name="bundPath">资源的路径</param>
        /// <param name="ltype">资源的类型</param>
        /// <returns></returns>
        public static LoadSourceEntity LoadResource(string resName, string bundPath, LoadType ltype)
        {
            return LoadSource(resName, bundPath, LoadType.TextContent, LoadWay.Resource);
        }

        /// <summary>
        /// 移出单个资源
        /// </summary>
        /// <param name="resName">资源名字</param>
        /// <returns></returns>
        public static bool RemoveSource(string resName)
        {
            if (string.IsNullOrEmpty(resName))
            {
                SLDebugHelper.WriteError("移出的资源名字为空！,resName = " + resName);
                return true;
            }
            LoadSourceEntity entity;
            if (!TryFind<SLManageSource>(resName, out entity))
            {
                SLDebugHelper.WriteError("移出的资源不存在！,resName = " + resName);
                return true;
            }
            SLUnloadSource.UnLoadSource(entity.Bundle);
            Remove<SLManageSource>(resName);
            return true;
        }

        /// <summary>
        /// 移出所有资源
        /// </summary>
        /// <returns></returns>
        public static void RemoveAllSource()
        {
            foreach (LoadSourceEntity entity in FindValues<SLManageSource>())
                SLUnloadSource.UnLoadSource(entity.Bundle);
            Remove<SLManageSource>();
        }
    }

}