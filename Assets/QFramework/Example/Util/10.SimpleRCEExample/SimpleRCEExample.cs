using UnityEngine;

namespace QFramework
{
    public class SimpleRCEExample : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/Util/10.SimpleRCEExample", false, 10)]
        private static void MenuItem()
        {
            var room = new Room();

            room.EnterPeople();
            room.EnterPeople();
            room.EnterPeople();

            room.LeavePeople();
            room.LeavePeople();
            room.LeavePeople();

            room.EnterPeople();
        }
#endif

        class Light
        {
            public void Open()
            {
                Debug.Log("灯打开了");
            }

            public void Close()
            {
                Debug.Log("灯关闭了");
            }
        }

        class Room : SimpleRC
        {
            Light mLight = new Light();

            public void EnterPeople()
            {
                if (RefCount == 0)
                {
                    mLight.Open();
                }

                Retain();
                Debug.LogFormat("一个人进入房间,当前房间有{0}", RefCount);
            }

            public void LeavePeople()
            {
                Release();
                Debug.LogFormat("一个人离开房间,当前房间有{0}", RefCount);
            }

            protected override void OnZeroRef()
            {
                base.OnZeroRef();
                mLight.Close();
            }
        }
    }
}