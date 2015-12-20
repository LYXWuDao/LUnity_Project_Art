
using System;
using System.Collections.Generic;
using LGame.LJson;
using UnityEngine;

namespace LGame.LMessage
{

    /***
     * 
     * 
     * 使用untiy 自己的 PlayerPrefs 类 进行保存数据
     * 
     *  window 下对注册表直接进行操作
     * 
     */

    public static class SLLocalPrefs
    {

        /// <summary>
        /// 保存的数据 key
        /// </summary>
        private static List<string> PrefsKeys = new List<string>();

        /// <summary>
        /// 是否存在key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool HasPrefsKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        /// <summary>
        /// 删除所以的值
        /// </summary>
        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
            PrefsKeys.Clear();
        }

        /// <summary>
        /// 删除某一个值
        /// </summary>
        public static void Delete(string key)
        {
            PlayerPrefs.DeleteKey(key);
            PrefsKeys.Remove(key);
        }

        /// <summary>
        /// 删除某个下班的数据
        /// </summary>
        /// <param name="index">从0开始</param>
        public static void DeleteAt(int index)
        {
            if (index < 0 || index >= PrefsKeys.Count) return;
            string key = PrefsKeys[index];
            PlayerPrefs.DeleteKey(key);
            PrefsKeys.Remove(key);
        }

        /// <summary>
        /// 移出某个范围的数据
        /// </summary>
        /// <param name="start">开始移出的位置,不能小于0</param>
        /// <param name="length">
        /// 移出的长度，如果超出长度，就删除开始位置后面所有的数据
        /// 如果 length = 0， 则删除开始位置后面所有数据
        /// </param>
        public static void DeleteRange(int start, int length = 0)
        {
            if (start < 0 || start >= PrefsKeys.Count) return;
            int end = start + length;
            if (length == 0 || end > PrefsKeys.Count) end = PrefsKeys.Count;
            for (int i = start, len = end; i < len; i++)
                PlayerPrefs.DeleteKey(PrefsKeys[i]);
            PrefsKeys.RemoveRange(start, length);
        }

        /// <summary>
        /// 增加 整数型数据
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        public static void AddInt(string key, int value)
        {
            if (string.IsNullOrEmpty(key)) return;
            PlayerPrefs.SetInt(key, value);
            if (!HasPrefsKey(key)) PrefsKeys.Add(key);
        }

        /// <summary>
        /// 增加 浮点型数据
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        public static void AddFloat(string key, float value)
        {
            if (string.IsNullOrEmpty(key)) return;
            PlayerPrefs.SetFloat(key, value);
            if (!HasPrefsKey(key)) PrefsKeys.Add(key);
        }

        /// <summary>
        /// 增加 字符串型数据
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        public static void AddString(string key, string value)
        {
            if (string.IsNullOrEmpty(key)) return;
            PlayerPrefs.SetString(key, value);
            if (!HasPrefsKey(key)) PrefsKeys.Add(key);
        }

        /// <summary>
        /// 增加 json 数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddJson<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key)) return;
            AddString(key, CLJson.ToJson(value));
        }

        /// <summary>
        /// 增加 json 数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddJson(string key, string value)
        {
            if (string.IsNullOrEmpty(key)) return;
            AddString(key, value);
        }

        /// <summary>
        /// 得到整数型数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetInt(string key)
        {
            if (string.IsNullOrEmpty(key)) return 0;
            return PlayerPrefs.GetInt(key, 0);
        }

        /// <summary>
        /// 得到整数型数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryInt(string key, out int value)
        {
            value = 0;
            if (!HasPrefsKey(key)) return false;
            value = PlayerPrefs.GetInt(key);
            return true;
        }

        /// <summary>
        /// 得到浮点型数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static float GetFloat(string key)
        {
            if (string.IsNullOrEmpty(key)) return 0;
            return PlayerPrefs.GetFloat(key, 0);
        }

        /// <summary>
        /// 得到浮点型数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryFloat(string key, out float value)
        {
            value = 0;
            if (!HasPrefsKey(key)) return false;
            value = PlayerPrefs.GetFloat(key);
            return true;
        }

        /// <summary>
        /// 得到字符串型数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static String GetString(string key)
        {
            if (string.IsNullOrEmpty(key)) return string.Empty;
            return PlayerPrefs.GetString(key, string.Empty);
        }

        /// <summary>
        /// 得到字符串型数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryString(string key, out string value)
        {
            value = string.Empty;
            if (!HasPrefsKey(key)) return false;
            value = PlayerPrefs.GetString(key);
            return true;
        }

        /// <summary>
        /// 得到 json 数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static CLJson GetJson(string key)
        {
            if (string.IsNullOrEmpty(key)) return null;
            string json = GetString(key);
            return string.IsNullOrEmpty(json) ? null : new CLJson(json);
        }

        /// <summary>
        /// 得到 json 数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetJson<T>(string key)
        {
            if (string.IsNullOrEmpty(key)) return default(T);
            string json = GetString(key);
            return CLJson.ToObejct<T>(json);
        }

        /// <summary>
        /// 保存数据到磁盘
        /// </summary>
        public static void SavePrefs()
        {
            PlayerPrefs.Save();
        }

    }

}

