using System;
using System.Collections.Generic;
using LGame.LBehaviour;
using LGame.LCommon;
using UnityEngine;


namespace LGame.LUI
{

    /***
     * 
     * 
     * ui 界面响应事件行为组件
     *  
     * 如果是 c# 界面则直接继承它
     * 如果是lua 界面 则需要继承 LCLuaPage
     * 
     * 
     */

    public class ALUIBehaviour : ALBehaviour, LIUIBehaviour
    {

        /// <summary>
        /// 界面上所以的panel
        /// </summary>
        [NonSerialized]
        private UIPanel[] mPanels;

        /// <summary>
        /// 界面上所有的 Collider
        /// </summary>
        [NonSerialized]
        private Collider[] mBoxColliders;

        /// <summary>
        /// 收集界面上所有的 UIEventTrigger  事件
        /// </summary>
        [NonSerialized]
        private UIEventTrigger[] mEventTrigger;

        /// <summary>
        /// 界面 ui
        /// </summary>
        [NonSerialized]
        private string mWinName = string.Empty;

        /// <summary>
        /// 界面的深度
        /// </summary>
        [NonSerialized]
        private int mWinDepth = 0;

        /// <summary>
        /// 窗口深度
        /// </summary>
        public int WinDepth
        {
            get
            {
                return mWinDepth;
            }
        }

        /// <summary>
        /// 窗口的名字
        /// </summary>
        public string WinName
        {
            get
            {
                return mWinName;
            }
        }

        private new void Awake()
        {
            mPanels = SLCompHelper.GetComponents<UIPanel>(gameObject);
            mBoxColliders = SLCompHelper.GetComponents<Collider>(gameObject);
            mEventTrigger = SLCompHelper.GetComponents<UIEventTrigger>(gameObject);

            if (mEventTrigger != null && mEventTrigger.Length > 0)
            {
                for (int i = 0, len = mEventTrigger.Length; i < len; i++)
                {
                    UIEventTrigger trigger = mEventTrigger[i];
                    trigger.onClick.Add(new EventDelegate(OnClick));
                    trigger.onDoubleClick.Add(new EventDelegate(OnDoubleClick));
                    trigger.onPress.Add(new EventDelegate(OnPress));
                    trigger.onRelease.Add(new EventDelegate(OnRelease));
                }
            }

            OnAwake();
        }

        private new void Start()
        {
            OnStart();
        }

        /// <summary>
        /// 打开界面
        /// 
        /// 打开并创建界面
        /// </summary>
        public void OnOpen() { }

        /// <summary>
        /// 打开一个具有深度的窗口
        /// </summary>
        /// <param name="depth"></param>
        public void OnOpen(int depth)
        {
            mWinDepth = depth;
        }

        /// <summary>
        /// 打开界面
        /// </summary>
        /// <param name="depth">界面深度</param>
        /// <param name="winName">界面的名字</param>
        public void OnOpen(int depth, string winName)
        {
            mWinDepth = depth;
            mWinName = winName;
            if (mPanels == null) return;
            for (int i = 0, len = mPanels.Length; i < len; i++)
                mPanels[i].depth = depth + i;
        }

        /// <summary>
        ///  界面获得焦点
        /// </summary>
        public void OnFocus()
        {
            if (mBoxColliders == null) return;
            SLCompHelper.CollidersEnabled(mBoxColliders);
        }

        /// <summary>
        /// 界面失去焦点
        /// </summary>
        public void OnLostFocus()
        {
            if (mBoxColliders == null) return;
            SLCompHelper.CollidersEnabled(mBoxColliders, false);
        }

        /// <summary>
        /// 单击事件
        /// 
        /// 之类不应该继承它，而应该继承 OnCollider
        /// 
        /// 而且外部不应该调用这个函数
        /// 
        /// </summary>
        public void OnClick()
        {
            Collider click = UICamera.lastHit.collider;
            if (click == null) return;
            OnCollider(click.gameObject);
        }

        /// <summary>
        /// 双击事件
        /// 
        /// 之类不应该继承它，而应该继承 OnDoubleCollider
        /// 
        /// 而且外部不应该调用这个函数
        /// 
        /// </summary>
        public void OnDoubleClick()
        {
            Collider dclick = UICamera.lastHit.collider;
            if (dclick == null) return;
            OnDoubleCollider(dclick.gameObject);
        }

        /// <summary>
        /// 按下一定时间
        /// </summary>
        public void OnPress()
        {
            Collider press = UICamera.lastHit.collider;
            if (press == null) return;
            OnPressCollider(press.gameObject);
        }

        /// <summary>
        /// 鼠标按下一定时间后抬起
        /// </summary>
        public void OnRelease()
        {
            Collider release = UICamera.lastHit.collider;
            if (release == null) return;
            OnReleaseCollider(release.gameObject);
        }

        /// <summary>
        /// 显示
        /// </summary>
        public void OnShow()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        public void OnHide()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// 子类继承，并实现 awake
        /// </summary>
        public virtual void OnAwake()
        {
        }

        /// <summary>
        /// 子类继承，并实现 Start
        /// </summary>
        public virtual void OnStart()
        {
        }

        /// <summary>
        /// 刷新面板
        /// </summary>
        public virtual void OnRefresh()
        {

        }

        /// <summary>
        /// 子类继承该函数
        ///     
        /// 点击函数
        /// </summary>
        /// <param name="btn">当前点击 Collider </param>
        public virtual void OnCollider(GameObject btn)
        {

        }

        /// <summary>
        /// 子类继承该函数
        /// 
        /// 双击函数
        /// </summary>
        /// <param name="btn">当前双击 Collider</param>
        public virtual void OnDoubleCollider(GameObject btn)
        {

        }

        /// <summary>
        /// 子类继承该函数
        /// 
        /// 按下时候的操作
        /// </summary>
        /// <param name="btn">按下时操作的 Collider</param>
        public virtual void OnPressCollider(GameObject btn)
        {

        }

        /// <summary>
        /// 子类继承该函数
        /// 
        /// 抬起鼠标或者手指的操作
        /// </summary>
        /// <param name="btn">抬起的 Collider </param>
        public virtual void OnReleaseCollider(GameObject btn)
        {

        }

        /// <summary>
        /// 清理界面数据
        /// 
        /// 界面关闭时清理数据
        /// 
        /// </summary>
        public virtual new void OnClear()
        {

        }

        /// <summary>
        /// 关闭界面
        /// 
        /// 关闭并销毁该界面
        /// 
        /// 如果该类被重写，需要调用base该方法
        /// </summary>
        public void OnClose()
        {
            mPanels = null;
            mBoxColliders = null;
            mEventTrigger = null;
            SLUIManage.CloseWindow(this);
        }

    }

}

