using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QFramework
{
    public enum UILayer
    {
        Bg,
        Common,
        Top
    }
    public class GUIManager : MonoBehaviour
    {
        private static GameObject mPrivateUIRoot;

        public static GameObject UIRoot {
            get {
                if (mPrivateUIRoot == null) {
                    var uirootPrefab = Resources.Load<GameObject>("UIRoot");
                    mPrivateUIRoot = Instantiate(uirootPrefab);
                    mPrivateUIRoot.name = "UIRoot";
                }
                return mPrivateUIRoot;
            }
        }

        public static void SetResolution(float width, float height, float matchWidthOrHeight) {
            var canvasScaler = UIRoot.GetComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.referenceResolution = new Vector2(width, height);
            canvasScaler.matchWidthOrHeight = matchWidthOrHeight;
        }

        private static Dictionary<string, GameObject> mPanelsDict = new Dictionary<string, GameObject>();

        public static void UnLoadPanel(string panelName) {
            if (mPanelsDict.ContainsKey(panelName)) {
                Destroy(mPanelsDict[panelName]);
            }
        }

        public static GameObject LoadPanel(string panelName, UILayer layer) {
            var panelPrefab = Resources.Load<GameObject>(panelName);
            var panel = Instantiate(panelPrefab);
            panel.name = panelName;

            mPanelsDict.Add(panelName, panel);

            switch (layer) {
                case UILayer.Bg:
                    panel.transform.SetParent(UIRoot.transform.Find("Bg"));
                    break;
                case UILayer.Common:
                    panel.transform.SetParent(UIRoot.transform.Find("Common"));
                    break;
                case UILayer.Top:
                    panel.transform.SetParent(UIRoot.transform.Find("Top"));
                    break;
            }

            var panelRectTrans = panel.transform as RectTransform;

            panelRectTrans.offsetMin = Vector2.zero;
            panelRectTrans.offsetMax = Vector2.zero;
            panelRectTrans.anchoredPosition3D = Vector3.zero;
            panelRectTrans.anchorMin = Vector2.zero;
            panelRectTrans.anchorMax = Vector2.one;

            return panel;
        }
    }
}