
using System;
using LitJson;

namespace LGame.LJson
{

    /***
     * 
     * 
     *  封装 json 操作
     * 
     */

    public class LCJson
    {

        private JsonData _jsonData;

        #region 静态处理

        /// <summary>
        /// 是一个单一的值，还是一个对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static bool IsObject(object obj)
        {
            return !(obj is Boolean || obj is Double || obj is Int32 || obj is Int64 || obj is String);
        }

        /// <summary>
        /// 将json 数据转成 JsonData
        /// </summary>
        /// <param name="json">json 字符串</param>
        /// <returns></returns>
        private static JsonData ToJsonData(string json)
        {
            if (string.IsNullOrEmpty(json)) return null;
            return JsonMapper.ToObject(json);
        }

        /// <summary>
        /// 将 T 数据转成 JsonData
        /// </summary>
        /// <param name="obj">obj对象</param>
        /// <returns></returns>
        private static JsonData ToJsonData(object obj)
        {
            if (obj == null) return null;
            return IsObject(obj) ? ToJsonData(JsonMapper.ToJson(obj)) : new JsonData { obj };
        }

        /// <summary>
        /// 将 object 转成json 字符串
        /// </summary>
        /// <param name="obj">object 对象</param>
        /// <returns></returns>
        public static string ToJson(object obj)
        {
            return obj == null ? string.Empty : JsonMapper.ToJson(obj);
        }

        /// <summary>
        /// 将 object 数组转成 json 字符串
        /// </summary>
        /// <param name="obj">object 对象数组</param>
        /// <returns></returns>
        public static string ToJson(object[] obj)
        {
            return obj == null ? null : JsonMapper.ToJson(obj);
        }

        /// <summary>
        /// 将json 字符串转成 T 类型的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">json 字符串</param>
        /// <returns></returns>
        public static T ToObejct<T>(string json)
        {
            return string.IsNullOrEmpty(json) ? default(T) : JsonMapper.ToObject<T>(json);
        }

        /// <summary>
        /// 将 json 数据转成基础类型数组
        /// 
        /// 基础类型包括：int, string, bool, long, double
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static object[] ToArray(string json)
        {
            if (string.IsNullOrEmpty(json)) return null;
            JsonData data = ToJsonData(json);
            int len = data.Count;
            if (len <= 0) return null;
            object[] results = new object[len];
            for (int i = 0; i < len; i++)
            {
                JsonData temp = data[i];
                if (temp.IsObject || temp.IsArray) continue;
                results[i] = temp;
            }
            return results;
        }

        /// <summary>
        /// 将 json 数据转成类数组
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T[] ToArray<T>(string json)
        {
            if (string.IsNullOrEmpty(json)) return null;
            JsonData data = ToJsonData(json);
            int len = data.Count;
            if (len <= 0) return null;
            T[] results = new T[len];
            for (int i = 0; i < len; i++)
            {
                JsonData temp = data[i];
                if (!temp.IsObject) continue;
                results[i] = ToObejct<T>(temp.ToJson());
            }
            return results;
        }

        #endregion

        /// <summary>
        /// 初始化 json 对象
        /// 
        /// 将根据第一次增加的数据判断是数组还是键值对
        /// 
        /// </summary>
        public LCJson()
        {
            _jsonData = new JsonData();
        }

        /// <summary>
        /// 初始化 json 对象
        /// 
        /// 将 json 字符串转成一个对象
        /// 
        /// </summary>
        /// <param name="json">json 数据</param>
        public LCJson(string json)
        {
            _jsonData = JsonMapper.ToObject(json);
        }

        /// <summary>
        /// 
        /// 初始化 json 对象
        /// 
        /// 如果是类类型数据，则转换成 键值对
        /// 
        /// 如果是基础数据则默认是数组
        /// 
        /// 基础类型包括：int, string, bool, long, double
        /// </summary>
        /// <param name="obj">一个object对象</param>
        public LCJson(object obj)
        {
            _jsonData = ToJsonData(obj);
        }

        /// <summary>
        /// 得到数组长度
        /// </summary>
        public int Length
        {
            get
            {
                if (!_jsonData.IsArray) return 0;
                return _jsonData.Count;
            }
        }

        /// <summary>
        /// 
        /// 数组增加 
        /// 
        /// 增加一个值
        /// </summary>
        /// <param name="value"></param>
        public void Add(object value)
        {
            if (_jsonData.GetJsonType() != JsonType.None && !_jsonData.IsArray) return;
            _jsonData.Add(IsObject(value) ? ToJsonData(value) : value);
        }

        /// <summary>
        /// 
        /// 增加键值对
        /// 
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void Add(string key, object value)
        {
            if (_jsonData.GetJsonType() != JsonType.None && !_jsonData.IsObject) return;
            _jsonData[key] = IsObject(value) ? ToJsonData(value) : new JsonData(value);
        }

        /// <summary>
        /// 如果是数组则获得第几个数据
        /// </summary>
        /// <param name="index"></param>
        public object GetValue(int index)
        {
            if (!_jsonData.IsArray) return null;
            if (index < 0 || index >= _jsonData.Count) return null;
            return _jsonData[index];
        }

        /// <summary>
        /// 如果是数组则获得第几个数据
        /// </summary>
        /// <param name="index"></param>
        public T GetValue<T>(int index)
        {
            if (!_jsonData.IsArray) return default(T);
            if (index < 0 || index >= _jsonData.Count) return default(T);
            JsonData data = _jsonData[index];
            return !data.IsObject ? default(T) : ToObejct<T>(data.ToJson());
        }

        /// <summary>
        /// 如果是键值对
        /// 
        /// 根据 key 获得值
        /// </summary>
        /// <param name="key"></param>
        public object GetValue(string key)
        {
            if (!_jsonData.IsObject) return null;
            if (!_jsonData.Keys.Contains(key)) return null;
            return _jsonData[key];
        }

        /// <summary>
        /// 如果是键值对
        /// 
        /// 根据 key 获得值
        /// </summary>
        /// <param name="key"></param>
        public T GetValue<T>(string key)
        {
            if (!_jsonData.IsObject) return default(T);
            if (!_jsonData.Keys.Contains(key)) return default(T);
            JsonData data = _jsonData[key];
            if (!data.IsObject) return default(T);
            return ToObejct<T>(data.ToJson());
        }

        /// <summary>
        /// 将对象转换成 json
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return _jsonData.ToJson();
        }

    }

}