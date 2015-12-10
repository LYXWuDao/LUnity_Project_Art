using System;
using System.Collections.Generic;
using LGame.LBehaviour;
using LGame.LCommon;
using UnityEngine;

namespace Game.LUtils
{

    public class LCDelayAction : LABehaviour
    {

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

        public static LCDelayAction BeginAction(GameObject go, float delayTime, Action action)
        {
            if (go == null && action == null) return null;
            if (go == null || delayTime <= 0)
            {
                action();
                return null;
            }
            LCDelayAction delact = LCSCompHelper.FindComponet<LCDelayAction>(go);
            delact.mActionBack = action;
            delact.mActionTime = delayTime;
            return delact;
        }

        /// <summary>
        /// 移出当前脚本
        /// </summary>
        public void RemoveAction()
        {
            Destroy(this);
        }

    }

}