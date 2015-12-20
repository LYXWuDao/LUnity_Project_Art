using LGame.LCommon;
using UnityEngine;

namespace LGame.LDebug
{

    /***
     * 
     *  控制台输出使用静态输出
     * 
     */

    public static class SLConsole
    {

        /// <summary>
        /// 输出 debug 日志
        /// </summary>
        /// <param name="msg">输入内容</param>
        /// <param name="logType">输出的类型</param>
        private static void WriteDebug(object msg, DebugType logType)
        {
            if (!SLConfig.IsDebugMode) return;
            switch (logType)
            {
                case DebugType.Log:
                    UnityEngine.Debug.Log(msg);
                    break;
                case DebugType.Warning:
                    UnityEngine.Debug.LogWarning(msg);
                    break;
                case DebugType.Error:
                    UnityEngine.Debug.LogError(msg);
                    break;
            }
        }

        /// <summary>
        /// 写日志 log 类型的
        /// </summary>
        /// <param name="msg">输出日志</param>
        public static void Write(object msg)
        {
            WriteDebug(msg, DebugType.Log);
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void Write(string msg, params object[] args)
        {
            WriteDebug(string.Format(msg, args), DebugType.Log);
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="args"></param>
        public static void Write(params object[] args)
        {
            if (!SLConfig.IsDebugMode) return;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < args.Length; ++i)
            {
                sb.Append(args[i]);
                sb.Append(", ");
            }
            WriteDebug(sb.ToString(), DebugType.Log);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteError(object msg)
        {
            WriteDebug(msg, DebugType.Error);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void WriteError(string msg, params object[] args)
        {
            WriteDebug(string.Format(msg, args), DebugType.Error);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="args"></param>
        public static void WriteError(params object[] args)
        {
            if (!SLConfig.IsDebugMode) return;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < args.Length; ++i)
            {
                sb.Append(args[i]);
                sb.Append(", ");
            }
            WriteDebug(sb.ToString(), DebugType.Error);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteWarning(object msg)
        {
            WriteDebug(msg, DebugType.Warning);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void WriteWarning(string msg, params object[] args)
        {
            WriteDebug(string.Format(msg, args), DebugType.Warning);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="args"></param>
        public static void WriteWarning(params object[] args)
        {
            if (!SLConfig.IsDebugMode) return;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < args.Length; ++i)
            {
                sb.Append(args[i]);
                sb.Append(", ");
            }
            WriteDebug(sb.ToString(), DebugType.Warning);
        }

    }

}

