using UnityEditor;
using UnityEngine;

namespace QFramework
{
    public class SimulationModeMenu : MonoBehaviour
    {
        private const string kSimulationModePath = "QFramework/Framework/ResKit/Simulation Mode";

        private static bool SimulationMode
        {
            get => ResMgr.SimulationMode;
            set => ResMgr.SimulationMode = value;
        }

        [MenuItem(kSimulationModePath)]
        private static void ToggleSimulationMode()
        {
            SimulationMode = !SimulationMode;
        }

        [MenuItem(kSimulationModePath, true)]
        public static bool ToggleSimulationModeValidate()
        {
            Menu.SetChecked(kSimulationModePath, SimulationMode);
            return true;
        }
    }
}