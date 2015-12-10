using System;
using System.Collections.Generic;
using LGame.LBehaviour;
using UnityEngine;

namespace Game.LUtils
{

    public class LCTweener : LABehaviour
    {
        /// <summary>
        ///  运动方式
        /// </summary>
        public enum RotateType
        {

            /// <summary>
            /// 一次
            /// </summary>
            Once = 0,

            /// <summary>
            /// 循环
            /// </summary>
            Loop = 1,

            /// <summary>
            ///  像乒乓球一样来回运动
            /// </summary>
            PingPong = 2,

        }

        /// <summary>
        /// 运动的方式
        /// </summary>
        public RotateType mType = RotateType.Once;

        /// <summary>
        /// 运动物体
        /// </summary>
        public Transform mSource;

        /// <summary>
        /// 起始点
        /// </summary>
        public Vector3 mSourceVect;

        /// <summary>
        /// 目标点
        /// </summary>
        public Vector3 mTargetVect;

        /// <summary>
        /// 起始点 Transform
        /// </summary>
        public Transform mSourceTrans;

        /// <summary>
        /// 终点 Transform
        /// </summary>
        public Transform mTargetTrans;

        /// <summary>
        /// 运动的速度
        /// </summary>
        public float mSpeed = 10f;

        /// <summary>
        /// 起始点位置
        /// </summary>
        public Vector3 SourceTweenVect
        {
            get
            {
                if (mSourceVect == Vector3.zero && mSourceTrans != null) mSourceVect = mSourceTrans.localPosition;
                return mSourceVect;
            }
            set
            {
                mSourceVect = value;
            }
        }

        /// <summary>
        /// 终点位置
        /// </summary>
        public Vector3 TargetTweenVect
        {
            get
            {
                if (mTargetVect == Vector3.zero && mTargetTrans != null) mTargetVect = mTargetTrans.localPosition;
                return mTargetVect;
            }
            set
            {
                mTargetVect = value;
            }
        }

    }
}
