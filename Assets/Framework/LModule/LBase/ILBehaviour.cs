using System;
using System.Collections.Generic;
using UnityEngine;

namespace LGame.LBehaviour
{

    /****
     * 
     * 框架基础行为接口
     * 
     */

    public interface ILBehaviour
    {

        /// <summary>
        /// 对象创建后调用
        /// </summary>
        void Awake();

        /// <summary>
        /// 获得焦点
        /// 
        /// 只在每次激活GameObject的时候执行一次
        /// 例如：gameobject.SetActive(true);
        /// 
        /// </summary>
        void OnEnable();

        void FixedUpdate();

        void OnFixedUpdate(float fixedTime);

        /// <summary>
        /// 在update第一帧调用前调用
        /// </summary>
        void Start();

        /// <summary>
        /// 每一帧调用
        /// </summary>
        void Update();

        void OnUpdate(float deltaTime);

        void LateUpdate();

        void OnLateUpdate(float deltaTime);

        /// <summary>
        /// unity 界面绘制调用
        /// </summary>
        void OnGUI();

        /// <summary>
        /// 项目暂停时调用
        /// </summary>
        void OnApplicationPause();

        /// <summary>
        /// 对象失去焦点时调用
        /// 
        /// 只在每次失去焦点,GameObject的时候执行一次
        /// 例如：gameobject.SetActive(false);
        /// 
        /// </summary>
        void OnDisable();

        /// <summary>
        /// 被销毁的时候清理数据
        /// </summary>
        void OnClear();

        /// <summary>
        /// 主动调用销毁
        /// </summary>
        void Destroy();

        /// <summary>
        /// unity 自动调用
        /// 
        /// 对象被销毁时调用
        /// </summary>
        void OnDestroy();

        /// <summary>
        /// 项目退出时调用
        /// </summary>
        void OnApplicationQuit();

    }

}
