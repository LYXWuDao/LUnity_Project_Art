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

    public abstract class LAUIBehaviour : LABehaviour, LIUIBehaviour
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

        public virtual new void Awake()
        {
            mPanels = LCSCompHelper.GetComponents<UIPanel>(gameObject);
            mBoxColliders = LCSCompHelper.GetComponents<Collider>(gameObject);
        }

        /// <summary>
        /// 打开界面
        /// 
        /// 打开并创建界面
        /// </summary>
        public virtual void OnOpen() { }

        /// <summary>
        /// 打开一个具有深度的窗口
        /// </summary>
        /// <param name="depth"></param>
        public virtual void OnOpen(int depth)
        {
            mWinDepth = depth;
        }

        /// <summary>
        /// 打开界面
        /// </summary>
        /// <param name="depth">界面深度</param>
        /// <param name="winName">界面的名字</param>
        public virtual void OnOpen(int depth, string winName)
        {
            mWinDepth = depth;
            mWinName = winName;
            if (mPanels == null) return;
            for (int i = 0, len = mPanels.Length; i < len; i++)
                mPanels[i].depth = depth + i;
        }

        /// <summary>
        /// 清理界面数据
        /// </summary>
        public override void OnClear()
        {
            mPanels = null;
            mBoxColliders = null;
        }

        /// <summary>
        /// 关闭界面
        /// 
        /// 关闭并销毁该界面
        /// 
        /// 如果该类被重写，需要调用base该方法
        /// </summary>
        public virtual void OnClose()
        {
            LCSUIManage.CloseWindow(this);
        }

        /// <summary>
        ///  界面获得焦点
        /// </summary>
        public virtual void OnFocus()
        {
            if (mBoxColliders == null) return;
            LCSCompHelper.CollidersEnabled(mBoxColliders);
        }

        /// <summary>
        /// 界面失去焦点
        /// </summary>
        public virtual void OnLostFocus()
        {
            if (mBoxColliders == null) return;
            LCSCompHelper.CollidersEnabled(mBoxColliders, false);
        }

        /// <summary>
        /// 单击事件
        /// </summary>
        public virtual void OnClick() { }

        /// <summary>
        /// 双击事件
        /// </summary>
        public virtual void OnDoubleClick() { }

        /// <summary>
        /// 按下一定时间
        /// </summary>
        public virtual void OnPress() { }

        /// <summary>
        /// 鼠标按下一定时间后抬起
        /// </summary>
        public virtual void OnRelease() { }

        /// <summary>
        /// 显示
        /// </summary>
        public virtual void OnShow()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        public virtual void OnHide()
        {
            gameObject.SetActive(false);
        }

    }

}

