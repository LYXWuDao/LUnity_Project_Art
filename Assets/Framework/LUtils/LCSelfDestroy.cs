using LGame.LBehaviour;
using LGame.LCommon;
using UnityEngine;
using System.Collections;

/*****
 * 
 * 
 * 自我毁坏
 * 
 */

namespace Game.LUtils
{

    public class LCSelfDestroy : LABehaviour
    {

        /// <summary>
        /// 自我销毁时间
        /// </summary>
        public float mDtyTime = 0f;

        /// <summary>
        /// 增加一个销毁脚本
        /// 
        /// 开启一个销毁
        /// </summary>
        /// <returns></returns>
        public static LCSelfDestroy Begin(GameObject go, float dtyTime)
        {
            LCSelfDestroy dest = LCSCompHelper.FindComponet<LCSelfDestroy>(go);
            dest.mDtyTime = dtyTime;
            return dest;
        }

        public override void OnUpdate(float deltaTime)
        {
            if (mDtyTime > 0)
            {
                mDtyTime -= deltaTime;
                return;
            }
            GameObject.Destroy(gameObject);
        }

    }
}
