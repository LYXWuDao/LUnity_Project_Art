using LGame.LBehaviour;
using LGame.LCommon;
using UnityEngine;
using System.Collections;

namespace LGame.LUtils
{


    /*****
     * 
     * 
     * 自我毁坏
     * 
     */


    public class CLSelfDestroy : ALBehaviour
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
        public static CLSelfDestroy Begin(GameObject go, float dtyTime)
        {
            CLSelfDestroy dest = SLCompHelper.FindComponet<CLSelfDestroy>(go);
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
            Destroy();
        }

    }
}
