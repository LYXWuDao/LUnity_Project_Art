using System;
using System.Collections.Generic;
using LGame.LBehaviour;
using UnityEngine;


namespace LGame.LCommon
{

    /***
     * 
     * 
     * 开辟一个调用协成的类
     * 
     * 
     */

    public class LCCoroutine : LABehaviour
    {

        private static LCCoroutine _instance;

        private static object _lock = new object();

        public static LCCoroutine Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            GameObject create = LCSCompHelper.Create("_async load");
                            _instance = LCSCompHelper.FindComponet<LCCoroutine>(create);
                        }
                    }
                }
                return _instance;
            }
        }

    }

}
