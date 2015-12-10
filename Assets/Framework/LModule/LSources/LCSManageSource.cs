using System;
using System.Collections.Generic;
using LGame.LBase;
using LGame.LCommon;
using LGame.LDebug;
using UnityEngine;

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

    public sealed class LCSManageSource : LATManager<LoadSourceEntity>
    {

        /// <summary>
        /// 
        /// 私有话 
        /// 
        /// 不允许实例化
        /// 
        /// </summary>
        private LCSManageSource()
        {
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="entity">资源加载实体</param>
        /// <param name="resName">资源名字</param>
        /// <param name="bundPath">加载路径</param>
        /// <param name="type">资源加载类型</param>
        private static void AsyncLoadSource(LoadSourceEntity entity, string resName, string bundPath, LoadType type)
        {
            LoadSourceEntity old = null;
            if (TryFind<LCSManageSource>(resName, out old))
            {
                AsyncLoadCallback(old);
                return;
            }
            entity.ResName = resName;
            entity.BundlePath = bundPath;
            entity.Type = type;
            LCAsyncLoadSource.Instance.LoadSource(entity, AsyncLoadCallback);
        }

        /// <summary>
        /// 异步加载资源完成回调
        /// 
        /// 增加异步加载的资源管理
        /// </summary>
        /// <param name="entity"></param>
        private static void AsyncLoadCallback(LoadSourceEntity entity)
        {
            if (null == entity)
            {
                LCSConsole.WriteError("资源加载回调数据为空！ entity = null");
                return;
            }

            if (null == entity.LoadObj)
            {
                LCSConsole.WriteError("资源回调中加载的资源为空！ entity.LoadObj = null");
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
            Add<LCSManageSource>(entity.ResName, entity);
        }

        /// <summary>
        /// 同步加载资源
        /// 
        /// 资源的名字不能相同,加特殊标志区分
        /// </summary>
        /// <param name="resName"></param>
        /// <param name="bundPath"></param>
        /// <returns></returns>
        private static UnityEngine.Object SyncLoadSource(string resName, string bundPath)
        {
            LoadSourceEntity entity = null;
            if (TryFind<LCSManageSource>(resName, out entity)) return entity.LoadObj;
            entity = LCSLoadSource.LoadSource(resName, bundPath);
            if (entity == null) return null;
            Add<LCSManageSource>(resName, entity);
            return entity.LoadObj;
        }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <param name="resName">资源名字，不带后缀, 资源名字唯一</param>
        /// <param name="bundPath">资源完成路径(打包后的路径)</param>
        /// <returns></returns>
        public static GameObject LoadSource(string resName, string bundPath)
        {
            UnityEngine.Object load = SyncLoadSource(resName, bundPath);
            if (load == null)
            {
                LCSConsole.WriteError("同步加载资源 GameObject 失败!");
                return null;
            }
            return load as GameObject;
        }

        /// <summary>
        /// 加载贴图
        /// </summary>
        /// <returns></returns>
        public static Texture2D LoadTexture(string resName, string bundPath)
        {
            UnityEngine.Object load = SyncLoadSource(resName, bundPath);
            if (load == null)
            {
                LCSConsole.WriteError("同步加载资源 Texture2D 失败!");
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
        public static AudioClip LoadAudioClip(string resName, string bundPath)
        {
            UnityEngine.Object load = SyncLoadSource(resName, bundPath);
            if (load == null)
            {
                LCSConsole.WriteError("同步加载资源 AudioClip 失败!");
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
        public static UIAtlas LoadUIAtlas(string resName, string bundPath)
        {
            UnityEngine.Object load = SyncLoadSource(resName, bundPath);
            if (load == null)
            {
                LCSConsole.WriteError("同步加载资源 UIAtlas 失败!");
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
        public static AudioSource LoadAudioSource(string resName, string bundPath)
        {
            UnityEngine.Object load = SyncLoadSource(resName, bundPath);
            if (load == null)
            {
                LCSConsole.WriteError("同步加载资源 AudioSource 失败!");
                return null;
            }
            return load as AudioSource;
        }

        /// <summary>
        /// 异步导入数据
        /// </summary>
        /// <param name="resName">资源名字，不带后缀, 资源名字唯一</param>
        /// <param name="bundPath">资源完成路径(打包后的路径)</param>
        /// <param name="type">资源加载类型</param>
        /// <param name="callback">加载完成回调</param>
        /// <returns></returns>
        public static void AsyncLoadSource(string resName, string bundPath, Action<string, GameObject> callback)
        {
            AsyncLoadSource(new LoadSourceEntity { CallGameObject = callback }, resName, bundPath, LoadType.GameObject);
        }

        /// <summary>
        /// 异步加载贴图
        /// </summary>
        /// <returns></returns>
        public static void AsyncLoadTexture(string resName, string bundPath, Action<string, Texture2D> callback)
        {
            AsyncLoadSource(new LoadSourceEntity { CallTexture = callback }, resName, bundPath, LoadType.GameObject);
        }

        /// <summary>
        /// 异步加载声音
        /// </summary>
        /// <param name="resName"></param>
        /// <param name="bundPath"></param>
        /// <returns></returns>
        public static void AsyncLoadAudioClip(string resName, string bundPath, Action<string, AudioClip> callback)
        {
            AsyncLoadSource(new LoadSourceEntity { CallAudioClip = callback }, resName, bundPath, LoadType.GameObject);
        }

        /// <summary>
        /// 异步加载 ngui 图集
        /// </summary>
        /// <param name="resName"></param>
        /// <param name="bundPath"></param>
        /// <returns></returns>
        public static void AsyncLoadUIAtlas(string resName, string bundPath, Action<string, UIAtlas> callback)
        {
            AsyncLoadSource(new LoadSourceEntity { CallUIAtlas = callback }, resName, bundPath, LoadType.GameObject);
        }

        /// <summary>
        /// 异步加载视频
        /// </summary>
        /// <param name="resName"></param>
        /// <param name="bundPath"></param>
        /// <returns></returns>
        public static void AsyncLoadAudioSource(string resName, string bundPath, Action<string, AudioSource> callback)
        {
            AsyncLoadSource(new LoadSourceEntity { CallAudioSource = callback }, resName, bundPath, LoadType.GameObject);
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
                LCSConsole.WriteError("移出的资源名字为空！,resName = " + resName);
                return false;
            }
            LoadSourceEntity entity;
            if (!TryFind<LCSManageSource>(resName, out entity))
            {
                LCSConsole.WriteError("移出的资源不存在！,resName = " + resName);
                return false;
            }
            LCSUnloadSource.UnLoadSource(entity.Bundle);
            Remove<LCSManageSource>(resName);
            return true;
        }

        /// <summary>
        /// 移出所有资源
        /// </summary>
        /// <returns></returns>
        public static void RemoveAllSource()
        {
            foreach (LoadSourceEntity entity in FindValues<LCSManageSource>())
                LCSUnloadSource.UnLoadSource(entity.Bundle);
            Remove<LCSManageSource>();
        }

    }

}