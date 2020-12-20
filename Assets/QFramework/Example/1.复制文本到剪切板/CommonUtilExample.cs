using UnityEngine;

namespace QFramework
{
    public class CommonUtilExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/1.复制⽂本到剪切板", false,
        1)]
#endif
        private static void MenuClicked2() {
            CommonUtil.CopyText("要复制的关键字");
        }
	}
}