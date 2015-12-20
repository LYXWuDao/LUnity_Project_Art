using System;
using System.Collections;
using System.IO;
using LGame.LAndroid;
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

    public static class SLLoadSource
    {
        /// <summary>
        /// 导入资源
        /// 
        /// 以二进制加载
        /// </summary>
        /// <param name="bundPath">导入 AssetBundle 路径 </param>
        /// <param name="resName"> 从 AssetBundle 中导入的资源名  </param>
        /// <returns></returns>
        private static LoadSourceEntity LoadBinarySources(string resName, string bundPath)
        {
            if (string.IsNullOrEmpty(resName))
            {
                SLDebugHelper.WriteError("导入资源名字为空,resName = " + resName);
                return null;
            }

            if (string.IsNullOrEmpty(bundPath))
            {
                SLDebugHelper.WriteError("导入 AssetBundle 路径为空, bundPath = " + bundPath);
                return null;
            }

            byte[] bytes = SLManageSource.GetLoadFileBytes(bundPath);
            if (bytes == null)
            {
                SLDebugHelper.WriteError("获取文件的字节流数据为空! bytes = null");
                return null;
            }

            AssetBundle bundle = AssetBundle.CreateFromMemoryImmediate(bytes);

            if (bundle == null)
            {
                SLDebugHelper.WriteError("创建资源 AssetBundle 失败!");
                return null;
            }

            UnityEngine.Object retobj = bundle.Load(resName);
            if (retobj == null)
            {
                SLDebugHelper.WriteError("资源 AssetBundle 中不存在 resName = " + resName);
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
        /// 同步加载 AssetBundle 类 资源
        /// 
        /// 区分 Android， iphone， untiy 
        /// 
        /// 默认 unity
        /// 
        /// </summary>
        /// <param name="resName">资源名字</param>
        /// <param name="bundPath">资源完整路径</param>
        /// <returns></returns>
        public static LoadSourceEntity LoadAssetBundleSource(string resName, string bundPath)
        {
            if (string.IsNullOrEmpty(bundPath)) bundPath = resName;
            string path = SLPathHelper.UnityLoadSourcePath() + bundPath;
            return LoadBinarySources(resName, path);
        }

        /// <summary>
        /// 得到资源的二进制数据
        /// </summary>
        /// <param name="resName">资源的名字</param>
        /// <param name="bundPath">资源的路径</param>
        public static LoadSourceEntity LoadSourceBytes(string resName, string bundPath)
        {
            if (string.IsNullOrEmpty(bundPath))
            {
                SLDebugHelper.WriteError("加载 bundPath 路径为空, bundPath = string.Empty.");
                return null;
            }
            string path = SLPathHelper.UnityLoadSourcePath() + bundPath;
            return new LoadSourceEntity
            {
                BundlePath = bundPath,
                ResName = resName,
                SourceBytes = SLManageSource.GetLoadFileBytes(path)
            };
        }

        
    }

}