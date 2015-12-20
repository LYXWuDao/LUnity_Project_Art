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

    public class CLLogStack : ALBehaviour
    {

        private static CLLogStack _instance = null;

        /// <summary>
        /// 日志堆栈回调函数
        /// 
        /// 不能在输出 log 日志
        /// </summary>
        /// <param name="logString">输入的日志</param>
        /// <param name="stackTrace">堆栈数据</param>
        /// <param name="type">日志类型</param>
        private void LogCallback(string logString, string stackTrace, LogType type)
        {
            switch (type)
            {
                case UnityEngine.LogType.Log:
                    SLDebugHelper.Write(logString);
                    SLDebugHelper.Write(stackTrace);
                    break;
                case UnityEngine.LogType.Warning:
                    SLDebugHelper.WriteWarning(logString);
                    SLDebugHelper.WriteWarning(stackTrace);
                    break;
                case UnityEngine.LogType.Error:
                    SLDebugHelper.WriteError(logString);
                    SLDebugHelper.WriteError(stackTrace);
                    break;
            }
        }

        /// <summary>
        /// 开始堆栈
        /// </summary>
        /// <returns></returns>
        public static CLLogStack Begin()
        {
            // todo: 增加启动 项目开始时设置
            if (_instance != null) return _instance;
            GameObject create = SLCompHelper.Create("_LOG Stack");
            DontDestroyOnLoad(create);
            _instance = SLCompHelper.FindComponet<CLLogStack>(create);
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
            if (SLConfig.IsWriteLogToFile) SLLogFile.Clear();
        }

    }

}
