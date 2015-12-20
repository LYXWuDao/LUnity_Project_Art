using System;
using System.Collections.Generic;
using UnityEngine;

namespace LGame.LDebug
{

    /****
     * 
     * 
     * 静态界面日志输出类
     * 
     */

    public static class SLLogGUI
    {

        private static CLLogGUI _logGui = null;

        private static object _lock = new object();

        private static CLLogGUI Instance
        {
            get
            {
                if (_logGui != null) return _logGui;
                lock (_lock)
                {
                    if (_logGui == null)
                    {
                        _logGui = CLLogGUI.Instance;
                    }
                }
                return _logGui;
            }
        }

        /// <summary>
        /// 写日志 log 类型的
        /// </summary>
        /// <param name="msg">输出日志</param>
        public static void Write(object msg)
        {
            Instance.Write(msg);
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void Write(string msg, params object[] args)
        {
            Instance.Write(msg, args);
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="args"></param>
        public static void Write(params object[] args)
        {
            Instance.Write(args);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteError(object msg)
        {
            Instance.WriteError(msg);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void WriteError(string msg, params object[] args)
        {
            Instance.WriteError(msg, args);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="args"></param>
        public static void WriteError(params object[] args)
        {
            Instance.WriteError(args);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteWarning(object msg)
        {
            Instance.WriteWarning(msg);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void WriteWarning(string msg, params object[] args)
        {
            Instance.WriteWarning(msg, args);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="args"></param>
        public static void WriteWarning(params object[] args)
        {
            Instance.WriteWarning(args);
        }

        /// <summary>
        /// 清理界面上的日志
        /// </summary>
        public static void Clear()
        {
            Instance.OnClear();
            _logGui = null;
        }

    }

}
