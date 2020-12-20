using UnityEngine;
using System.IO;
using System;
using UnityEditor;

namespace QFramework
{
    public partial class Exporter
    {
        [MenuItem("QFramework/Framework/Editor/导出 UnityPackage %e", false, 1)]
        private static void MenuClicked() {
            var generatePackageName = GenerateUnityPackageName();
            EditorUtil.ExportPackage("Assets/QFramework", generatePackageName + ".unitypackage");
            EditorUtil.OpenInFolder(Path.Combine(Application.dataPath, "../"));
        }

        private static string GenerateUnityPackageName() {
            return "QFramework_" + DateTime.Now.ToString("yyyyMMdd_HH");
        }
    }
}