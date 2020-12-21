using UnityEngine;
namespace QFramework
{
    public class MsgDispatcherExample : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/Util/8.简易消息机制", false, 8)]
#endif
        private static void MenuClicked() {
            // 全部清空，确保测试有效
            MsgDispatcher.UnRegisterAll("消息1");
            MsgDispatcher.Register("消息1", OnMsgReceived);
            MsgDispatcher.Register("消息1", OnMsgReceived);
            MsgDispatcher.Send("消息1", "hello world");
            MsgDispatcher.UnRegister("消息1", OnMsgReceived);
            MsgDispatcher.Send("消息1", "hello");
        }
        private static void OnMsgReceived(object data) {
            Debug.LogFormat("消息1:{0}", data);
        }
    }
}