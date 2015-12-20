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
        /// 
        /// 私有话 
        /// 
        /// 不允许实例化
        /// 
        /// </summary>
        private SLManageSource()
        {
        }

        /// <summary>
        /// 异步加载 assetbundle 资源
        /// </summary>
        /// <param name="entity">资源加载实体</param>
        /// <param name="resName">资源名字</param>
        /// <param name="bundPath">加载路径</param>
        /// <param name="type">资源加载类型</param>
        private static void AsyncLoadAssetSource(LoadSourceEntity entity, string resName, string bundPath, LoadType type)
        {
            LoadSourceEntity old = null;
            if (TryFind<SLManageSource>(resName, out old))
            {
                AsyncLoadAssetCallback(old);
                return;
            }
            entity.ResName = resName;
            entity.BundlePath = bundPath;
            entity.Type = type;
            CLAsyncLoadSource.Instance.LoadAssetSource(entity, AsyncLoadAssetCallback);
        }

        /// <summary>
        /// 异步加载资源完成回调
        /// 
        /// 增加异步加载的资源管理
        /// </summary>
        /// <param name="entity"></param>
        private static void AsyncLoadAssetCallback(LoadSourceEntity entity)
        {
            if (null == entity)
            {
                SLDebugHelper.WriteError("资源加载回调数据为空！ entity = null");
                return;
            }

            if (null == entity.LoadObj)
            {
                SLDebugHelper.WriteError("资源回调中加载的资源为空！ entity.LoadObj = null");
                return;
            }

            switch (entity.Type)
            {
                case LoadType.Object:
                    if (entity.CallObject == null) return;
                    entity.CallObject(entity.ResName, entity.LoadObj);
                    break;
                case LoadType.GameObject:
                    if (entity.CallGameObject == null) return;
                    entity.CallGameObject(entity.ResName, entity.LoadObj as GameObject);
                    break;
                case LoadType.Texture2D:
                    if (entity.CallTexture == null) return;
                    entity.CallTexture(entity.ResName, entity.LoadObj as Texture2D);
                    break;
                case LoadType.AudioClip:
                    if (entity.CallAudioClip == null) return;
                    entity.CallAudioClip(entity.ResName, entity.LoadObj as AudioClip);
                    break;
                case LoadType.UIAtlas:
                    if (entity.CallUIAtlas == null) return;
                    entity.CallUIAtlas(entity.ResName, entity.LoadObj as UIAtlas);
                    break;
                case LoadType.AudioSource:
                    if (entity.CallAudioSource == null) return;
                    entity.CallAudioSource(entity.ResName, entity.LoadObj as AudioSource);
                    break;
            }
            Add<SLManageSource>(entity.ResName, entity);
        }

        /// <summary>
        /// 同步加载资源
        /// 
        /// 资源的名字不能相同,加特殊标志区分
        /// </summary>
        /// <param name="resName">资源的名字</param>
        /// <param name="bundPath">资源的路径</param>
        /// <returns></returns>
        private static UnityEngine.Object SyncLoadSource(string resName, string bundPath)
        {
            LoadSourceEntity entity = null;
            if (TryFind<SLManageSource>(resName, out entity)) return entity.LoadObj;
            entity = SLLoadSource.LoadAssetBundleSource(resName, bundPath);
            if (entity == null) return null;
            Add<SLManageSource>(resName, entity);
            return entity.LoadObj;
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
                SLDebugHelper.WriteError("文件路径不成长,bundPath = " + filePath);
                return null;
            }
            return File.ReadAllBytes(filePath);
        }


        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <param name="resName">资源名字，不带后缀, 资源名字唯一</param>
        /// <param name="bundPath">资源完成路径(打包后的路径)</param>
        /// <returns></returns>
        public static GameObject LoadAssetSource(string resName, string bundPath)
        {
            UnityEngine.Object load = SyncLoadSource(resName, bundPath);
            if (load == null)
            {
                SLDebugHelper.WriteError("同步加载资源 GameObject 失败!");
                return null;
            }
            return load as GameObject;
        }

        /// <summary>
        /// 加载贴图
        /// </summary>
        /// <returns></returns>
        public static Texture2D LoadAssetTexture(string resName, string bundPath)
        {
            UnityEngine.Object load = SyncLoadSource(resName, bundPath);
            if (load == null)
            {
                SLDebugHelper.WriteError("同步加载资源 Texture2D 失败!");
                return null;
            }
            return load as Texture2D;
        }

        /// <summary>
        /// 加载声音
        /// </summary>
        /// <param name="resName"></param>
        /// <param name="bundPath"></param>
        /// <returns></returns>
        public static AudioClip LoadAssetAudioClip(string resName, string bundPath)
        {
            UnityEngine.Object load = SyncLoadSource(resName, bundPath);
            if (load == null)
            {
                SLDebugHelper.WriteError("同步加载资源 AudioClip 失败!");
                return null;
            }
            return load as AudioClip;
        }

        /// <summary>
        /// 加载 ngui 图集
        /// </summary>
        /// <param name="resName"></param>
        /// <param name="bundPath"></param>
        /// <returns></returns>
        public static UIAtlas LoadAssetUIAtlas(string resName, string bundPath)
        {
            UnityEngine.Object load = SyncLoadSource(resName, bundPath);
            if (load == null)
            {
                SLDebugHelper.WriteError("同步加载资源 UIAtlas 失败!");
                return null;
            }
            return load as UIAtlas;
        }

        /// <summary>
        /// 加载视频
        /// </summary>
        /// <param name="resName"></param>
        /// <param name="bundPath"></param>
        /// <returns></returns>
        public static AudioSource LoadAssetAudioSource(string resName, string bundPath)
        {
            UnityEngine.Object load = SyncLoadSource(resName, bundPath);
            if (load == null)
            {
                SLDebugHelper.WriteError("同步加载资源 AudioSource 失败!");
                return null;
            }
            return load as AudioSource;
        }

        /// <summary>
        /// 加载打包资源 AssetBundle 视频
        /// </summary>
        /// <param name="resName">资源名字</param>
        /// <param name="bundPath">资源加载路径</param>
        /// <returns></returns>
        public static TextAsset LoadAssetTextAsset(string resName, string bundPath)
        {
            UnityEngine.Object load = SyncLoadSource(resName, bundPath);
            if (load == null)
            {
                SLDebugHelper.WriteError("同步加载资源 TextAsset 失败!");
                return null;
            }
            return load as TextAsset;
        }

        /// <summary>
        /// 异步导入 assetbundle 数据
        /// </summary>
        /// <param name="resName">资源名字，不带后缀, 资源名字唯一</param>
        /// <param name="bundPath">资源完成路径(打包后的路径)</param>
        /// <param name="callback">加载完成回调</param>
        /// <returns></returns>
        public static void AsyncLoadAssetSource(string resName, string bundPath, Action<string, GameObject> callback)
        {
            AsyncLoadAssetSource(new LoadSourceEntity { CallGameObject = callback }, resName, bundPath, LoadType.GameObject);
        }

        /// <summary>
        /// 异步加载assetbundle贴图
        /// </summary>
        /// <param name="resName">加载资源的名字</param>
        /// <param name="bundPath">加载资源的地址</param>
        /// <param name="callback">回调函数</param>
        /// <returns></returns>
        public static void AsyncLoadAssetTexture(string resName, string bundPath, Action<string, Texture2D> callback)
        {
            AsyncLoadAssetSource(new LoadSourceEntity { CallTexture = callback }, resName, bundPath, LoadType.GameObject);
        }

        /// <summary>
        /// 异步加载assetbundle声音
        /// </summary>
        /// <param name="resName">加载资源的名字</param>
        /// <param name="bundPath">加载资源的地址</param>
        /// <param name="callback">回调函数</param>
        /// <returns></returns>
        public static void AsyncLoadAssetAudioClip(string resName, string bundPath, Action<string, AudioClip> callback)
        {
            AsyncLoadAssetSource(new LoadSourceEntity { CallAudioClip = callback }, resName, bundPath, LoadType.GameObject);
        }

        /// <summary>
        /// 异步加载 ngui assetbundle 图集
        /// </summary>
        /// <param name="resName">加载资源的名字</param>
        /// <param name="bundPath">加载资源的地址</param>
        /// <param name="callback">回调函数</param>
        /// <returns></returns>
        public static void AsyncLoadAssetUIAtlas(string resName, string bundPath, Action<string, UIAtlas> callback)
        {
            AsyncLoadAssetSource(new LoadSourceEntity { CallUIAtlas = callback }, resName, bundPath, LoadType.GameObject);
        }

        /// <summary>
        /// 异步加载 assetbundle 视频
        /// </summary>
        /// <param name="resName">加载资源的名字</param>
        /// <param name="bundPath">加载资源的地址</param>
        /// <param name="callback">回调函数</param>
        /// <returns></returns>
        public static void AsyncLoadAssetAudioSource(string resName, string bundPath, Action<string, AudioSource> callback)
        {
            AsyncLoadAssetSource(new LoadSourceEntity { CallAudioSource = callback }, resName, bundPath, LoadType.GameObject);
        }

        /// <summary>
        /// 
        /// 没有打包的数据
        /// 
        /// 根据资源路径直接加载文本文件
        /// 
        /// </summary>
        /// <param name="resName">文件名字</param>
        /// <param name="bundPath">文件路径</param>
        /// <returns></returns>
        public static string LoadTextAsset(string resName, string bundPath)
        {
            LoadSourceEntity entity = null;
            if (TryFind<SLManageSource>(resName, out entity)) return entity.TextContent;
            entity = SLLoadSource.LoadSourceBytes(resName, bundPath);
            if (entity == null || entity.SourceBytes == null) return string.Empty;
            entity.TextContent = System.Text.Encoding.UTF8.GetString(entity.SourceBytes);
            Add<SLManageSource>(resName, entity);
            return entity.TextContent;
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
                return false;
            }
            LoadSourceEntity entity;
            if (!TryFind<SLManageSource>(resName, out entity))
            {
                SLDebugHelper.WriteError("移出的资源不存在！,resName = " + resName);
                return false;
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