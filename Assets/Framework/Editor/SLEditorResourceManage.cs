using System.IO;
using LGame.LDebug;
using UnityEditor;
using UnityEngine;

namespace LGame.LEditor
{

    /// <summary>
    /// 管理和操作编辑器下的资源
    /// </summary>
    public class SLEditorResourceManage
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
        /// 将当前路径下的所有文件保存到另一个地方
        /// 
        /// 循环拷贝文件
        /// </summary>
        private static void LoopCopyFile(string sourcePath, string savePath)
        {
            DirectoryInfo info = new DirectoryInfo(sourcePath);

            DirectoryInfo[] childInfo = info.GetDirectories();
            for (int i = 0, len = childInfo.Length; i < len; i++)
            {
                DirectoryInfo dinfo = childInfo[i];
                string folder = string.Format("{0}/{1}", savePath, dinfo.Name);
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                LoopCopyFile(string.Format("{0}/{1}", sourcePath, dinfo.Name), folder);
            }

            FileInfo[] fileInfo = info.GetFiles();
            for (int j = 0, jLen = fileInfo.Length; j < jLen; j++)
            {
                FileInfo finfo = fileInfo[j];
                if (finfo.Extension == ".meta") continue;
                string dpath = string.Format("{0}/{1}", savePath, finfo.Name);
                Debug.Log(dpath);
                File.Copy(finfo.FullName, dpath, true);
            }
        }

        /// <summary>
        /// 
        /// 删除当前路径下的所有文件
        /// 
        /// 循环删除文件
        /// </summary>
        /// <param name="sourcePath"></param>
        public static void LoopDeleteFile(string sourcePath)
        {
            if (!Directory.Exists(sourcePath)) return;

            DirectoryInfo info = new DirectoryInfo(sourcePath);

            FileInfo[] fileInfo = info.GetFiles();
            for (int j = 0, jLen = fileInfo.Length; j < jLen; j++)
            {
                fileInfo[j].Delete();
            }

            DirectoryInfo[] childInfo = info.GetDirectories();
            for (int i = 0, len = childInfo.Length; i < len; i++)
            {
                DirectoryInfo dinfo = childInfo[i];
                string folder = string.Format("{0}/{1}", sourcePath, dinfo.Name);
                LoopDeleteFile(folder);
                dinfo.Delete();
            }
        }

        /// <summary>
        /// 打包ui资源
        /// 
        /// 例如 Prefab
        /// </summary>
        [MenuItem("Assets/LMenu/UI")]
        public static void PackUiResource()
        {
            // 获得打包保存路径
            string savePath = EditorUtility.OpenFolderPanel("package file path", "", "");
            SLConsole.WriteError("save path = " + savePath);

            // 得到打包路径
            Object obj = Selection.activeObject;
            string packPath = string.Format("{0}/{1}.data", savePath, GetSelectionName(obj));
            SLConsole.WriteError("pack path = " + packPath);

            // 得到选择的ui，如果是文件夹 返回下层所以ui
            Object[] selects = Selection.GetFiltered(typeof(Object),
                SelectionMode.Assets | SelectionMode.DeepAssets | SelectionMode.OnlyUserModifiable);
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
        /// 打包场景资源
        /// 
        /// 例如 Scene
        /// </summary>
        [MenuItem("Assets/LMenu/Scene")]
        public static void PackSceneResource()
        {
            // 获得打包保存路径
            string savePath = EditorUtility.OpenFolderPanel("package scene file path", "", "");
            SLConsole.WriteError("save path = " + savePath);

            // 得到打包路径
            Object obj = Selection.activeObject;
            if (obj == null)
            {
                SLConsole.WriteError("pack select file is empty");
                return;
            }
            //选择文件的路径（相对于 asset 文件夹）
            string selectPath = AssetDatabase.GetAssetPath(obj);
            if (string.IsNullOrEmpty(selectPath) || !selectPath.Contains(".unity")) return;
            string sceneName = GetSelectionName(obj);
            string packPath = string.Format("{0}/{1}.data", savePath, sceneName);
            SLConsole.WriteError("scene package path = " + packPath);
            // 打包场景
            string[] scenes = { selectPath };
            //打包  
            BuildPipeline.BuildPlayer(scenes, packPath, BuildTarget.Android, BuildOptions.BuildAdditionalStreamedScenes);
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 拷贝打apk包需要的资源文件
        /// 
        /// 内容资源
        /// </summary>
        [MenuItem("Assets/LMenu/copy apk resource")]
        public static void CopyApkPackageResource()
        {
            // 拷贝资源的源路径
            string sourcePath = EditorUtility.OpenFolderPanel("copy resource root folder", "", "");
            if (string.IsNullOrEmpty(sourcePath))
            {
                SLConsole.WriteError("copy resource root path is null ! (sourcePath = null)");
                return;
            }
            // 拷贝资源的目标路径
            string targetPath = Application.streamingAssetsPath + "/Assets";
            if (Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
            LoopCopyFile(sourcePath, targetPath);

            AssetDatabase.Refresh();
        }
    }

}