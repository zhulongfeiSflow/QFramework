using UnityEngine;

namespace QFramework
{
    public class UiXXXPanel : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/9.UIXXXPanel", false, 9)]
        private static void MenuItem()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("UIXXXPanel")
                .AddComponent<UiXXXPanel>()
                .gameObject.AddComponent<UIYYYPanel>();
        }
#endif

        private ResLoader _mResLoader = new ResLoader();

        // Use this for initialization
        private void Start()
        {
            var coinClip = _mResLoader.LoadSync<AudioClip>("resources://coin");

            var homeClip = _mResLoader.LoadSync<AudioClip>("resources://home");

            var bgClip = _mResLoader.LoadSync<AudioClip>("resources://coin");

            OtherFunction();
        }

        private void OtherFunction()
        {
            var bgClip = _mResLoader.LoadSync<AudioClip>("resources://coin");
        }

        private void OnDestroy()
        {
            _mResLoader.ReleaseAll();
            _mResLoader = null;
        }
    }

    public class UIYYYPanel : MonoBehaviour
    {
        private ResLoader _mResLoader = new ResLoader();

        // Use this for initialization
        private void Start()
        {
            var coinClip = _mResLoader.LoadSync<AudioClip>("resources://coin");

            var homeClip = _mResLoader.LoadSync<AudioClip>("resources://home");

            var bgClip = _mResLoader.LoadSync<AudioClip>("resources://coin");

            OtherFunction();
        }

        private void OtherFunction()
        {
            var bgClip = _mResLoader.LoadSync<AudioClip>("resources://coin");
        }

        private void OnDestroy()
        {
            _mResLoader.ReleaseAll();
            _mResLoader = null;
        }
    }
}