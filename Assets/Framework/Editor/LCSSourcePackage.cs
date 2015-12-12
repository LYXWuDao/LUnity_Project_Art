using System.Collections.Generic;
using System.IO;
using LGame.LBehaviour;
using LGame.LDebug;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class LCSSourcePackage : LABehaviour
{

    /// <summary>
    /// 返回选择的对象的名字
    /// 
    /// 如果是文件夹就返回文件夹名字
    /// </summary>
    /// <param name="selection">选择的对象</param>
    /// <returns></returns>
    private static string GetSelectionName(Object selection)
    {
        if (selection == null) return string.Empty;
        string selectPath = AssetDatabase.GetAssetPath(selection);
        return Path.GetFileNameWithoutExtension(selectPath);
    }

    /// <summary>
    /// 打包ui资源
    /// 
    /// 例如 Prefab
    /// </summary>
    [MenuItem("Assets/LMenu/UI")]
    public static void UiSourcePackage()
    {
        // 获得打包保存路径
        string savePath = EditorUtility.OpenFolderPanel("package file path", "", "");
        LCSConsole.WriteError("save path = " + savePath);

        // 得到打包路径
        Object obj = Selection.activeObject;
        string packPath = string.Format("{0}/{1}.data", savePath, GetSelectionName(obj));
        LCSConsole.WriteError("pack path = " + packPath);

        // 得到选择的ui，如果是文件夹 返回下层所以ui
        Object[] selects = Selection.GetFiltered(typeof(Object), SelectionMode.Assets | SelectionMode.DeepAssets | SelectionMode.OnlyUserModifiable);
        BuildAssetBundleOptions bundleOptions = BuildAssetBundleOptions.DeterministicAssetBundle |
                                                BuildAssetBundleOptions.CollectDependencies |
                                                BuildAssetBundleOptions.CompleteAssets |
                                                BuildAssetBundleOptions.DisableWriteTypeTree;

        BuildPipeline.PushAssetDependencies();
        if (selects.Length == 1)
        {
            // 打包  
            BuildPipeline.BuildAssetBundle(obj, null, packPath, bundleOptions, BuildTarget.Android);
        }
        else
        {
            // 打包  
            BuildPipeline.BuildAssetBundle(null, selects, packPath, bundleOptions, BuildTarget.Android);
        }
        BuildPipeline.PopAssetDependencies();

        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 资源场景打包
    /// 
    /// 例如 Scene
    /// </summary>
    [MenuItem("Assets/LMenu/Scene")]
    public static void SceneSourcePackage()
    {
        // 获得打包保存路径
        string savePath = EditorUtility.OpenFolderPanel("package file path", "", "");
        LCSConsole.WriteError("save path = " + savePath);

        // 得到打包路径
        Object obj = Selection.activeObject;
        string sceneName = GetSelectionName(obj);
        string packPath = string.Format("{0}/{1}.scene", savePath, sceneName);
        LCSConsole.WriteError("pack path = " + packPath);

        string[] scenes = { string.Format("Assets/Scenes/{0}.unity", sceneName) };
        //打包  
        BuildPipeline.BuildPlayer(scenes, packPath, BuildTarget.Android, BuildOptions.BuildAdditionalStreamedScenes);
        AssetDatabase.Refresh();
    }

}
