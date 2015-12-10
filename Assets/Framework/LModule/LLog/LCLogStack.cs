using LGame.LBehaviour;
using LGame.LCommon;
using UnityEngine;

namespace LGame.LDebug
{

    /*****
     * 
     * 
     *  获得日志堆栈
     * 
     */

    public class LCLogStack : LABehaviour
    {

        private static LCLogStack _instance = null;

        /// <summary>
        /// 日志堆栈回调函数
        /// 
        /// 不能在输出 log 日志
        /// </summary>
        /// <param name="logString">输入的日志</param>
        /// <param name="stackTrace">堆栈数据</param>
        /// <param name="type">日志类型</param>
        private void LogCallback(string logString, string stackTrace, UnityEngine.LogType type)
        {
            switch (type)
            {
                case UnityEngine.LogType.Log:
                    if (LCSConfig.IsAutoWriteToFile)
                    {
                        LCSLogFile.Write(logString);
                        LCSLogFile.Write(stackTrace);
                    }
                    if (LCSConfig.IsAutoWriteToGui)
                    {
                        LCSLogGUI.Write(logString);
                        LCSLogGUI.Write(stackTrace);
                    }
                    break;
                case UnityEngine.LogType.Warning:
                    if (LCSConfig.IsAutoWriteToFile)
                    {
                        LCSLogFile.WriteWarning(logString);
                        LCSLogFile.WriteWarning(stackTrace);
                    }
                    if (LCSConfig.IsAutoWriteToGui)
                    {
                        LCSLogGUI.WriteWarning(logString);
                        LCSLogGUI.WriteWarning(stackTrace);
                    }
                    break;
                case UnityEngine.LogType.Error:
                    if (LCSConfig.IsAutoWriteToFile)
                    {
                        LCSLogFile.WriteError(logString);
                        LCSLogFile.WriteError(stackTrace);
                    }
                    if (LCSConfig.IsAutoWriteToGui)
                    {
                        LCSLogGUI.WriteError(logString);
                        LCSLogGUI.WriteError(stackTrace);
                    }
                    break;
            }
        }

        /// <summary>
        /// 开始堆栈
        /// </summary>
        /// <returns></returns>
        public static LCLogStack Begin()
        {
            // todo: 增加启动 项目开始时设置
            if (_instance != null) return _instance;
            GameObject create = LCSCompHelper.Create("_LOG Stack");
            _instance = LCSCompHelper.FindComponet<LCLogStack>(create);
            Application.RegisterLogCallback(_instance.LogCallback);
            return _instance;
        }

        /// <summary>
        /// 清除数据
        /// </summary>
        public override void OnClear()
        {
            _instance = null;
            Application.RegisterLogCallback(null);
            if (LCSConfig.IsAutoWriteToFile) LCSLogFile.Clear();
        }

    }

}
