using System;
using System.Collections.Generic;
using LGame.LBehaviour;
using LGame.LCommon;
using UnityEngine;

namespace LGame.LEvent
{

    /****
     * 
     * 
     * 执行 一串 tweener 事件
     * 
     * 主要对 tweener 事件辅助
     * 
     */

    public class CLTweenEvent : ALBehaviour
    {
        private static CLTweenEvent _tweenEvent = null;

        private static CLTweenEvent Instance
        {
            get
            {
                if (_tweenEvent == null)
                {
                    GameObject create = SLCompHelper.Create("_tween event");
                    _tweenEvent = SLCompHelper.FindComponet<CLTweenEvent>(create);
                    DontDestroyOnLoad(create);
                }
                return _tweenEvent;
            }
        }

        /// <summary>
        /// 运动时间
        /// </summary>
        private float mTweenTime = 0;

        /// <summary>
        /// 保存一个列表
        /// </summary>
        private List<TweenerEntity> TweenerList = new List<TweenerEntity>();

        /// <summary>
        /// 增加一个 透明运动
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginAlpha(GameObject go, float duration, float from, float to)
        {
            if (go == null) return;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                TweenType = TweenerType.Alpha,
                Target = go,
                from = from,
                to = to,
                Duration = duration,
                IsImmediately = false
            });
        }

        /// <summary>
        /// 增加一个 透明运动，是否立即开始下一个
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginAlphaImmediate(GameObject go, float duration, float from, float to)
        {
            if (go == null) return;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                TweenType = TweenerType.Alpha,
                Target = go,
                from = from,
                to = to,
                Duration = duration,
                IsImmediately = true
            });
        }

        /// <summary>
        /// 增加一个 位置运动
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginPosition(GameObject go, float duration, Vector3 from, Vector3 to)
        {
            if (go == null) return;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                TweenType = TweenerType.Position,
                Target = go,
                fVector = from,
                tVector = to,
                Duration = duration,
                IsImmediately = false
            });
        }

        /// <summary>
        /// 增加一个 位置运动
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginPositionImmediate(GameObject go, float duration, Vector3 from, Vector3 to)
        {
            if (go == null) return;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                TweenType = TweenerType.Position,
                Target = go,
                fVector = from,
                tVector = to,
                Duration = duration,
                IsImmediately = true
            });
        }

        /// <summary>
        /// 增加一个 旋转运动
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginRotation(GameObject go, float duration, Vector3 from, Vector3 to)
        {
            if (go == null) return;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                TweenType = TweenerType.Rotation,
                Target = go,
                fVector = from,
                tVector = to,
                Duration = duration,
                IsImmediately = false
            });
        }

        /// <summary>
        /// 增加一个 旋转运动
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginRotationImmediate(GameObject go, float duration, Vector3 from, Vector3 to)
        {
            if (go == null) return;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                TweenType = TweenerType.Rotation,
                Target = go,
                fVector = from,
                tVector = to,
                Duration = duration,
                IsImmediately = true
            });
        }

        /// <summary>
        /// 增加一个 大小
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginScale(GameObject go, float duration, Vector3 from, Vector3 to)
        {
            if (go == null) return;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                TweenType = TweenerType.Scale,
                Target = go,
                fVector = from,
                tVector = to,
                Duration = duration,
                IsImmediately = false
            });
        }

        /// <summary>
        /// 增加一个 大小
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginScaleImmediate(GameObject go, float duration, Vector3 from, Vector3 to)
        {
            if (go == null) return;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                TweenType = TweenerType.Scale,
                Target = go,
                fVector = from,
                tVector = to,
                Duration = duration,
                IsImmediately = true
            });
        }

        public override void OnUpdate(float deltaTime)
        {
            if (TweenerList.Count <= 0 && mTweenTime <= 0)
            {
                Destroy();
                return;
            }

            if (mTweenTime > 0)
            {
                mTweenTime -= deltaTime;
                return;
            }

            TweenerEntity entity = TweenerList[0];
            if (entity == null || entity.TweenType == TweenerType.None)
            {
                TweenerList.RemoveAt(0);
                return;
            }
            entity.Target.SetActive(true);
            UITweener tween = null;
            switch (entity.TweenType)
            {
                case TweenerType.Scale:
                    TweenScale scale = TweenScale.Begin(entity.Target, entity.Duration, entity.tVector);
                    scale.from = entity.fVector;
                    scale.to = entity.tVector;
                    scale.method = UITweener.Method.EaseInOut;
                    tween = scale;
                    break;
                case TweenerType.Alpha:
                    TweenAlpha alpha = TweenAlpha.Begin(entity.Target, entity.Duration, entity.to);
                    alpha.from = entity.from;
                    alpha.to = entity.to;
                    alpha.method = UITweener.Method.EaseInOut;
                    tween = alpha;
                    break;
                case TweenerType.Position:
                    TweenPosition position = TweenPosition.Begin(entity.Target, entity.Duration, entity.tVector);
                    position.from = entity.fVector;
                    position.to = entity.tVector;
                    position.method = UITweener.Method.EaseInOut;
                    tween = position;
                    break;
                case TweenerType.Rotation:
                    TweenRotation rotation = TweenRotation.Begin(entity.Target, entity.Duration, Quaternion.identity);
                    rotation.from = entity.fVector;
                    rotation.to = entity.tVector;
                    rotation.method = UITweener.Method.EaseInOut;
                    tween = rotation;
                    break;
            }
            mTweenTime = entity.IsImmediately ? 0 : tween.duration;
            tween.PlayForward();
            TweenerList.RemoveAt(0);
        }
    }

}

