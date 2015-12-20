using System;
using UnityEngine;

/****
 * 
 * 自定义组件帮助类
 * 组件 辅助类
 * 
 * 包括自动创建组件，删除 等
 * 
 */

namespace LGame.LCommon
{

    public static class SLCompHelper
    {

        /// <summary>
        ///  创建 gameobject
        /// </summary>
        /// <returns></returns>
        public static GameObject Create()
        {
            return Create("game object", null);
        }

        /// <summary>
        ///  创建 gameobject
        /// </summary>
        /// <param name="name">创建的 gameobject 名字 </param>
        /// <returns></returns>
        public static GameObject Create(string name)
        {
            return Create(name, null);
        }

        /// <summary>
        ///  创建 gameobject
        /// </summary>
        /// <param name="name">创建的 gameobject 名字</param>
        /// <param name="parent">创建的 gameobject 父节点</param>
        /// <returns></returns>
        public static GameObject Create(string name, Transform parent)
        {
            return Create(name, parent, null);
        }

        /// <summary>
        ///  创建 gameobject
        /// </summary>
        /// <param name="name">创建的 gameobject 名字</param>
        /// <param name="parent">创建的 gameobject 父节点</param>
        /// <param name="t">创建 gameobject 类型 </param>
        /// <returns></returns>
        public static GameObject Create(string name, Transform parent, Type t)
        {
            return CreateInstance(name, parent, t);
        }

        /// <summary>
        /// 创建一个 gameobject 并且实例和初始化
        /// </summary>
        /// <returns></returns>
        public static GameObject CreateInstance(string name, Transform parent, Type t)
        {
            GameObject create = new GameObject(name, t);
            Transform trans = create.transform;
            if (parent != null) trans.parent = parent;
            trans.localPosition = Vector3.zero;
            trans.localRotation = Quaternion.identity;
            trans.localScale = Vector3.one;
            return create;
        }

        /// <summary>
        /// 查找组件
        /// </summary>
        /// <typeparam name="T">组件的类型</typeparam>
        /// <param name="source">查找组件的对象</param>
        /// <returns></returns>
        public static T GetComponent<T>(GameObject source) where T : Component
        {
            return GetComponent<T>(source, string.Empty);
        }

        /// <summary>
        /// 查找组件
        /// 
        /// 单个
        /// </summary>
        /// <typeparam name="T">组件的类型</typeparam>
        /// <param name="source">查找组件的对象</param>
        /// <param name="childPath">子节点路径</param>
        /// <returns></returns>
        public static T GetComponent<T>(GameObject source, string childPath) where T : Component
        {
            if (source == null) return default(T);
            if (string.IsNullOrEmpty(childPath)) return source.GetComponent<T>();
            Transform child = FindTransform(source, childPath);
            if (child == null) return default(T);
            return child.GetComponent<T>();
        }

        /// <summary>
        /// 查找组件
        /// 
        /// 多个，包括子节点上的
        /// </summary>
        /// <typeparam name="T">组件的类型</typeparam>
        /// <param name="source">查找组件的对象</param>
        /// <returns></returns>
        public static T[] GetComponents<T>(GameObject source) where T : Component
        {
            return source == null ? null : source.GetComponentsInChildren<T>();
        }

        /// <summary>
        /// 增加组件 
        /// </summary>
        /// <typeparam name="T">组件的类型</typeparam>
        /// <param name="source">增加组件的 gameobject </param>
        /// <returns></returns>
        public static T AddComponet<T>(GameObject source) where T : Component
        {
            return AddComponet<T>(source, string.Empty);
        }

        /// <summary>
        /// 增加组件 
        /// </summary>
        /// <typeparam name="T">组件的类型</typeparam>
        /// <param name="source">增加组件的 gameobject </param>
        /// <param name="childPath">子节点路径</param>
        /// <returns></returns>
        public static T AddComponet<T>(GameObject source, string childPath) where T : Component
        {
            if (source == null) return default(T);
            if (string.IsNullOrEmpty(childPath)) return source.AddComponent<T>();
            Transform child = FindTransform(source, childPath);
            if (child == null) return default(T);
            return child.gameObject.AddComponent<T>();
        }

        /// <summary>
        /// 发现组件，如果不存在就自动增加
        /// </summary>
        /// <typeparam name="T">组件的类型</typeparam>
        /// <param name="source">查找/增加 组件的 gameobject</param>
        /// <returns></returns>
        public static T FindComponet<T>(GameObject source) where T : Component
        {
            return FindComponet<T>(source, string.Empty);
        }

        /// <summary>
        /// 发现组件，如果不存在就自动增加
        /// </summary>
        /// <typeparam name="T">组件的类型</typeparam>
        /// <param name="source">查找/增加 组件的 gameobject</param>
        /// <param name="childPath">子节点路径</param>
        /// <returns></returns>
        public static T FindComponet<T>(GameObject source, string childPath) where T : Component
        {
            T t = GetComponent<T>(source, childPath);
            if (t != null) return t;
            return AddComponet<T>(source, childPath);
        }

        /// <summary>
        /// 查找子节点中的GameObject
        /// </summary>
        /// <param name="source"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static GameObject FindGameObject(GameObject source, string path)
        {
            if (source == null) return null;
            return FindGameObject(source.transform, path);
        }

        /// <summary>
        /// 查找子节点中的GameObject
        /// </summary>
        /// <param name="source"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static GameObject FindGameObject(Transform source, string path)
        {
            Transform trans = FindTransform(source, path);
            if (trans == null) return null;
            return trans.gameObject;
        }

        /// <summary>
        /// 发现子节点
        /// </summary>
        /// <param name="source">资源父节点</param>
        /// <param name="path">查找子节点路径</param>
        /// <returns></returns>
        public static Transform FindTransform(GameObject source, string path)
        {
            if (source == null) return null;
            return FindTransform(source.transform, path);
        }

        /// <summary>
        /// 发现子节点
        /// </summary>
        /// <param name="source">资源父节点</param>
        /// <param name="path">查找子节点路径</param>
        /// <returns></returns>
        public static Transform FindTransform(Transform source, string path)
        {
            if (source == null || string.IsNullOrEmpty(path)) return null;
            return source.Find(path);
        }

        /// <summary>
        /// 初始化 transform
        /// </summary>
        /// <param name="target">初始化预设</param>
        /// <param name="parent">父节点</param>
        public static void InitTransform(GameObject target, Transform parent)
        {
            if (target == null) return;
            InitTransform(target.transform, parent);
        }

        /// <summary>
        /// 初始化 transform
        /// </summary>
        /// <param name="target"></param>
        /// <param name="parent">父节点</param>
        public static void InitTransform(Transform target, Transform parent)
        {
            if (target == null) return;
            if (parent != null) target.parent = parent;
            target.localPosition = Vector3.zero;
            target.localRotation = Quaternion.identity;
            target.localScale = Vector3.one;
        }

        /// <summary>
        /// 获得屏幕的高和宽
        /// </summary>
        /// <returns></returns>
        public static Vector2 SceneWidthAndHeight()
        {
            Vector2 vect = new Vector2(Screen.width, Screen.height);
            UIRoot root = GameObject.FindObjectOfType<UIRoot>();
            if (root != null)
            {
                float s = (float)root.activeHeight / Screen.height;
                int height = Mathf.CeilToInt(Screen.height * s);
                int width = Mathf.CeilToInt(Screen.width * s);
                vect = new Vector2(width, height);
            }
            return vect;
        }

        /// <summary>
        /// 计算粒子播放的时长
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static float ParticleSystemLength(Transform transform)
        {
            if (transform == null) return 0f;
            ParticleSystem[] particleSystems = transform.GetComponentsInChildren<ParticleSystem>();
            if (particleSystems == null || particleSystems.Length <= 0) return 0f;
            float maxDuration = 0;
            for (int i = 0, len = particleSystems.Length; i < len; i++)
            {
                ParticleSystem ps = particleSystems[i];
                if (ps == null || !ps.enableEmission) continue;
                if (ps.loop) return -1f;

                float dunration = 0f;
                if (ps.emissionRate <= 0)
                {
                    dunration = ps.startDelay + ps.startLifetime;
                }
                else
                {
                    dunration = ps.startDelay + Mathf.Max(ps.duration, ps.startLifetime);
                }
                if (dunration > maxDuration)
                {
                    maxDuration = dunration;
                }
            }
            return maxDuration;
        }

        /// <summary>
        /// 设置 box Colliders 是否可用
        /// </summary>
        /// <param name="source">设置对象</param>
        /// <param name="enable">是否能够使用</param>
        public static void CollidersEnabled(Collider[] source, bool enable = true)
        {
            if (source == null) return;
            for (int i = 0, len = source.Length; i < len; i++) source[i].enabled = enable;
        }

    }

}