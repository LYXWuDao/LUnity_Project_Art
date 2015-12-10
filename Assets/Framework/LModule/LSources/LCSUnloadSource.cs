using LGame.LDebug;
using UnityEngine;


/*****
 * 
 * 
 * 销毁资源
 * 
 */

namespace LGame.LSource
{

    public class LCSUnloadSource
    {

        /// <summary>
        /// 卸载资源
        /// </summary>
        /// <param name="bundle">卸载的资源</param>
        public static void UnLoadSource(AssetBundle bundle)
        {
            UnLoadSource(bundle, true);
        }

        /// <summary>
        /// 卸载资源
        /// </summary>
        /// <param name="bundle">卸载的资源</param>
        /// <param name="unload"></param>
        public static void UnLoadSource(AssetBundle bundle, bool unload)
        {
            if (bundle == null)
            {
                LCSConsole.WriteError("卸载资源为空！");
                return;
            }
            bundle.Unload(unload);
        }

    }

}