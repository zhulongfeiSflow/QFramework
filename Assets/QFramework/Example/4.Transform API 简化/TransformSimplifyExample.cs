using UnityEngine;
namespace QFramework
{
    public class TransformSimplifyExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/4.Transform API 简化",
        false, 4)]
#endif
        private static void MenuClicked1() {
            var transform = new GameObject("transform").transform;
            TransformExtensions.SetLocalPosX(transform, 5.0f);
            TransformExtensions.SetLocalPosY(transform, 5.0f);
            TransformExtensions.SetLocalPosZ(transform, 5.0f);
            TransformExtensions.Identity(transform);

            var childTrans = new GameObject("Child").transform;
            TransformExtensions.AddChild(transform, childTrans);
        }
    }
}