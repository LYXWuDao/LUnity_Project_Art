using System;
using System.Collections.Generic;
using LGame.LBase;
using LGame.LCommon;
using LGame.LDebug;
using LGame.LSource;
using UnityEngine;

namespace LGame.LScenes
{

    /***
     * 
     * 场景管理
     * 
     * 场景只会打开一个，没打开一个就会关闭另一个场景
     * 
     */

    public class SLScenesManage : ATLManager<CLSceneBehaviour>
    {

        /// <summary>
        /// 当前场景的名字
        /// </summary>
        private static string CurrentName = string.Empty;

        /// <summary>
        /// 当前脚本
        /// </summary>
        private static string CurrentScript = string.Empty;

        /// <summary>
        /// 加载完成回调
        /// </summary>
        private static Action LoadFinish = null;

        /// <summary>
        /// 场景加载完成时回调
        /// </summary>
        /// <param name="sceneName">加载的场景名字</param>
        /// <param name="obj">场景的对象</param>
        private static void LoadSceneFinish(string sceneName, UnityEngine.Object obj)
        {
            CurrentName = string.Empty;
            if (string.IsNullOrEmpty(sceneName))
            {
                SLDebugHelper.WriteError("打开的场景名字为空 sceneName = string.Empty");
                return;
            }

            if (obj == null)
            {
                SLDebugHelper.WriteError("资源加载失败!");
                return;
            }

            // 跳转场景
            Application.LoadLevel(sceneName);

            // 场景加载完成回调
            if (LoadFinish != null)
            {
                LoadFinish();
                LoadFinish = null;
            }

            GameObject root = GameObject.Find("SceneRoot");
            if (root == null)
            {
                SLDebugHelper.WriteError("场景不存在 SceneRoot 根节点!");
                return;
            }

            CLSceneBehaviour scene = SLCompHelper.GetComponent<CLSceneBehaviour>(root);

            // 为场景加载绑定行为组件
            if (scene == null && !string.IsNullOrEmpty(CurrentScript))
            {
                if (SLConfig.IsLuaWindow)
                {
                    scene = SLCompHelper.AddComponet<CLSceneLuaBehaviour>(root);
                    // todo: 处理lua 初始化的问题
                }
                else
                {
                    scene = root.AddComponent(CurrentScript) as CLSceneBehaviour;
                }
            }
            if (scene == null) scene = SLCompHelper.FindComponet<CLSceneBehaviour>(root);

            scene.OnEnterScene();
            Add<SLScenesManage>(sceneName, scene);
            CurrentScript = string.Empty;
            CurrentName = sceneName;
        }

        /// <summary>
        /// 验证当前场景是否打开
        /// </summary>
        /// <param name="sceneName">场景的名字</param>
        public static bool VerifyOpenScene(string sceneName)
        {
            return !string.IsNullOrEmpty(CurrentName) && CurrentName == sceneName;
        }

        /// <summary>
        /// 得到当前场景
        /// </summary>
        public static CLSceneBehaviour CurrentScene
        {
            get
            {
                if (string.IsNullOrEmpty(CurrentName)) return null;
                return Find<SLScenesManage>(CurrentName);
            }
        }

        /// <summary>
        /// 打开场景
        /// </summary>
        /// <param name="sceneName">场景的名字</param>
        /// <param name="scenePath">场景的路径</param>
        /// <param name="sceneScript">场景的脚本</param>
        /// <param name="finish">场景加载完成</param>
        public static void OpenToScenes(string sceneName, string scenePath, string sceneScript, Action finish)
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                SLDebugHelper.WriteError("打开的场景名字为空 sceneName = string.Empty");
                return;
            }

            if (string.IsNullOrEmpty(scenePath))
            {
                SLDebugHelper.WriteError("场景加载路径为空， scenePath = string.Empty");
                return;
            }

            if (VerifyOpenScene(sceneName))
            {
                SLDebugHelper.WriteError("场景已经加载 sceneName = ", sceneName);
                return;
            }

            if (!string.IsNullOrEmpty(CurrentName))
            {
                // 当前已经打开一个场景
                CLSceneBehaviour curr = Find<SLScenesManage>(CurrentName);
                curr.OnLeaveScene();
            }

            CurrentName = sceneName;
            CurrentScript = sceneScript;
            LoadFinish = finish;
            LoadSourceEntity entity = SLManageSource.LoadAssetSource(sceneName, scenePath, LoadType.Object);
            LoadSceneFinish(sceneName, entity != null ? entity.LoadObj : null);
        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="sceneName">场景的名字</param>
        /// <param name="scenePath">场景的路径</param>
        /// <param name="sceneScript">场景的脚本</param>
        /// <param name="finish">场景加载完成</param>
        public static void AsyncOpenToScenes(string sceneName, string scenePath, string sceneScript, Action finish)
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                SLDebugHelper.WriteError("打开的场景名字为空 sceneName = string.Empty");
                return;
            }

            if (string.IsNullOrEmpty(scenePath))
            {
                SLDebugHelper.WriteError("场景加载路径为空， scenePath = string.Empty");
                return;
            }

            if (VerifyOpenScene(sceneName))
            {
                SLDebugHelper.WriteError("场景已经加载 sceneName = ", sceneName);
                return;
            }

            if (!string.IsNullOrEmpty(CurrentName))
            {
                // 当前已经打开一个场景
                CLSceneBehaviour curr = Find<SLScenesManage>(CurrentName);
                curr.OnLeaveScene();
            }

            CurrentName = sceneName;
            CurrentScript = sceneScript;
            LoadFinish = finish;
            SLManageSource.AsyncLoadAssetSource(sceneName, scenePath, LoadType.Object, delegate(LoadSourceEntity entity)
            {
                LoadSceneFinish(sceneName, entity != null ? entity.LoadObj : null);
            });
        }

    }

}

