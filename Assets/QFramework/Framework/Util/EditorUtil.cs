using UnityEngine;
namespace QFramework
{
    public partial class EditorUtil
    {
#if UNITY_EDITOR
        public static void CallMenuItem(string menuPath) {
            UnityEditor.EditorApplication.ExecuteMenuItem(menuPath);
        }
        public static void OpenInFolder(string folderPath) {
            Application.OpenURL("file:///" + folderPath);
        }
        public static void ExportPackage(string assetPathName, string fileName) {
            UnityEditor.AssetDatabase.ExportPackage(assetPathName, fileName, UnityEditor.ExportPackageOptions.Recurse);
        }
#endif
    }
}