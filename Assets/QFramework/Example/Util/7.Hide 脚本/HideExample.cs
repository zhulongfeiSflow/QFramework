using UnityEngine;
namespace QFramework
{
    public class HideExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/Util/7.Hide 脚本", false, 7)]
        private static void MenuClicked() {
            UnityEditor.EditorApplication.isPlaying = true;
            var gameObj = new GameObject("Hide");
            gameObj.AddComponent<Hide>();
        }
#endif
    }
}