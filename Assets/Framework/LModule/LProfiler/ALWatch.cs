using System;
using System.Collections.Generic;
using LGame.LCommon;

namespace LGame.LProfiler
{


    /***
     * 
     * 抽象性能检测类数据处理
     * 
     */

    public abstract class ALWatch
    {

        /// <summary>
        /// 保存当前观察时间的实体
        /// 
        /// 可保存多个
        /// </summary>
        private static Dictionary<string, RecordWatchEntity> _recordEnitys = new Dictionary<string, RecordWatchEntity>();

        /// <summary>
        /// 当前观察数据
        /// </summary>
        private RecordWatchEntity _currentWatch;

        /// <summary>
        /// 增加一个观察
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void AddWatch(RecordWatchEntity entity)
        {
            if (entity == null || string.IsNullOrEmpty(entity.WatchKey)) return;
            if (_recordEnitys.ContainsKey(entity.WatchKey))
                _recordEnitys[entity.WatchKey] = entity;
            else
                _recordEnitys.Add(entity.WatchKey, entity);
            CurrentWatch = entity;
        }

        /// <summary>
        /// 发现一个观察
        /// </summary>
        /// <param name="watchKey"></param>
        /// <returns></returns>
        protected virtual RecordWatchEntity FindWatch(string watchKey)
        {
            if (string.IsNullOrEmpty(watchKey)) return null;
            RecordWatchEntity entity;
            return _recordEnitys.TryGetValue(watchKey, out entity) ? entity : null;
        }

        /// <summary>
        /// 移出一个观察
        /// </summary>
        /// <param name="watchKey"></param>
        protected virtual void RemoveWatch(string watchKey)
        {
            if (string.IsNullOrEmpty(watchKey) || !_recordEnitys.ContainsKey(watchKey)) return;
            _recordEnitys.Remove(watchKey);
            CurrentWatch = null;
        }

        /// <summary>
        /// 移出一个观察
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void RemoveWatch(RecordWatchEntity entity)
        {
            if (entity == null || string.IsNullOrEmpty(entity.WatchKey)) return;
            RemoveWatch(entity.WatchKey);
        }

        /// <summary>
        /// 清除所有的观察
        /// </summary>
        protected virtual void ClearAllWatch()
        {
            _recordEnitys.Clear();
        }

        /// <summary>
        /// 得到当前的观察数据
        /// </summary>
        public RecordWatchEntity CurrentWatch
        {
            private set
            {
                _currentWatch = value;
            }
            get
            {
                return _currentWatch;
            }
        }

    }

}
