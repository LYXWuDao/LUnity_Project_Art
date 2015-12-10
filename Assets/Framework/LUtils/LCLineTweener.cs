using UnityEngine;
using System.Collections;

/****
 * 
 *  物体运动
 * 
 */


namespace Game.LUtils
{

    public class LCLineTweener : LCTweener
    {

        /// <summary>
        /// 是正向移动
        /// </summary>
        private int mForward = 1;

        /// <summary>
        /// 方向向量
        /// </summary>
        private Vector3 mDirVector = Vector3.one;

        /// <summary>
        /// 每次递增
        /// </summary>
        private Vector3 mSourceTemp = Vector3.zero;

        /// <summary>
        /// 验证是否到达终点
        /// </summary>
        /// <returns></returns>
        private bool Verify(Vector3 source, Vector3 target)
        {
            return Vector3.Distance(source, target) <= 1;
        }

        public override void Start()
        {
            mSourceTemp = SourceTweenVect;
            mDirVector = (TargetTweenVect - SourceTweenVect).normalized;
            UnityEngine.Debug.Log(mDirVector);
            mForward = 1;
        }

        public override void Update()
        {
            if (SourceTweenVect == TargetTweenVect || mSource == null) return;

            mSourceTemp = mSourceTemp + mDirVector*mSpeed*Time.deltaTime;
            if (Verify(mSourceTemp, TargetTweenVect))
            {
                if (mType == RotateType.Once)
                {
                    enabled = false;
                }
                else if (mType == RotateType.PingPong)
                {
                    TargetTweenVect = SourceTweenVect;
                    SourceTweenVect = mSourceTemp;
                    mDirVector = -mDirVector;
                    mForward = -mForward;
                }
                else if (mType == RotateType.Loop)
                {
                    mSourceTemp = SourceTweenVect;
                }
                return;
            }

            mSource.localPosition = mSourceTemp;
        }

    }
}