using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace LGame.LCommon
{

    /****
     * 
     * 
     * 实现自己的唯一id， guid
     * 
     */

    public class LCSGuid
    {

        /// <summary>
        /// 新建一个 guid
        /// </summary>
        /// <returns></returns>
        public static String NewGuid()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 创建一个大写的 guid
        /// </summary>
        /// <returns></returns>
        public static String NewUpperGuid()
        {
            string guid = NewGuid();
            if (string.IsNullOrEmpty(guid)) return string.Empty;
            return guid.ToUpper();
        }

        /// <summary>
        /// 创建一个小写的 guid
        /// </summary>
        /// <returns></returns>
        public static String NewLowerGuid()
        {
            string guid = NewGuid();
            if (string.IsNullOrEmpty(guid)) return string.Empty;
            return guid.ToLower();
        }

    }

}

