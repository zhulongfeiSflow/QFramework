
namespace QFramework
{
    public partial class EditorUtilExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/Util/2.MenuItem 复⽤", false, 2)]
        private static void MenuClicked() {
            EditorUtil.CallMenuItem("QFramework/Example/1.复制⽂本到剪切板");
        }
#endif
    }
}