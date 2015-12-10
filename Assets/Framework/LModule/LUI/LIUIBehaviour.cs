using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LGame.LUI
{

    /****
     * 
     * 
     * ui 界面接口类
     * 
     */

    interface LIUIBehaviour
    {

        /// <summary>
        /// 打开界面
        /// 
        /// 打开并创建界面
        /// </summary>
        void OnOpen();

        /// <summary>
        /// 打开界面
        /// </summary>
        /// <param name="depth">界面深度</param>
        void OnOpen(int depth);

        /// <summary>
        /// 打开界面
        /// </summary>
        /// <param name="depth">界面深度</param>
        /// <param name="winName">界面的名字</param>
        void OnOpen(int depth, string winName);

        /// <summary>
        /// 关闭界面
        /// 
        /// 关闭并销毁该界面
        /// </summary>
        void OnClose();

        /// <summary>
        ///  界面获得焦点
        /// </summary>
        void OnFocus();

        /// <summary>
        /// 界面失去焦点
        /// </summary>
        void OnLostFocus();

        /// <summary>
        /// 单击事件
        /// </summary>
        void OnClick();

        /// <summary>
        /// 双击事件
        /// </summary>
        void OnDoubleClick();

        /// <summary>
        /// 按下一定时间
        /// </summary>
        void OnPress();

        /// <summary>
        /// 鼠标按下一定时间后抬起
        /// </summary>
        void OnRelease();

        /// <summary>
        /// 显示
        /// </summary>
        void OnShow();

        /// <summary>
        /// 隐藏
        /// </summary>
        void OnHide();

    }

}
