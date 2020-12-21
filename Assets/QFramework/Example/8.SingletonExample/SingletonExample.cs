using UnityEngine;

namespace QFramework
{
    public class SingletonExample : Singleton<SingletonExample>
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/8.SingletonExample", false, 8)]
        private static void MenuClicked() {
            var initInstance = Instance;
            initInstance = Instance;
        }
#endif
        private SingletonExample() {
            Debug.Log("SingletonExample ctor");
        }
    }
}