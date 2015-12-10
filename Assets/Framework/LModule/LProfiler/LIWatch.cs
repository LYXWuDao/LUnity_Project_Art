using System;
using System.Collections.Generic;

namespace LGame.LProfiler
{

    /***
     * 
     * 
     * 性能观察基础类
     * 
     * 
     */

    public interface LIWatch
    {

        /// <summary>
        /// 开始性能观察
        /// </summary>
        void StartWatch();

        /// <summary>
        /// 开始性能观察
        /// </summary>
        void StartWatch(string key);

        /// <summary>
        /// 结束性能观察
        /// </summary>
        double EndWatch();

        /// <summary>
        /// 得到观察的时间
        /// 
        /// 如果已经清除默认返回 0
        /// </summary>
        double GetWatchTime();

        /// <summary>
        /// 清除性能观察
        /// </summary>
        void ClearWatch();

    }

}

