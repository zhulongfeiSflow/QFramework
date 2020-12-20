using System.Collections;
using UnityEngine;

namespace QFramework
{
    public class ResMgrExample : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/23.ResMgrExample", false, 23)]
        private static void MenuItem()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("ResMgrExample")
                .AddComponent<ResMgrExample>();
        }
#endif

        ResLoader resLoader = new ResLoader();

        private IEnumerator Start()
        {
            // yield return new WaitForSeconds(2.0f);
            //
            // resLoader.LoadSync<AudioClip>("coin");
            //
            // yield return new WaitForSeconds(2.0f);
            //
            // resLoader.LoadSync<AudioClip>("home");
            
            yield return new WaitForSeconds(2.0f);

            resLoader.LoadAsync<GameObject>("resources://HomePanel", homePanel =>
            {
                Debug.Log(homePanel.name);
                Debug.Log("loadedComplete:"+Time.time);
            });
            Debug.Log("after LoadAsync:"+Time.time);
            
            yield return new WaitForSeconds(1.0f);
            
            resLoader.LoadAsync<GameObject>("resources://HomePanel", homePanel =>
            {
                Debug.Log(homePanel.name);
                Debug.Log("loadedComplete2:"+Time.time);
            });
            Debug.Log("after LoadAsync2:"+Time.time);

            yield return new WaitForSeconds(5.0f);
            resLoader.ReleaseAll();
        }
    }
}