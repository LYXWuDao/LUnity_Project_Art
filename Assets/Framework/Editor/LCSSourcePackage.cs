using System.IO;
using LGame.LBehaviour;
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
    [MenuItem("Assets/LYXMenu/UI")]
    public static void UiSourcePackage()
    {
        // 得到打包路径
        Object obj = Selection.activeObject;
        string packPath = string.Format(Application.dataPath + "/SourceAssets/UI/{0}.data", GetSelectionName(obj));

        // 得到选择的ui，如果是文件家 返回下层所以ui
        Object[] selects = Selection.GetFiltered(typeof(Object), SelectionMode.Assets | SelectionMode.DeepAssets | SelectionMode.OnlyUserModifiable);
        BuildAssetBundleOptions bundleOptions = BuildAssetBundleOptions.CollectDependencies |
                                                BuildAssetBundleOptions.CompleteAssets;
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
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 资源场景打包
    /// 
    /// 例如 Scene
    /// </summary>
    [MenuItem("Assets/LYXMenu/Scene")]
    public static void SceneSourcePackage()
    {
        // 得到打包路径
        Object obj = Selection.activeObject;
        string sceneName = GetSelectionName(obj);
        string packPath = string.Format(Application.dataPath + "/SourceAssets/Scenes/{0}.scene", sceneName);
        string[] scenes = { string.Format("Assets/Scenes/{0}.unity", sceneName) };
        //打包  
        BuildPipeline.BuildPlayer(scenes, packPath, BuildTarget.Android, BuildOptions.BuildAdditionalStreamedScenes);
        AssetDatabase.Refresh();
    }

}
