using System;
using System.Collections.Generic;
using LGame.LBehaviour;

namespace LGame.LScenes
{

    /****
     * 
     * 
     * 场景 响应事件行为组件
     * 
     * 如果是 c# 界面则直接继承它
     * 如果是lua 则需要继承 CLSceneLuaBehaviour
     * 
     * 每个场景都会有一个 SceneRoot  节点，挂载这个组件
     * 
     */

    public class CLSceneBehaviour : ALBehaviour
    {

        /// <summary>
        /// 进入场景
        /// </summary>
        public virtual void OnEnterScene()
        {

        }

        /// <summary>
        /// 离开场景
        /// </summary>
        public virtual void OnLeaveScene()
        {

        }

    }

}

