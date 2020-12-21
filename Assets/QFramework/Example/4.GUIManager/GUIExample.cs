using UnityEngine;
namespace QFramework
{
    public class GUIExample : MonoBehaviourSimplify
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/4.GUIManager", false, 4)]
        private static void MenuClicked() {
            UnityEditor.EditorApplication.isPlaying = true;
            new GameObject("GUIExample").AddComponent<GUIExample>();
        }
#endif
        private void Start() {
            GUIManager.SetResolution(1280, 720, 0);
            GUIManager.LoadPanel("HomePanel", UILayer.Common);
            Delay(3.0f, () =>
            {
                GUIManager.UnLoadPanel("HomePanel");
            });
        }
        protected override void OnBeforeDestroy() { }
    }
}