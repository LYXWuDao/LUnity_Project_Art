using LGame.LBehaviour;
using UnityEngine;
using System.Collections;


/****
 * 
 * 
 * 将 ui 嵌入 3d 场景内
 * 
 * 如：战斗 buff  伤害数值
 * 
 */

namespace Game.LUtils
{

    public class LCUIInset3D : LABehaviour
    {

        /// <summary>
        /// 3d 摄像机
        /// </summary>
        public Camera mainCamera;

        /// <summary>
        /// 看向的目标
        /// </summary>
        public GameObject mTarget;

        public Vector3 mOffset;

        /// <summary>
        /// 保存当前目标的位置
        /// </summary>
        [System.NonSerialized]
        private Vector3 mSaveTagPos;

        public override void Start()
        {
            if (mainCamera == null) mainCamera = Camera.main;
            mSaveTagPos = Vector3.one * -1000;
        }

        public override void OnUpdate(float deltaTime)
        {
            if (mTarget == null) return;
            Vector3 tpos = mTarget.transform.position;
            if (mSaveTagPos == tpos) return;
            mSaveTagPos = tpos;
            // 将 2d ui 设置到3d 目标位置
            transform.position = mSaveTagPos;
            transform.localPosition = transform.localPosition + mOffset;
        }

    }

}