using UnityEngine;

namespace QFramework
{
    public static partial class TransformExtensions
    {
        /// <summary>
        /// 重置操作
        /// </summary>
        /// <param name="trans">Trans.</param>
        public static void Identity(this MonoBehaviour monoBehaviour) {
            monoBehaviour.transform.Identity();
        }

        /// <summary>
        /// 重置操作
        /// </summary>
        /// <param name="trans">Trans.</param>
        public static void Identity(this Transform trans) {
            trans.localPosition = Vector3.zero;
            trans.localScale = Vector3.one;
            trans.localRotation = Quaternion.identity;
        }
        public static void SetLocalPosX(this Transform transform, float x) {
            var localPos = transform.localPosition;
            localPos.x = x;
            transform.localPosition = localPos;
        }
        public static void SetLocalPosY(this Transform transform, float y) {
            var localPos = transform.localPosition;
            localPos.y = y;
            transform.localPosition = localPos;
        }
        public static void SetLocalPosZ(this Transform transform, float z) {
            var localPos = transform.localPosition;
            localPos.z = z;
            transform.localPosition = localPos;
        }
        public static void SetLocalPosXY(this Transform transform, float x, float y) {
            var localPos = transform.localPosition;
            localPos.x = x;
            localPos.y = y;
            transform.localPosition = localPos;
        }
        public static void SetLocalPosXZ(this Transform transform, float x, float z) {
            var localPos = transform.localPosition;
            localPos.x = x;
            localPos.z = z;
            transform.localPosition = localPos;
        }
        public static void SetLocalPosYZ(this Transform transform, float y, float z) {
            var localPos = transform.localPosition;
            localPos.y = y;
            localPos.z = z;
            transform.localPosition = localPos;
        }
        public static void AddChild(this Transform transform, Transform childTrans) {
            childTrans.SetParent(transform);
        }
    }
}
