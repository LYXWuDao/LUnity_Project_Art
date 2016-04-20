using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LGame.LCommon
{

    /***
     *
     *
     *  框架内部使用的所以枚举
     * 
     *
     */


    /// <summary>
    /// debug 日志输出的类型
    /// </summary>
    public enum LLogType
    {
        /// <summary>
        /// 普通日志（白色）
        /// </summary>
        Log = 0,

        /// <summary>
        /// 警告日志（黄色）
        /// </summary>
        Warning = 2,

        /// <summary>
        /// 错误日志（红色）
        /// </summary>
        Error = 3,
    }


    /// <summary>
    /// 资源加载方式
    /// </summary>
    public enum LoadWay
    {

        /// <summary>
        /// 同步 AssetBundle资源
        /// </summary>
        SyncAsset = 1,

        /// <summary>
        /// 异步AssetBundle资源
        /// </summary>
        AsyncAsset = 2,

        /// <summary>
        /// 文本资源
        /// </summary>
        TextSource = 3,

        /// <summary>
        /// 内部 Resources 资源
        /// </summary>
        Resource = 4,

        /// <summary>
        /// 异步场景资源
        /// </summary>
        AsyncSceneSource = 5,
    }

    /// <summary>
    /// 资源加载类型
    /// </summary>
    public enum LoadType
    {
        /// <summary>
        /// 所有的类型
        /// </summary>
        Object = 1,

        /// <summary>
        /// 一般模式
        /// 
        /// prefab： 界面，模型
        /// </summary>
        GameObject = 2,

        /// <summary>
        /// 贴图
        /// </summary>
        Texture2D = 3,

        /// <summary>
        /// ngui 图集
        /// </summary>
        UIAtlas = 4,

        /// <summary>
        /// 加载音频
        /// </summary>
        AudioClip = 5,

        /// <summary>
        /// 加载视频
        /// </summary>
        AudioSource = 6,

        /// <summary>
        /// 加载文本 Asset
        /// </summary>
        TextAsset = 7,

        /// <summary>
        /// 直接加载的文本内容
        /// </summary>
        TextContent = 8,

    }

    /// <summary>
    /// ngui tween 运动的类型
    /// </summary>
    public enum TweenerType
    {

        None = 0,

        /// <summary>
        /// 透明度
        /// </summary>
        Alpha = 1,

        /// <summary>
        /// 颜色
        /// </summary>
        Color = 2,

        /// <summary>
        /// 位置
        /// </summary>
        Position = 3,

        /// <summary>
        /// 旋转
        /// </summary>
        Rotation = 4,

        /// <summary>
        /// 大小
        /// </summary>
        Scale = 5
    }

}
