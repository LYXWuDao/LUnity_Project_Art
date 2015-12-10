using System;
using System.Collections.Generic;
using LGame.LCommon;

namespace LGame.LBase
{

    /****
     * 
     * 
     * 所有管理的基类
     *  
     * 包括，资源管理，界面管理等
     * 
     * 
     */

    public abstract class LATManager<TValue>
    {

        /// <summary>
        /// 保存所有数据
        /// 
        /// Type 表示key值，唯一
        /// 
        /// </summary>
        private static Dictionary<Type, LCTManagerEntity<TValue>> _dicManager = new Dictionary<Type, LCTManagerEntity<TValue>>();

        /// <summary>
        /// 查找某一类型的数据
        /// </summary>
        /// <returns></returns>
        private static LCTManagerEntity<TValue> Find<T>()
        {
            Type tkey = typeof(T);
            LCTManagerEntity<TValue> entity;
            _dicManager.TryGetValue(tkey, out entity);
            return entity;
        }

        /// <summary>
        /// 查找查找一类数据
        /// </summary>
        /// <returns></returns>
        private static bool TryFind<T>(out LCTManagerEntity<TValue> value)
        {
            return null != (value = Find<T>());
        }

        /// <summary>
        /// 增加管理数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">值</param>
        public static void Add<T>(string key, TValue value)
        {
            LCTManagerEntity<TValue> entity;
            if (!TryFind<T>(out entity))
            {
                entity = new LCTManagerEntity<TValue>();
                _dicManager.Add(typeof(T), entity);
            }
            entity.Add(key, value);
        }

        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="key">数据key, 不能重复</param>
        /// <returns>返回查找到的数据</returns>
        public static TValue Find<T>(string key)
        {
            if (string.IsNullOrEmpty(key)) return default(TValue);
            LCTManagerEntity<TValue> entity;
            return !TryFind<T>(out entity) ? default(TValue) : entity.Find(key);
        }

        /// <summary>
        /// 尝试查找数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryFind<T>(string key, out TValue value)
        {
            return null != (value = Find<T>(key));
        }

        /// <summary>
        /// 查找某一类型数据的所有值
        /// </summary>
        /// <typeparam name="T">数据值的类型</typeparam>
        /// <returns></returns>
        public static List<TValue> FindValues<T>()
        {
            LCTManagerEntity<TValue> entity;
            return !TryFind<T>(out entity) ? null : entity.Values;
        }

        /// <summary>
        /// 查找某类型的所有key值
        /// </summary>
        /// <typeparam name="T">数据值的类型</typeparam>
        /// <returns></returns>
        public static List<string> FindKeys<T>()
        {
            LCTManagerEntity<TValue> entity;
            return !TryFind<T>(out entity) ? null : entity.Keys;
        }

        /// <summary>
        /// 清理该类型的所有数据
        /// </summary>
        /// <returns>返回是否清理成功</returns>
        public static bool Remove<T>()
        {
            Type tkey = typeof(T);
            return _dicManager.ContainsKey(tkey) && _dicManager.Remove(tkey);
        }

        /// <summary>
        /// 移出数据
        /// </summary>
        /// <param name="key">数据key, 不能重复</param>
        /// <returns>返回移出的数据,如果为空表示没有找到移出的数据</returns>
        public static TValue Remove<T>(string key)
        {
            LCTManagerEntity<TValue> entity;
            return !TryFind<T>(out entity) ? default(TValue) : entity.Remove(key);
        }

        /// <summary>
        /// 清除保存的所有数据
        /// </summary>
        public static void Clear()
        {
            if (_dicManager == null || _dicManager.Count <= 0) return;
            _dicManager.Clear();
        }

    }

}