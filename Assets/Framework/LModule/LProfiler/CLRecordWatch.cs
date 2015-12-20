using System;
using LGame.LCommon;
using LGame.LDebug;
using UnityEngine;

namespace LGame.LProfiler
{

    /***
     * 
     * 
     * 添加性能观察, 使用C#内置
     * 
     */

    public class CLRecordWatch : ALWatch, ILWatch
    {

        /// <summary>
        /// 开始观察
        /// </summary>
        public void StartWatch()
        {
            StartWatch(string.Empty);
        }

        /// <summary>
        /// 开始观察
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void StartWatch(string key)
        {
            RecordWatchEntity entity = CurrentWatch;
            if (entity != null && entity.IsWatch)
            {
                SLDebugHelper.WriteError("已经开始观察性能......");
                return;
            }
            if (entity == null) entity = new RecordWatchEntity { WatchKey = string.IsNullOrEmpty(key) ? SLGuid.NewUpperGuid() : key };
            entity.BeginWatch();
            AddWatch(entity);
        }

        /// <summary>
        /// 结束观察
        /// </summary>
        public double EndWatch()
        {
            RecordWatchEntity entity = CurrentWatch;
            if (entity == null)
            {
                SLDebugHelper.WriteError("观察还未开始...");
                return 0;
            }
            entity.EndWatch();
            double watchTime = entity.GetWatchTime();
            // 7位精度
            SLDebugHelper.WriteWarning("[Watch] {0} use time : {1}s.", entity.WatchKey, watchTime.ToString("F7"));
            return watchTime;
        }

        /// <summary>
        /// 得到当前的观察时间
        /// </summary>
        /// <returns></returns>
        public double GetWatchTime()
        {
            RecordWatchEntity entity = CurrentWatch;
            if (entity == null)
            {
                SLDebugHelper.WriteError("观察还未开始...");
                return 0;
            }
            if (entity.IsWatch)
            {
                SLDebugHelper.WriteError("观察还未结束...");
                return 0;
            }
            return entity.GetWatchTime();
        }

        /// <summary>
        /// 清除观察
        /// </summary>
        public void ClearWatch()
        {
            RecordWatchEntity entity = CurrentWatch;
            if (entity == null) return;
            RemoveWatch(entity);
        }

    }

}
