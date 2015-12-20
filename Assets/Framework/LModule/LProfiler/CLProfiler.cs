using LGame.LBehaviour;
using LGame.LCommon;
using UnityEngine;


/***
 * 
 * 
 * 系统内存分析
 * 
 * 
 */

namespace LGame.LProfiler
{

    /****
     * 
     * 
     * 内存使用分析
     * 
     * 
     */

    public class CLProfiler : ALBehaviour
    {

        /// <summary>
        /// 开始分析
        /// 
        /// 自己创建一个节点
        /// </summary>
        /// <returns></returns>
        public static CLProfiler BeginProfiler()
        {
            GameObject create = SLCompHelper.Create("_Profiler");
            return BeginProfiler(create);
        }

        /// <summary>
        /// 开始分析
        /// </summary>
        /// <returns></returns>
        public static CLProfiler BeginProfiler(GameObject go)
        {
            return go == null ? null : SLCompHelper.FindComponet<CLProfiler>(go);
        }

        /// <summary>
        /// 绘制分析结果
        /// </summary>
        public override void OnGUI()
        {
            if (!SLConfig.IsProfiler) return;

            GUI.Label(new Rect(0, 0, 200, 25), string.Format("MonoUsedSize:{0}", Profiler.GetMonoUsedSize() / SLConfig.KbSize));
            GUI.Label(new Rect(0, 15, 200, 25), string.Format("Allocated:{0}", Profiler.GetTotalAllocatedMemory() / SLConfig.KbSize));
            GUI.Label(new Rect(0, 30, 200, 25), string.Format("Reserved:{0}", Profiler.GetTotalReservedMemory() / SLConfig.KbSize));
        }

    }
}
