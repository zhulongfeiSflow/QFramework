using UnityEngine;

namespace QFramework
{
    public class ResKitUtil
    {
        public static string FullPathForAssetBundle(string assetBundleName)
        {
            var hotUpdateState = HotUpdateMgr.Instance.State;

            if (hotUpdateState == HotUpdateState.NeverUpdate || hotUpdateState == HotUpdateState.Overrided)
            {
                return Application.streamingAssetsPath + "/AssetBundles/" + GetPlatformName() + "/" + assetBundleName;
            }

            return PersistentAssetBundlesFolder + assetBundleName;
        }

        public static string PersistentAssetBundlesFolder
        {
            get { return Application.persistentDataPath + "/AssetBundles/"; }
        }

        public static string GetPlatformName()
        {
#if UNITY_EDITOR
            return GetPlatformName(UnityEditor.EditorUserBuildSettings.activeBuildTarget);
#else
                return GetPlatformName(Application.platform);
#endif
        }

#if UNITY_EDITOR
        static string GetPlatformName(UnityEditor.BuildTarget buildTarget)
        {
            switch (buildTarget)
            {
                case UnityEditor.BuildTarget.StandaloneWindows:
                case UnityEditor.BuildTarget.StandaloneWindows64:
                    return "Windows";
                case UnityEditor.BuildTarget.iOS:
                    return "ios";
                case UnityEditor.BuildTarget.Android:
                    return "Android";
                case UnityEditor.BuildTarget.StandaloneLinux64:
                    return "Linux";
                case UnityEditor.BuildTarget.StandaloneOSX:
                    return "OSX";
                case UnityEditor.BuildTarget.WebGL:
                    return "WebGL";
                default:
                    return null;
            }
        }
#endif
        static string GetPlatformName(RuntimePlatform runtimePlatform)
        {
            switch (runtimePlatform)
            {
                case RuntimePlatform.WebGLPlayer:
                    return "WebGL";
                case RuntimePlatform.OSXPlayer:
                    return "OSX";
                case RuntimePlatform.WindowsPlayer:
                    return "Windows";
                case RuntimePlatform.IPhonePlayer:
                    return "ios";
                case RuntimePlatform.Android:
                    return "Android";
                default:
                    return null;
            }
        }
    }
}