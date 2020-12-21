using UnityEngine;

namespace QFramework
{
    public class ResolutionCheckExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/Util/3.屏幕宽⾼⽐判断", false, 3)]
#endif
        private static void MenuClicked() {
            Debug.Log(ResolutionCheck.IsPadResolution() ? "是 Pad 分辨率" : "不是 Pad 分辨率");
            Debug.Log(ResolutionCheck.IsPhoneResolution() ? "是 Phone 分辨率" : "不是 Phone 分辨率");
            Debug.Log(ResolutionCheck.IsiPhoneXResolution() ? "是 iPhone X 分辨率" : "不是 iPhone X 分辨率");
        }
        
    }
}
