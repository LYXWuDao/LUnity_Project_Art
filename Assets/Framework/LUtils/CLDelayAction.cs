using System;
using System.Collections.Generic;
using LGame.LBehaviour;
using LGame.LCommon;
using UnityEngine;

namespace LGame.LUtils
{

    /******
     * 
     * 
     * 间隔多少时间
     * 
     */

    public class CLDelayAction : ALBehaviour
    {
        /// <summary>
        /// 创建时间间隔事件
        /// </summary>
        private static CLDelayAction Instance
        {
            get
            {
                GameObject go = SLCompHelper.Create("_delay action");
                return SLCompHelper.FindComponet<CLDelayAction>(go);
            }
        }

        /// <summary>
        /// 当前回调 action
        /// </summary>
        private Action mActionBack = null;

        /// <summary>
        /// 调用 action 间隔时间
        /// </summary>
        private float mActionTime = 0;

        public override void OnUpdate(float deltaTime)
        {
            if (mActionBack == null) return;
            if (mActionTime > 0)
            {
                mActionTime -= deltaTime;
                return;
            }
            mActionBack();
            mActionBack = null;
            RemoveAction();
        }

        /// <summary>
        /// 间隔多久后回调
        /// </summary>
        /// <param name="delayTime">间隔时间</param>
        /// <param name="action">回调函数</param>
        /// <returns></returns>
        public static CLDelayAction BeginAction(float delayTime, Action action)
        {
            if (action == null) return null;
            if (delayTime <= 0)
            {
                action();
                return null;
            }
            CLDelayAction delact = Instance;
            delact.mActionBack = action;
            delact.mActionTime = delayTime;
            return delact;
        }

        /// <summary>
        /// 移出当前脚本
        /// </summary>
        public void RemoveAction()
        {
            Destroy();
        }

    }

}