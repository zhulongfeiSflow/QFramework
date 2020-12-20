using UnityEngine;

namespace QFramework
{
    public class SingletonExample : Singleton<SingletonExample>
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/16.SingletonExample", false, 16)]
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