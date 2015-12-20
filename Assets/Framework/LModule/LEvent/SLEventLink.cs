using System;
using System.Collections.Generic;
using System.Threading;

namespace LGame.LEvent
{

    /***
     * 
     * 
     *  事件链处理类
     * 
     *  事件只会执行一次, 但是会等待上个事件完成，才会执行下一个事件
     * 
     * 
     */

    public class SLEventLink
    {

        private SLEventLink() { }

        /// <summary>
        /// 保存当前的事件
        /// </summary>
        private static SLEventLink _eventLink = null;

        /// <summary>
        /// 锁定
        /// </summary>
        private static object _lockObj = new object();

        /// <summary>
        /// 事件执行线程
        /// </summary>
        private static Thread _eventThread = null;

        private static SLEventLink Instance
        {
            get
            {
                if (_eventLink == null)
                {
                    lock (_lockObj)
                    {
                        if (_eventLink == null)
                        {
                            _eventLink = new SLEventLink();
                        }
                    }
                }
                return _eventLink;
            }
        }

        /// <summary>
        /// 开始事件
        /// </summary>
        private void StartEvent()
        {
            if (_eventThread == null) return;
            _eventThread = new Thread(DealEvent);
            _eventThread.Start();
        }

        /// <summary>
        /// 处理事件
        /// </summary>
        private void DealEvent()
        {

        }

        /// <summary>
        /// 增加事件
        /// </summary>
        private void AddEvent()
        {

        }

        /// <summary>
        /// 移出某个事件
        /// </summary>
        private void RemoveEvent()
        {

        }

        /// <summary>
        /// 移出所有的事件
        /// </summary>
        private void RemoveAllEvent()
        {

        }

        /// <summary>
        /// 执行事件
        /// </summary>
        public static void ExecuteEvent()
        {

            Instance.StartEvent();
        }

    }

}

