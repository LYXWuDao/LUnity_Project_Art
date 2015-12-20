
namespace LGame.LDebug
{

    /***
     * 
     *  debug  输出日志接口类
     * 
     * 标准化输出日志函数
     * 
     */

    public interface ILDeBug
    {

        /// <summary>
        /// 写日志 log 类型的
        /// </summary>
        /// <param name="msg">输出日志</param>
        void Write(object msg);

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        void Write(string msg, params object[] args);

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="args"></param>
        void Write(params object[] args);

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="msg"></param>
        void WriteError(object msg);

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        void WriteError(string msg, params object[] args);

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="args"></param>
        void WriteError(params object[] args);

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="msg"></param>
        void WriteWarning(object msg);

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        void WriteWarning(string msg, params object[] args);

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="args"></param>
        void WriteWarning(params object[] args);

    }

}
