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
        private static void WriteDebug(object msg, LLogType logType)
        {
            if (!SLConfig.IsDebugMode) return;
            switch (logType)
            {
                case LLogType.Log:
                    Debug.Log(msg);
                    break;
                case LLogType.Warning:
                    Debug.LogWarning(msg);
                    break;
                case LLogType.Error:
                    Debug.LogError(msg);
                    break;
            }
        }

        /// <summary>
        /// 写日志 log 类型的
        /// </summary>
        /// <param name="msg">输出日志</param>
        public static void Write(object msg)
        {
            WriteDebug(msg, LLogType.Log);
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void Write(string msg, params object[] args)
        {
            WriteDebug(string.Format(msg, args), LLogType.Log);
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="args"></param>
        public static void Write(params object[] args)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < args.Length; ++i)
            {
                sb.Append(args[i]);
                sb.Append(", ");
            }
            WriteDebug(sb.ToString(), LLogType.Log);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteError(object msg)
        {
            WriteDebug(msg, LLogType.Error);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void WriteError(string msg, params object[] args)
        {
            WriteDebug(string.Format(msg, args), LLogType.Error);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="args"></param>
        public static void WriteError(params object[] args)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < args.Length; ++i)
            {
                sb.Append(args[i]);
                sb.Append(", ");
            }
            WriteDebug(sb.ToString(), LLogType.Error);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteWarning(object msg)
        {
            WriteDebug(msg, LLogType.Warning);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void WriteWarning(string msg, params object[] args)
        {
            WriteDebug(string.Format(msg, args), LLogType.Warning);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="args"></param>
        public static void WriteWarning(params object[] args)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < args.Length; ++i)
            {
                sb.Append(args[i]);
                sb.Append(", ");
            }
            WriteDebug(sb.ToString(), LLogType.Warning);
        }

    }

}

