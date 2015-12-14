using System.IO;
using UnityEngine;

/*****
 * 
 * 
 *  框架所有路径配置
 *  
 * 
 */

namespace LGame.LCommon
{

    public static class LCSPathHelper
    {

        /// <summary>
        /// untiy  aeests 文件目录
        /// </summary>
        /// <returns></returns>
        public static string UnityAssets()
        {
            return Application.dataPath + "/";
        }

        /// <summary>
        /// untiy 内部资源 Resources 路径
        /// </summary>
        /// <returns></returns>
        public static string UntiyResource()
        {
            return Application.dataPath + "Resources/";
        }

        /// <summary>
        /// 
        /// 缺省的内部资源路径
        /// 
        /// 例: Assets/Resources
        /// 
        /// </summary>
        /// <returns></returns>
        public static string UnityDefaultResource()
        {
            return "Assets/Resources/";
        }

        /// <summary>
        /// untiy  streamingAssets 文件目录
        /// </summary>
        /// <returns></returns>
        public static string UnityStreamingAssets()
        {
            return Application.streamingAssetsPath + "/";
        }

        /// <summary>
        /// 
        /// 资源打包存放根目录
        /// 
        /// untiy assets 目录下 SourceAssets 文件夹
        /// </summary>
        /// <returns></returns>
        public static string UnityBuildRootPath()
        {
            return "E:/selfworkspace/LUnity/LUnity_Project_Data/android/";
        }

        /// <summary>
        /// 
        /// 资源打包存放根目录
        /// 
        /// untiy assets 目录下 SourceAssets/UI 文件夹
        /// </summary>
        /// <returns></returns>
        public static string UnityBuildUiPath()
        {
            return UnityBuildRootPath() + "UI/";
        }

        /// <summary>
        /// 
        /// untiy  StreamingAssets 路径 下的 Assets文件夹
        /// 
        /// </summary>
        /// <returns></returns>
        public static string UnityStreamingDefaultPath()
        {
            return "Assets/";
        }

        /// <summary>
        /// 游戏打包时,更新包资源根目录的位置
        /// 
        /// untiy  StreamingAssets/Assets 路径 
        /// </summary>
        /// <returns></returns>
        public static string UnityStreamingSourcePath()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
            return UnityStreamingDefaultPath();
#endif
            return UnityStreamingAssets() + "Assets/";
        }

        /// <summary>
        /// 日志文件保存路径
        /// </summary>
        /// <returns></returns>
        public static string UnityLogFilePath()
        {
            return UnityAssets() + "log.txt";
        }

        /// <summary>
        /// Android 工具类地址
        /// </summary>
        /// <returns></returns>
        public static string UnityAndroidUtilPath()
        {
            return "com.LUnity.game.LUtils.LUnityUtils";
        }

    }
}

