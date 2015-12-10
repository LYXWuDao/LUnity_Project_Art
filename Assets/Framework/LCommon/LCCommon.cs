using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

/****
*
* 
* 公用 枚举和实体 
* 
* 
*/

namespace LGame.LCommon
{

    #region  框架内 公用配置

    /// <summary>
    /// 公用配置
    /// </summary>
    public static class LCSConfig
    {

        /// <summary>
        /// 是否打印日志
        /// 
        /// IsDebugMode = true; 是debug模式  开启日志打印
        /// IsDebugMode = false; 不是debug模式  关闭日志打印
        /// </summary>
        public static bool IsDebugMode = true;

        /// <summary>
        /// 是否自动将日志写入文件
        /// </summary>
        public static bool IsAutoWriteToFile = true;

        /// <summary>
        /// 是否自动将日志写在屏幕上
        /// </summary>
        public static bool IsAutoWriteToGui = false;

        /// <summary>
        /// 是否分析内存
        /// </summary>
        public static bool IsProfiler = true;

        /// <summary>
        /// 1M  大小
        /// </summary>
        public static float KbSize = 1024.0f * 1024.0f;

        /// <summary>
        /// 默认文件日志缓存条数
        /// </summary>
        public static int LogFileCacheCount = 20;

        /// <summary>
        /// 界面深度的跨度
        /// </summary>
        public static int DepthSpan = 30;

    }

    #endregion

    #region 框架内 公用枚举

    /// <summary>
    /// debug 日志输出的类型
    /// </summary>
    public enum DebugType
    {
        Log = 0, // default
        Warning = 2,
        Error = 3,
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
    }

    #endregion

    #region 框架内 公用实体

    /***
     * 
     * 导入的二进制文件实体
     * 
     */

    public class LoadSourceEntity
    {

        /// <summary>
        /// 资源的id
        /// </summary>
        public string SourceId = string.Empty;

        /// <summary>
        ///  资源 AssetBundle
        /// </summary>
        public AssetBundle Bundle = null;

        /// <summary>
        /// 资源的名字
        /// </summary>
        public string ResName = string.Empty;

        /// <summary>
        /// 加载 资源 AssetBundle 的路径
        /// </summary>
        public string BundlePath = string.Empty;

        /// <summary>
        /// 资源加载类型
        /// </summary>
        public LoadType Type = LoadType.Object;

        /// <summary>
        /// 加载的资源
        /// </summary>
        public UnityEngine.Object LoadObj = null;

        /// <summary>
        /// 回调函数
        /// 
        /// 异步加载资源回调
        /// Object  加载获得的资源
        /// </summary>
        public Action<string, UnityEngine.Object> CallObject = null;

        /// <summary>
        /// 回调函数
        /// 
        /// 异步加载资源回调
        /// GameObject   加载获得的资源
        /// </summary>
        public Action<string, GameObject> CallGameObject = null;

        /// <summary>
        /// 回调函数
        /// 
        /// 异步加载资源回调
        /// Texture2D   加载获得的资源
        /// </summary>
        public Action<string, Texture2D> CallTexture = null;

        /// <summary>
        /// 回调函数
        /// 
        /// 异步加载资源回调
        /// UIAtlas   加载获得的资源
        /// </summary>
        public Action<string, UIAtlas> CallUIAtlas = null;

        /// <summary>
        /// 回调函数
        /// 
        /// 异步加载资源回调
        /// AudioClip   加载获得的资源
        /// </summary>
        public Action<string, AudioClip> CallAudioClip = null;

        /// <summary>
        /// 回调函数
        /// 
        /// 异步加载资源回调
        /// AudioSource   加载获得的资源
        /// </summary>
        public Action<string, AudioSource> CallAudioSource = null;

        public LoadSourceEntity()
        {
            SourceId = LCSGuid.NewUpperGuid();
        }

    }

    /// <summary>
    /// 观察程序运行时间实体
    /// </summary>
    public class RecordWatchEntity
    {
        /// <summary>
        /// 观察 key
        /// </summary>
        public string WatchKey = string.Empty;

        /// <summary>
        /// 开始时间
        /// </summary>
        public float StartTime = 0;

        /// <summary>
        /// 结束时间
        /// </summary>
        public float EndTime = 0;

        /// <summary>
        /// 时间差
        /// </summary>
        public float DiffTime
        {
            get
            {
                return EndTime - StartTime;
            }
        }

        /// <summary>
        /// 添加性能观察, 使用C#内置
        /// </summary>
        public Stopwatch Watch = null;

        /// <summary>
        /// 是否正在观察
        /// </summary>
        public bool IsWatch
        {
            set;
            get;
        }

        /// <summary>
        /// 开始观察
        /// </summary>
        public void BeginWatch()
        {
            IsWatch = true;
            Watch = new Stopwatch();
            Watch.Start();
        }

        /// <summary>
        /// 结束观察
        /// </summary>
        public void EndWatch()
        {
            IsWatch = false;
            if (Watch == null) return;
            Watch.Stop();
        }

        /// <summary>
        /// 得到观察时间
        /// 
        /// 单位 秒
        /// </summary>
        public double GetWatchTime()
        {
            if (Watch == null || Watch.IsRunning) return -1;
            //  获取当前实例测量得出的总时间
            TimeSpan timespan = Watch.Elapsed;
            // 转换成秒
            return timespan.TotalSeconds;
        }

    }

    /// <summary>
    /// 使用 gui 输出日志内容实体类
    /// </summary>
    public class GuiLogEntity
    {

        /// <summary>
        /// 日志内容
        /// </summary>
        public string LogString = string.Empty;

        /// <summary>
        /// 日志输出的颜色
        /// 
        /// 默认白色
        /// </summary>
        public Color LogColor = Color.white;

    }

    /// <summary>
    /// 
    /// 所有管理者的泛型实体基类
    /// 
    /// 可以重写
    /// 
    /// </summary>
    public abstract class LATManagerEntity<TKey, TValue>
    {
        /// <summary>
        /// 唯一id
        /// </summary>
        public string Guid = string.Empty;

        /// <summary>
        /// 保存数据
        /// </summary>
        public Dictionary<TKey, TValue> DicEntitys = new Dictionary<TKey, TValue>();

        /// <summary>
        /// 数据所有值集合
        /// </summary>
        public List<TValue> Values = new List<TValue>();

        /// <summary>
        /// 数据所有key 值集合
        /// </summary>
        public List<TKey> Keys = new List<TKey>();

        /// <summary>
        /// 长度
        /// </summary>
        public int Length
        {
            get
            {
                return Keys.Count;
            }
        }

        protected LATManagerEntity()
        {
            Guid = LCSGuid.NewUpperGuid();
        }

        /// <summary>
        /// 增加管理数据
        /// </summary>
        /// <param name="key">key 值</param>
        /// <param name="value">值</param>
        public virtual void Add(TKey key, TValue value)
        {
            TValue old;
            if (!DicEntitys.ContainsKey(key) || DicEntitys.TryGetValue(key, out old)) return;
            DicEntitys.Add(key, value);
            Values.Add(value);
            Keys.Add(key);
        }

        /// <summary>
        /// 修改管理数据
        /// 
        /// key 值不允许修改
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">新的值</param>
        public virtual void Modify(TKey key, TValue value)
        {
            TValue old;
            if (!DicEntitys.ContainsKey(key) || !DicEntitys.TryGetValue(key, out old)) return;
            DicEntitys[key] = value;
            var index = Values.IndexOf(old);
            Values[index] = value;
        }

        /// <summary>
        /// 查找管理数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual TValue Find(TKey key)
        {
            if (!DicEntitys.ContainsKey(key)) return default(TValue);
            TValue old;
            DicEntitys.TryGetValue(key, out old);
            return old;
        }

        /// <summary>
        /// 查实查找管理数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool TryFind(TKey key, out TValue value)
        {
            return null != (value = Find(key));
        }

        /// <summary>
        /// 查找第几个值
        /// </summary>
        /// <param name="index">值下标，从零开始</param>
        /// <returns>找到的值</returns>
        public virtual TValue FindValue(int index)
        {
            if (index < 0 || index >= Length) return default(TValue);
            return Values[index];
        }

        /// <summary>
        /// 该值的下标
        /// </summary>
        /// <param name="value"></param>
        /// <returns> -1表示没找到 </returns>
        public virtual int ValueIndexOf(TValue value)
        {
            return Values.IndexOf(value);
        }

        /// <summary>
        /// 查找第几个key
        /// </summary>
        /// <param name="index">值下标，从零开始</param>
        /// <returns>找到的key值</returns>
        public virtual TKey FindKey(int index)
        {
            if (index < 0 || index >= Length) return default(TKey);
            return Keys[index];
        }

        /// <summary>
        /// 该key值的下标
        /// </summary>
        /// <param name="key"></param>
        /// <returns>-1表示没找到</returns>
        public virtual int KeyIndexOf(TKey key)
        {
            return Keys.IndexOf(key);
        }

        /// <summary>
        /// 移出数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual TValue Remove(TKey key)
        {
            TValue result;
            if (!DicEntitys.ContainsKey(key) || !TryFind(key, out result)) return default(TValue);
            DicEntitys.Remove(key);
            return result;
        }
    }

    /// <summary>
    /// 
    /// 管理者泛型类
    /// 
    /// key值为字符串
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class LCTManagerEntity<TValue> : LATManagerEntity<string, TValue>
    {

    }

    #endregion

}