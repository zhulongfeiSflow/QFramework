using System.Collections.Generic;
using UnityEngine;

namespace QFramework
{
    public class ResMgr : MonoSingleton<ResMgr>
    {
        public List<Res> SharedLoadedReses = new List<Res>();

#if UNITY_EDITOR
        private void OnGUI()
        {
            if (Input.GetKey(KeyCode.F1))
            {
                GUILayout.BeginVertical(style: "box");
                SharedLoadedReses.ForEach(loadedRes =>
                {
                    GUILayout.Label(string.Format("Name:{0}, RefCount:{1}, State:{2}", loadedRes.Name, loadedRes.RefCount, loadedRes.State));
                });

                GUILayout.EndVertical();
            }
        }
#endif
    }
}