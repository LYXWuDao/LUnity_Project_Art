using System.Collections;
using LGame.LBehaviour;
using UnityEngine;

/****
 * 
 * 
 * 
 * 鼠标拖动物体移动事件组件
 * 
 * 
 *  响应鼠标左键
 * 
 */

namespace Game.LUtils
{

    public class LCDragEvent : LAMouseBehaviour
    {

        /// <summary>
        /// 鼠标发出的射线
        /// </summary>
        public Transform MouseRayTransform
        {
            get
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);
                return hit.transform;
            }
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        protected override void OnMouseLeftDown()
        {
            MouseMove(MouseRayTransform);
        }

        /// <summary>
        /// 鼠标抬起
        /// </summary>
        protected override void OnMouseLeftUp()
        {

        }

        /// <summary>
        /// 移动物体
        /// </summary>
        /// <param name="moveTrans"></param>
        protected virtual void MouseMove(Transform moveTrans)
        {
            StartCoroutine(MouseDragDrop(moveTrans));
        }

        /// <summary>
        /// 鼠标拖拽移动
        /// </summary>
        protected virtual IEnumerator MouseDragDrop(Transform moveTrans)
        {
            if (moveTrans == null) yield return null;

            float targetY = moveTrans.position.y;

            // 把目标物体的世界空间坐标转换到它自身的屏幕空间坐标   
            Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(moveTrans.position);

            // 存储鼠标的屏幕空间坐标（Z值使用目标物体的屏幕空间坐标）   
            Vector3 mouseScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetScreenPos.z);

            // 计算目标物体与鼠标物体在世界空间中的偏移量 
            Vector3 vec3Offset = moveTrans.position - Camera.main.ScreenToWorldPoint(mouseScreenPos);

            // 鼠标左键按下
            while (Input.GetMouseButton(0))
            {

                targetScreenPos = Camera.main.WorldToScreenPoint(moveTrans.position);

                // 存储鼠标的屏幕空间坐标（Z值使用目标物体的屏幕空间坐标）
                mouseScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetScreenPos.z);

                // 把鼠标的屏幕空间坐标转换到世界空间坐标（Z值使用目标物体的屏幕空间坐标），加上偏移量，以此作为目标物体的世界空间坐标  
                moveTrans.position = Camera.main.ScreenToWorldPoint(mouseScreenPos) + vec3Offset;
                moveTrans.position = new Vector3(moveTrans.position.x, targetY, moveTrans.position.z);

                // 等待固定更新 
                yield return new WaitForFixedUpdate();
            }
        }

    }

}

