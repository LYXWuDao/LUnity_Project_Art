using System;
using System.Collections.Generic;
using LGame.LCommon;

namespace LGame.LDebug
{

    /*
     * 
     * 日志输出方式
     * 
     */

    public sealed class SLDebugHelper
    {

        private SLDebugHelper() { }

        /// <summary>
        /// 写日志 log 类型的
        /// </summary>
        /// <param name="msg">输出日志</param>
        public static void Write(object msg)
        {
            if (!SLConfig.IsDebugMode) return;
            SLConsole.Write(msg);
            if (SLConfig.IsWriteLogToFile) SLLogFile.Write(msg);
            if (SLConfig.IsWriteLogToGui) SLLogGUI.Write(msg);
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void Write(string msg, params object[] args)
        {
            if (!SLConfig.IsDebugMode) return;
            SLConsole.Write(msg, args);
            if (SLConfig.IsWriteLogToFile) SLLogFile.Write(msg, args);
            if (SLConfig.IsWriteLogToGui) SLLogGUI.Write(msg, args);
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="args"></param>
        public static void Write(params object[] args)
        {
            if (!SLConfig.IsDebugMode) return;
            SLConsole.Write(args);
            if (SLConfig.IsWriteLogToFile) SLLogFile.Write(args);
            if (SLConfig.IsWriteLogToGui) SLLogGUI.Write(args);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteError(object msg)
        {
            if (!SLConfig.IsDebugMode) return;
            SLConsole.WriteError(msg);
            if (SLConfig.IsWriteLogToFile) SLLogFile.WriteError(msg);
            if (SLConfig.IsWriteLogToGui) SLLogGUI.WriteError(msg);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void WriteError(string msg, params object[] args)
        {
            if (!SLConfig.IsDebugMode) return;
            SLConsole.WriteError(msg, args);
            if (SLConfig.IsWriteLogToFile) SLLogFile.WriteError(msg, args);
            if (SLConfig.IsWriteLogToGui) SLLogGUI.WriteError(msg, args);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="args"></param>
        public static void WriteError(params object[] args)
        {
            if (!SLConfig.IsDebugMode) return;
            SLConsole.WriteError(args);
            if (SLConfig.IsWriteLogToFile) SLLogFile.WriteError(args);
            if (SLConfig.IsWriteLogToGui) SLLogGUI.WriteError(args);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteWarning(object msg)
        {
            if (!SLConfig.IsDebugMode) return;
            SLConsole.WriteWarning(msg);
            if (SLConfig.IsWriteLogToFile) SLLogFile.WriteWarning(msg);
            if (SLConfig.IsWriteLogToGui) SLLogGUI.WriteWarning(msg);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void WriteWarning(string msg, params object[] args)
        {
            if (!SLConfig.IsDebugMode) return;
            SLConsole.WriteWarning(msg, args);
            if (SLConfig.IsWriteLogToFile) SLLogFile.WriteWarning(msg, args);
            if (SLConfig.IsWriteLogToGui) SLLogGUI.WriteWarning(msg, args);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="args"></param>
        public static void WriteWarning(params object[] args)
        {
            if (!SLConfig.IsDebugMode) return;
            SLConsole.WriteWarning(args);
            if (SLConfig.IsWriteLogToFile) SLLogFile.WriteWarning(args);
            if (SLConfig.IsWriteLogToGui) SLLogGUI.WriteWarning(args);
        }

    }

}



