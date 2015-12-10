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

    public class LCProfiler : LABehaviour
    {

        /// <summary>
        /// 开始分析
        /// 
        /// 自己创建一个节点
        /// </summary>
        /// <returns></returns>
        public static LCProfiler BeginProfiler()
        {
            GameObject create = LCSCompHelper.Create("_Profiler");
            return BeginProfiler(create);
        }

        /// <summary>
        /// 开始分析
        /// </summary>
        /// <returns></returns>
        public static LCProfiler BeginProfiler(GameObject go)
        {
            return go == null ? null : LCSCompHelper.FindComponet<LCProfiler>(go);
        }

        /// <summary>
        /// 绘制分析结果
        /// </summary>
        public override void OnGUI()
        {
            if (!LCSConfig.IsProfiler) return;

            GUI.Label(new Rect(0, 0, 200, 25), string.Format("MonoUsedSize:{0}", Profiler.GetMonoUsedSize() / LCSConfig.KbSize));
            GUI.Label(new Rect(0, 15, 200, 25), string.Format("Allocated:{0}", Profiler.GetTotalAllocatedMemory() / LCSConfig.KbSize));
            GUI.Label(new Rect(0, 30, 200, 25), string.Format("Reserved:{0}", Profiler.GetTotalReservedMemory() / LCSConfig.KbSize));
        }

    }
}
