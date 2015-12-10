using System;
using System.Collections.Generic;
using LGame.LBehaviour;
using LGame.LCommon;
using UnityEngine;

namespace LGame.LDebug
{

    /***
     * 
     * 使用 gui 显示 debug
     * 
     * 最大显示 20 条
     * 
     */

    public class LCLogGUI : LABehaviour, LIDeBug
    {
        /// <summary>
        /// 输出日志列表
        /// </summary>
        private static List<GuiLogEntity> _guiLogs = new List<GuiLogEntity>();

        /// <summary>
        /// 当前类的实例
        /// 
        /// 实现单例模式
        /// </summary>
        private static LCLogGUI _instance = null;

        /// <summary>
        /// 用于线程锁
        /// </summary>
        private static object _lock = new object();

        /// <summary>
        /// 创建 gui 日志输出实例
        /// </summary>
        public static LCLogGUI Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        GameObject create = LCSCompHelper.Create("_GUI Debug");
                        _instance = LCSCompHelper.FindComponet<LCLogGUI>(create);
                        DontDestroyOnLoad(create);
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 增加输出日志
        /// </summary>
        /// <param name="log">输出的日志</param>
        /// <param name="color">日志颜色</param>
        private void Write(string log, Color color)
        {
            if (!LCSConfig.IsDebugMode || string.IsNullOrEmpty(log)) return;
            if (_guiLogs.Count >= 20) _guiLogs.RemoveAt(0);
            _guiLogs.Add(new GuiLogEntity
            {
                LogString = log,
                LogColor = color
            });
        }

        /// <summary>
        /// 写日志 log 类型的
        /// </summary>
        /// <param name="msg">输出日志</param>
        public void Write(object msg)
        {
            Write(msg.ToString(), Color.white);
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Write(string msg, params object[] args)
        {
            Write(string.Format(msg, args), Color.white);
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="args"></param>
        public void Write(params object[] args)
        {
            if (!LCSConfig.IsDebugMode) return;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < args.Length; ++i)
            {
                sb.Append(args[i]);
                sb.Append(", ");
            }
            Write(sb.ToString(), Color.white);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="msg"></param>
        public void WriteError(object msg)
        {
            Write(msg.ToString(), Color.red);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void WriteError(string msg, params object[] args)
        {
            Write(string.Format(msg, args), Color.red);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="args"></param>
        public void WriteError(params object[] args)
        {
            if (!LCSConfig.IsDebugMode) return;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < args.Length; ++i)
            {
                sb.Append(args[i]);
                sb.Append(", ");
            }
            Write(sb.ToString(), Color.red);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="msg"></param>
        public void WriteWarning(object msg)
        {
            Write(msg.ToString(), Color.yellow);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void WriteWarning(string msg, params object[] args)
        {
            Write(string.Format(msg, args), Color.yellow);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="args"></param>
        public void WriteWarning(params object[] args)
        {
            if (!LCSConfig.IsDebugMode) return;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < args.Length; ++i)
            {
                sb.Append(args[i]);
                sb.Append(", ");
            }
            Write(sb.ToString(), Color.yellow);
        }

        /// <summary>
        /// 清除当前界面的数据
        /// </summary>
        public override void OnClear()
        {
            _instance = null;
            if (_guiLogs != null) _guiLogs.Clear();
        }

        /// <summary>
        /// 在界面显示 debug 日志
        /// </summary>
        public override void OnGUI()
        {
            if (!LCSConfig.IsDebugMode) return;
            Rect rect = new Rect(5f, 5f, 1000f, 18f);

            for (int i = 0, imax = _guiLogs.Count; i < imax; ++i)
            {
                GuiLogEntity entity = _guiLogs[i];
                GUI.color = Color.black;
                GUI.Label(rect, entity.LogString);
                rect.y -= 1f;
                rect.x -= 1f;
                GUI.color = entity.LogColor;
                GUI.Label(rect, entity.LogString);
                rect.y += 18f;
                rect.x += 1f;
            }
        }

    }

}

