using System;
using LGame.LCommon;
using LGame.LDebug;
using UnityEngine;

namespace LGame.LProfiler
{


    /****
     * 
     * 
     * 使用 untiy 运行时间分析性能
     * 
     * 
     */

    public class LCRecordTime : LAWatch, LIWatch
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
        /// <param name="key">指定key值</param>
        public void StartWatch(string key)
        {
            RecordWatchEntity entity = CurrentWatch;
            if (entity != null && entity.IsWatch)
            {
                LCSConsole.WriteError("已经开始观察性能......");
                return;
            }

            if (entity == null) entity = new RecordWatchEntity { WatchKey = string.IsNullOrEmpty(key) ? LCSGuid.NewUpperGuid() : key };
            entity.IsWatch = true;
            entity.StartTime = Time.realtimeSinceStartup;

            // 记录 自游戏开始的实时时间
            AddWatch(entity);
        }

        /// <summary>
        /// 结束观察
        /// </summary>
        /// <returns></returns>
        public double EndWatch()
        {
            RecordWatchEntity entity = CurrentWatch;
            if (entity == null)
            {
                LCSConsole.WriteError("观察还未开始...");
                return 0;
            }
            entity.IsWatch = false;
            entity.EndTime = Time.realtimeSinceStartup;
            LCSConsole.Write(string.Format(" [RecordTime] {0} use {1}s.", entity.WatchKey, entity.DiffTime));
            return entity.DiffTime;
        }

        /// <summary>
        /// 得到观察的时间
        /// </summary>
        /// <returns></returns>
        public double GetWatchTime()
        {
            RecordWatchEntity entity = CurrentWatch;
            if (entity == null)
            {
                LCSConsole.WriteError("观察还未开始...");
                return 0;
            }
            if (entity.IsWatch)
            {
                LCSConsole.WriteError("观察还未结束...");
                return 0;
            }
            return entity.DiffTime;
        }

        /// <summary>
        /// 清理观察
        /// </summary>
        public void ClearWatch()
        {
            RecordWatchEntity entity = CurrentWatch;
            if (entity == null) return;
            RemoveWatch(entity);
        }

    }

}

