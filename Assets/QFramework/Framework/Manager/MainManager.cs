using UnityEngine;

namespace QFramework
{
	public enum EnvironmentMode
	{
	    Developing,
	    Test,
	    Production
	}
	
	public abstract class MainManager : MonoBehaviour
	{
	    public EnvironmentMode Mode;
        private static EnvironmentMode mSharedMode;
        private static bool mModeSetted = false;

        private void Start() {
            if (!mModeSetted) {
                mSharedMode = Mode;
                mModeSetted = true;
            }

            switch (mSharedMode) {
	            case EnvironmentMode.Developing:
	                LaunchInDevelopingMode();
	                break;
	            case EnvironmentMode.Test:
	                LaunchInTestMode();
	                break;
	            case EnvironmentMode.Production:
	                LaunchInProductionMode();
	                break;
	        }
	    }
	    protected abstract void LaunchInDevelopingMode();
	    protected abstract void LaunchInProductionMode();
	    protected abstract void LaunchInTestMode();
	}
}