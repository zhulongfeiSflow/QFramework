using UnityEngine;
namespace QFramework
{
    public class MonoSingletonExample : MonoSingleton<MonoSingletonExample>
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/17.MonoSingletonExample", false, 17)]
        private static void MenuClicked() {
            UnityEditor.EditorApplication.isPlaying = true;
        }
#endif
        //[RuntimeInitializeOnLoadMethod]
        private static void Example() {
            var initInstance = MonoSingletonExample.Instance;
            initInstance = MonoSingletonExample.Instance;
        }
    }
}