
namespace LGame.LCommon
{

    /***
     *
     *
     * 游戏框架使用的基础配置
     *
     *
     */

    public class SLConfig
    {
        /// <summary>
        /// 是否打印日志
        /// 
        /// IsDebugMode = true;  是debug模式    开启日志打印
        /// IsDebugMode = false; 不是debug模式  关闭日志打印
        /// </summary>
        public static bool IsDebugMode = true;

        /// <summary>
        /// 是否开启堆栈模式
        /// </summary>
        public static bool IsDebugStack = false;

        /// <summary>
        /// 是否自动将日志写入文件
        /// </summary>
        public static bool IsWriteLogToFile = false;

        /// <summary>
        /// 是否自动将日志写在屏幕上
        /// </summary>
        public static bool IsWriteLogToGui = false;

        /// <summary>
        /// 1M  大小
        /// </summary>
        public static float KbSize = 1024.0f * 1024.0f;

        /// <summary>
        /// 默认文件日志缓存条数
        /// </summary>
        public static int DebugCacheCount = 20;

        /// <summary>
        /// 是否分析内存
        /// </summary>
        public static bool IsProfiler = true;

        /// <summary>
        /// 是否使用 lua
        /// </summary>
        public static bool IsLuaWindow = false;

        /// <summary>
        /// 界面深度的跨度
        /// </summary>
        public static int DepthSpan = 30;
    }
}
