using UnityEngine;
namespace QFramework
{
    public class GameObjectSimplifyExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/Util/6.GameObejct API 简化", false, 6)]
#endif
        private static void MenuClicked() {
            var gameObject = new GameObject();
            GameObjectExtension.Hide(gameObject);
            GameObjectExtension.Hide(gameObject.transform);
        }
    }
}