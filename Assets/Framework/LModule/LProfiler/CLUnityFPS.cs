using LGame.LBehaviour;
using UnityEngine;
using System.Collections;

namespace LGame.LProfiler
{

    /*****
     * 
     * 
     * 
     *  显示 unity 帧率
     * 
     * 
     */

    public class CLUnityFPS : ALBehaviour
    {

        /// <summary>
        /// 跟新时间
        /// </summary>
        private float f_UpdateInterval = 0.5F;

        private float f_LastInterval;

        private int i_Frames = 0;

        /// <summary>
        /// 运行时帧率
        /// </summary>
        private float f_Fps;

        public override void Start()
        {
            f_LastInterval = Time.realtimeSinceStartup;
            i_Frames = 0;
        }

        public override void OnGUI()
        {
            GUI.Label(new Rect(0, 100, 200, 200), "FPS:" + f_Fps.ToString("f2"));
        }

        public override void Update()
        {
            ++i_Frames;

            if (Time.realtimeSinceStartup > f_LastInterval + f_UpdateInterval)
            {
                f_Fps = i_Frames / (Time.realtimeSinceStartup - f_LastInterval);

                i_Frames = 0;

                f_LastInterval = Time.realtimeSinceStartup;
            }
        }
    }

}