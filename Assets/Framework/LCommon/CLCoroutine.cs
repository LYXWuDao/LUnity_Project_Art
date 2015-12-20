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

    public class CLCoroutine : ALBehaviour
    {

        private static CLCoroutine _instance;

        private static object _lock = new object();

        public static CLCoroutine Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            GameObject create = SLCompHelper.Create("_game coroutine");
                            _instance = SLCompHelper.FindComponet<CLCoroutine>(create);
                        }
                    }
                }
                return _instance;
            }
        }

    }

}
