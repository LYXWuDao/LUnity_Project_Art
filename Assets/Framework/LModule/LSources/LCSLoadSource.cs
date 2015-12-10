using System;
using System.Collections;
using System.IO;
using LGame.LCommon;
using LGame.LDebug;
using UnityEngine;

/****
 * 
 * 
 *  导入资源
 * 
 * 
 */

namespace LGame.LSource
{

    public static class LCSLoadSource
    {
        /// <summary>
        /// 加载 Assets Streaming 文件夹下资源
        /// </summary>
        /// <param name="resName">加载的资源名字</param>
        /// <returns></returns>
        private static LoadSourceEntity LoadStreamingSources(string resName)
        {
            return LoadStreamingSources(resName, string.Empty);
        }

        /// <summary>
        /// 加载 Assets Streaming 文件夹下资源
        /// </summary>
        /// <param name="resName">加载的资源名字</param>
        /// <param name="bundPath">Streaming 下文件夹  如： UI</param>
        /// <param name="type">资源的类型</param>
        /// <returns></returns>
        private static LoadSourceEntity LoadStreamingSources(string resName, string bundPath)
        {
            if (string.IsNullOrEmpty(bundPath)) bundPath = resName;
            string path = LCSPathHelper.UnityStreamingSourcePath() + bundPath;
            return LoadBinarySources(path, resName);
        }

        /// <summary>
        /// 加载 Assets SourceAssets 文件夹下资源
        /// </summary>
        /// <param name="resName">加载的资源名字</param>
        /// <returns></returns>
        private static LoadSourceEntity LoadBuildSources(string resName)
        {
            return LoadBuildSources(resName, string.Empty);
        }

        /// <summary>
        /// 加载 Assets SourceAssets 文件夹下资源
        /// </summary>
        /// <param name="resName">加载的资源名字</param>
        /// <param name="bundPath">导入 AssetBundle 路径</param>
        /// <param name="type">资源的类型</param>
        /// <returns></returns>
        private static LoadSourceEntity LoadBuildSources(string resName, string bundPath)
        {
            if (string.IsNullOrEmpty(bundPath)) bundPath = resName;
            string path = LCSPathHelper.UnityBuildRootPath() + bundPath;
            return LoadBinarySources(path, resName);
        }

        /// <summary>
        /// 导入资源
        /// 
        /// 以二进制加载
        /// </summary>
        /// <param name="bundPath">导入 AssetBundle 路径 </param>
        /// <param name="resName"> 从 AssetBundle 中导入的资源名  </param>
        /// <param name="type">加载资源的类型</param>
        /// <returns></returns>
        private static LoadSourceEntity LoadBinarySources(string bundPath, string resName)
        {
            if (string.IsNullOrEmpty(resName))
            {
                LCSConsole.WriteError("导入资源名字为空,resName = " + resName);
                return null;
            }

            if (string.IsNullOrEmpty(bundPath))
            {
                LCSConsole.WriteError("导入 AssetBundle 路径为空, bundPath = " + bundPath);
                return null;
            }

            if (!File.Exists(bundPath))
            {
                LCSConsole.WriteError("导入 AssetBundle 路径不存在,bundPath = " + bundPath);
                return null;
            }

            byte[] bytes = File.ReadAllBytes(bundPath);
            AssetBundle bundle = AssetBundle.CreateFromMemoryImmediate(bytes);

            if (bundle == null)
            {
                LCSConsole.WriteError("创建资源 AssetBundle 失败!");
                return null;
            }

            UnityEngine.Object retobj = bundle.Load(resName);
            if (retobj == null)
            {
                LCSConsole.WriteError("资源 AssetBundle 中不存在 resName = " + resName);
                return null;
            }

            return new LoadSourceEntity
            {
                LoadObj = retobj,
                Bundle = bundle,
                BundlePath = bundPath,
                ResName = resName
            };
        }

        /// <summary>
        /// 同步导入资源
        /// 
        /// 直接加载  Resources 文件夹下资源
        /// </summary>
        /// <param name="resPath">
        /// 
        /// 资源名字
        /// 
        /// Resources夹下路径
        /// 例如：Scenes/battleScene.scene
        /// </param>
        /// <returns>
        /// 加载的资源
        /// 
        /// return null : 资源名字为空，或者资源不存在， 或者资源类型不对
        /// 
        /// </returns>
        private static LoadSourceEntity LoadResources(string resPath)
        {
            if (string.IsNullOrEmpty(resPath))
            {
                LCSConsole.WriteError("导入资源名字为空,resPath = string.Empty");
                return null;
            }
            UnityEngine.Object load = Resources.Load(resPath);
            if (load == null)
            {
                LCSConsole.WriteError("导入资源不存在！！");
                return null;
            }
            return new LoadSourceEntity
            {
                ResName = resPath,
                BundlePath = resPath,
                LoadObj = load,
                Bundle = null
            };
        }

        /// <summary>
        /// 同步加载资源
        /// 
        /// 区分 Android， iphone， untiy 
        /// 
        /// 默认 unity
        /// 
        /// </summary>
        /// <param name="resName">资源名字</param>
        /// <param name="bundPath">资源完整路径</param>
        /// <param name="type">加载资源的类型</param>
        /// <returns></returns>
        public static LoadSourceEntity LoadSource(string resName, string bundPath)
        {
            return LoadBuildSources(resName, bundPath);
        }

    }

}