using System.Collections.Generic;
using UnityEngine;

namespace QFramework
{
    public class ResMgr : MonoSingleton<ResMgr>
    {
        private const string kSimulationMode = "simulation mode";

#if UNITY_EDITOR
        private static int mSimulationMode = -1;

        public static bool SimulationMode
        {
            get
            {
                if (mSimulationMode == -1)
                {
                    mSimulationMode = UnityEditor.EditorPrefs.GetBool(kSimulationMode, true) ? 1 : 0;
                }

                return mSimulationMode != 0;
            }
            set
            {
                mSimulationMode = value ? 1 : 0;
                UnityEditor.EditorPrefs.SetBool(kSimulationMode, value);
            }
        }
#endif
        public static bool IsSimulationModeLogic
        {
            get
            {
#if UNITY_EDITOR
                return SimulationMode;
#endif
                return false;
            }
        }

        public List<Res> SharedLoadedReses = new List<Res>();

#if UNITY_EDITOR
        private void OnGUI()
        {
            if (Input.GetKey(KeyCode.F1))
            {
                GUILayout.BeginVertical(style: "box");
                SharedLoadedReses.ForEach(loadedRes =>
                {
                    GUILayout.Label(string.Format("Name:{0}, RefCount:{1}, State:{2}", loadedRes.Name,
                        loadedRes.RefCount, loadedRes.State));
                });

                GUILayout.EndVertical();
            }
        }
#endif
    }
}