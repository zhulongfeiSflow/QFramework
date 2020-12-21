using UnityEngine;

namespace QFramework
{
    public class DelayWithCoroutine : MonoBehaviourSimplify
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/1.定时功能", false, 1)]
        private static void MenuClickd() {
            
            UnityEditor.EditorApplication.isPlaying = true;
            new GameObject("DelayWithCoroutine")
                .AddComponent<DelayWithCoroutine>();
        }
        private void Start() {
            Delay(5.0f, () =>
            {
                UnityEditor.EditorApplication.isPlaying = false;
            });
        }
#endif 
        protected override void OnBeforeDestroy() {

        }
    }
}