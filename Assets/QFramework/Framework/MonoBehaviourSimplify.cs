using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace QFramework
{
    public abstract partial class MonoBehaviourSimplify : MonoBehaviour
    {
        #region TransformSimplify
        public void Identity() {
            transform.Identity();
        }
        #endregion

        #region Timer
        public void Delay(float seconds, Action onFinished) {
            StartCoroutine(DelayCoroutine(seconds, onFinished));
        }

        private static IEnumerator DelayCoroutine(float seconds, Action onFinished) {
            yield return new WaitForSeconds(seconds);
            onFinished();
        }
        #endregion

        #region MsgDispatcher
        List<MsgRecord> mMsgRecorder = new List<MsgRecord>();
        private class MsgRecord
        {
            private static readonly Stack<MsgRecord> mMsgRecordPool = new Stack<MsgRecord>();
            public static MsgRecord Allocate(string msgName, Action<object> onMsgReceived) {
                MsgRecord retMsgRecord = null;

                retMsgRecord = mMsgRecordPool.Count > 0 ? mMsgRecordPool.Pop() : new MsgRecord();
                retMsgRecord.Name = msgName;
                retMsgRecord.OnMsgReceived = onMsgReceived;
                return retMsgRecord;
            }
            public void Recycle() {
                Name = null;
                OnMsgReceived = null;

                mMsgRecordPool.Push(this);
            }
            public string Name;
            public Action<object> OnMsgReceived;
        }
        protected void RegisterMsg(string msgName, Action<object> onMsgReceived) {
            MsgDispatcher.Register(msgName, onMsgReceived);

            mMsgRecorder.Add(MsgRecord.Allocate(msgName, onMsgReceived));
        }
        protected void UnRegisterMsg(string msgName) {
            var selectedRecords = mMsgRecorder.FindAll(recorder => recorder.Name == msgName);
            selectedRecords.ForEach((selectRecord) =>
            {
                MsgDispatcher.UnRegister(selectRecord.Name, selectRecord.OnMsgReceived);
                mMsgRecorder.Remove(selectRecord);
            });
            selectedRecords.Clear();
        }

        protected void UnRegisterMsg(string msgName, Action<object> onMsgReceived) {
            var selectedRecords = mMsgRecorder.FindAll(recorder => recorder.Name == msgName && recorder.OnMsgReceived == onMsgReceived);

            selectedRecords.ForEach((selectRecord) =>
            {
                MsgDispatcher.UnRegister(selectRecord.Name, selectRecord.OnMsgReceived);
                mMsgRecorder.Remove(selectRecord);
            });
            selectedRecords.Clear();
        }

        protected void SendMsg(string msgName, object data) {
            MsgDispatcher.Send(msgName, data);
        }

        private void OnDestroy() {
            OnBeforeDestroy();
            foreach (var msgRecord in mMsgRecorder) {

                MsgDispatcher.UnRegister(msgRecord.Name, msgRecord.OnMsgReceived);
                msgRecord.Recycle();
            }

            mMsgRecorder.Clear();
        }
        protected abstract void OnBeforeDestroy();
        #endregion MsgDispatcher
    }
}