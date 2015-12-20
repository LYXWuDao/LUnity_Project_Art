using LGame.LBehaviour;
using LGame.LCommon;
using UnityEngine;

/***
 * 
 * 对象自适应屏幕
 * 
 * 屏幕自适应
 * 
 */

namespace LGame.LUtils
{

    public class CLAdaptiveScene : ALBehaviour
    {

        /// <summary>
        /// 跟新的方式
        /// </summary>
        public enum LYXAnchorUpdate
        {
            OnEnable,
            OnUpdate,
            OnStart,
        }

        /// <summary>
        ///  跟新方式
        /// </summary>
        public LYXAnchorUpdate anchorUpdate = LYXAnchorUpdate.OnStart;

        /// <summary>
        /// 当前是否存在 ngui widget 组件
        /// </summary>
        private UIWidget anchorWidget;

        /// <summary>
        /// 屏幕的宽
        /// </summary>
        public int mSceneWidth;

        /// <summary>
        /// 屏幕的高
        /// </summary>
        public int mSceneHeight;

        /// <summary>
        /// 物体的原始宽度
        /// </summary>
        public float mOriginalWidth;

        /// <summary>
        /// 物体的原始高度
        /// </summary>
        public float mOriginalHeight;

        public override void Awake()
        {
            transform.localScale = Vector3.one;
            anchorWidget = gameObject.GetComponent<UIWidget>();
            bool isWidget = anchorWidget != null;
            Vector2 vect = SLCompHelper.SceneWidthAndHeight();
            mSceneWidth = (int)vect.x;
            mSceneHeight = (int)vect.y;
            if (isWidget)
            {
                anchorWidget.ResetAnchors();
                mOriginalWidth = anchorWidget.localSize.x;
                mOriginalHeight = anchorWidget.localSize.y;
            }
            else
            {
                // 1024 - 15
                // 768 - 11
                mOriginalWidth = 15 / 1024f;
                mOriginalHeight = 11 / 768f;
            }
        }

        public override void OnEnable()
        {
            if (anchorUpdate == LYXAnchorUpdate.OnEnable) AdaptiveScene();
        }

        public override void Start()
        {
            if (anchorUpdate == LYXAnchorUpdate.OnStart) AdaptiveScene();
        }

        public override void Update()
        {
            if (anchorUpdate == LYXAnchorUpdate.OnUpdate)
            {
                Vector2 vect = SLCompHelper.SceneWidthAndHeight();
                mSceneWidth = (int)vect.x;
                mSceneHeight = (int)vect.y;
                AdaptiveScene();
            }
        }

        /// <summary>
        /// 适应屏幕
        /// </summary>
        public void AdaptiveScene()
        {
            int w = 0, h = 0;
            if (anchorWidget != null)
            {
                float ratioW = mOriginalWidth / mSceneWidth;
                float ratioH = mOriginalHeight / mSceneHeight;
                if (ratioW < ratioH)
                {
                    w = mSceneWidth;
                    h = (int)(mOriginalHeight / ratioW);
                }
                else
                {
                    h = mSceneHeight;
                    w = (int)(mOriginalWidth / ratioH);
                }
                anchorWidget.SetDimensions(w, h);
            }
            else
            {
                float ratioW = Mathf.Ceil(mSceneWidth * mOriginalWidth);
                float ratioH = Mathf.Ceil(mSceneHeight * mOriginalHeight);
                transform.localScale = new Vector3(ratioW, 1, ratioH);
            }
        }

    }
}