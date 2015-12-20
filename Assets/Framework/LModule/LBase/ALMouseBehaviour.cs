using UnityEngine;

/****
 * 
 *  鼠标响应事件行为组件
 *  
 *  分别响应鼠标左键，中键，右键 按下，抬起 持续事件
 * 
 */

namespace LGame.LBehaviour
{

    public abstract class ALMouseBehaviour : ALBehaviour
    {

        public override void Update()
        {

            if (Input.GetMouseButtonDown(0))
            {
                OnMouseLeftDown();
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnMouseLeftUp();
            }

            if (Input.GetMouseButtonDown(1))
            {
                OnMouseRightDown();
            }

            if (Input.GetMouseButtonUp(1))
            {
                OnMouseRightUp();
            }

            if (Input.GetMouseButtonDown(2))
            {
                OnMouseCenterDown();
            }

            if (Input.GetMouseButtonUp(2))
            {
                OnMouseCenterUp();
            }

            if (Input.GetMouseButton(0))
            {
                OnMouseLeftButton();
            }

            if (Input.GetMouseButton(1))
            {
                OnMouseRightButton();
            }

            if (Input.GetMouseButton(2))
            {
                OnMouseCenterButton();
            }
        }

        /// <summary>
        ///  当鼠标左键按下   
        /// </summary>
        protected virtual void OnMouseLeftDown()
        {

        }

        /// <summary>
        ///  当鼠标左键抬起
        /// </summary>
        protected virtual void OnMouseLeftUp()
        {
        }

        /// <summary>
        /// 鼠标左键保持按下的过程
        /// </summary>
        protected virtual void OnMouseLeftButton()
        {

        }

        /// <summary>
        ///  当鼠标右键按下
        /// </summary>
        protected virtual void OnMouseRightDown()
        {

        }

        /// <summary>
        ///  当鼠标右键抬起
        /// </summary>
        protected virtual void OnMouseRightUp()
        {

        }

        /// <summary>
        /// 鼠标右键保持按下的过程
        /// </summary>
        protected virtual void OnMouseRightButton()
        {

        }

        /// <summary>
        ///  当鼠标中键按下
        /// </summary>
        protected virtual void OnMouseCenterDown()
        {

        }

        /// <summary>
        ///  当鼠标中键抬起
        /// </summary>
        protected virtual void OnMouseCenterUp()
        {

        }

        /// <summary>
        /// 鼠标中键保持按下的过程
        /// </summary>
        protected virtual void OnMouseCenterButton()
        {

        }

    }

}