using UnityEngine;

namespace QFramework
{
    public partial class ResolutionCheck
	{
	    /// <summary>
	    /// 获取屏幕宽??
	    /// </summary>
	    /// <returns></returns>
	    public static float GetAspectRatio() {
	        return Screen.width > Screen.height ? (float)Screen.width /
	        Screen.height : (float)Screen.height / Screen.width;
	    }
	    /// <summary>
	    /// 是否是 Pad 分辨率 4 : 3 
	    /// </summary>
	    /// <returns></returns>
	    public static bool IsPadResolution() {
	        var aspect = GetAspectRatio();
	        return aspect > 4.0f / 3 - 0.05 && aspect < 4.0f / 3 + 0.05;
	    }
	    /// <summary>
	    /// 是否是?机分辨率 16:9
	    /// </summary>
	    /// <returns></returns>
	    public static bool IsPhoneResolution() {
	        var aspect = GetAspectRatio();
	        return aspect > 16.0f / 9 - 0.05 && aspect < 16.0f / 9 + 0.05;
	    }
	    /// <summary>
	    /// 是否是iPhone X 分辨率 2436:1125
	    /// </summary>
	    /// <returns></returns>
	    public static bool IsiPhoneXResolution() {
	        var aspect = GetAspectRatio();
	        return aspect > 2436.0f / 1125 - 0.05 && aspect < 2436.0f / 1125 + 0.05;
	    }
	}
}
