using System;
using System.Collections.Generic;
using LGame.LCommon;
using UnityEngine;

namespace LGame.LAndroid
{

    /*****
     * 
     * 
     * unity 与 Android 对接
     * 
     * 
     */

    public sealed class SLAndroidJava
    {

        /// <summary>
        /// 调用Android 函数，取一个对象
        /// </summary>
        /// <param name="methodName">函数名字</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static object GetStaticObject(string methodName, params object[] args)
        {
            return CallStatic<object>(methodName, args);
        }

        /// <summary>
        /// 调用Android 函数，取一个int
        /// </summary>
        /// <param name="methodName">函数名字</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static int GetStaticInt32(string methodName, params object[] args)
        {
            return CallStatic<int>(methodName, args);
        }

        /// <summary>
        /// 调用Android 函数，取一个字符串
        /// </summary>
        /// <param name="methodName">函数名字</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static string GetStaticString(string methodName, params object[] args)
        {
            return CallStatic<string>(methodName, args);
        }

        /// <summary>
        /// 调用Android 函数，取一个float
        /// </summary>
        /// <param name="methodName">函数名字</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static float GetStaticFloat(string methodName, params object[] args)
        {
            return CallStatic<float>(methodName, args);
        }

        /// <summary>
        /// 调用Android 函数，取一个byte
        /// </summary>
        /// <param name="methodName">函数名字</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static byte GetStaticByte(string methodName, params object[] args)
        {
            return CallStatic<byte>(methodName, args);
        }

        /// <summary>
        /// 调用Android函数，获取对象(object)数组
        /// </summary>
        /// <param name="methodName">函数名字</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static object[] GetStaticObjects(string methodName, params object[] args)
        {
            return CallStatic<object[]>(methodName, args);
        }

        /// <summary>
        /// 调用Android函数，获取整数(int32)数组
        /// </summary>
        /// <param name="methodName">函数名字</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static int[] GetStaticInt32s(string methodName, params object[] args)
        {
            return CallStatic<int[]>(methodName, args);
        }

        /// <summary>
        /// 调用Android函数，获取字符串数组
        /// </summary>
        /// <param name="methodName">函数名字</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static string[] GetStaticStrings(string methodName, params object[] args)
        {
            return CallStatic<string[]>(methodName, args);
        }

        /// <summary>
        /// 调用Android 函数，取一个float数组
        /// </summary>
        /// <param name="methodName">函数名字</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static float[] GetStaticFloats(string methodName, params object[] args)
        {
            return CallStatic<float[]>(methodName, args);
        }

        /// <summary>
        /// 调用Android函数，获取字节数组
        /// </summary>
        /// <param name="methodName">函数名字</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static byte[] GetStaticBytes(string methodName, params object[] args)
        {
            return CallStatic<byte[]>(methodName, args);
        }

        /// <summary>
        /// 调用Android 静态函数，没有返回值
        /// </summary>
        /// <param name="methodName">方法名字</param>
        /// <param name="args">参数</param>
        public static void CallStatic(string methodName, params object[] args)
        {
            CallStatic(SLPathHelper.UnityAndroidUtilPath(), methodName, args);
        }

        /// <summary>
        /// 调用 Android 静态函数, 有返回值
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="methodName">方法名字</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static T CallStatic<T>(string methodName, params object[] args)
        {
            return CallStatic<T>(SLPathHelper.UnityAndroidUtilPath(), methodName, args);
        }

        /// <summary>
        /// 调用Android 静态函数，没有返回值
        /// </summary>
        /// <param name="className">类的名字</param>
        /// <param name="methodName">方法名字</param>
        /// <param name="args">参数</param>
        public static void CallStatic(string className, string methodName, params object[] args)
        {
            AndroidJavaClass cla = new AndroidJavaClass(className);
            cla.CallStatic(methodName, args);
        }

        /// <summary>
        /// 调用 Android 静态函数, 有返回值
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="className">类的名字</param>
        /// <param name="methodName">方法名字</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static T CallStatic<T>(string className, string methodName, params object[] args)
        {
            AndroidJavaClass cla = new AndroidJavaClass(className);
            return cla.CallStatic<T>(methodName, args);
        }

    }

}
