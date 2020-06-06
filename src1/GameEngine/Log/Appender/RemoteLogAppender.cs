using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Threading;
using UnityEngine;

/***
 * RomateLogAppender.cs
 *
 * @author administrator
 */
namespace GameEngine
{
    public class RomateLogAppender : AbsLogAppender
    {
        // ������Ϣ������
        private Queue<NameValueCollection> mSendQueue;

        // HttpClient
        private WebClient mClient;

        // ������־��Ϣ�߳�
        private Thread mThread = null;

        public RomateLogAppender()
        {
#if !UNITY_EDITOR
            mSendQueue = new Queue<NameValueCollection>();

            mClient = new WebClient();

            mThread = new Thread(new ThreadStart(ProcessMsgToServer));
            mThread.IsBackground = true;
            mThread.Start();
#endif
        }

        protected override void OnWrite(string msg, string stackTrace)
        {
#if !UNITY_EDITOR

            if (LogType != LoggerType.Error) {
                return;
            }

            AddLogMessage(msg, stackTrace);
#endif
        }

        private void AddLogMessage(string message, string stack)
        {
            NameValueCollection namevalue = new NameValueCollection();
            namevalue["Time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            namevalue["Type"] = LogType.ToString();
            namevalue["Condition"] = message;
            namevalue["StackTrace"] = stack;
            namevalue["OperatingSystem"] = SystemInfo.operatingSystem;
            namevalue["Device"] = SystemInfo.deviceName;
            namevalue["DeviceIP"] = GetLocalIp();
            namevalue["DeviceModel"] = SystemInfo.deviceModel;

            mSendQueue.Enqueue(namevalue);
        }

        /// <summary>
        /// ���ͻ���������
        /// </summary>
        /// <returns></returns>
        private void ProcessMsgToServer()
        {
            if (mSendQueue == null) {
                return;
            }
            while (true) {
                if (mSendQueue.Count > 0) {
                    UploadData(mSendQueue.Dequeue());
                }
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// �ϴ�����
        /// </summary>
        /// <param name="value"></param>
        private void UploadData(NameValueCollection value)
        {
            try {
                if (mClient == null) {
                    return;
                }
                mClient.UploadValuesAsync(new Uri(LoggerConf.RomateLogURL), "POST", value);
            } catch (Exception ex) {
                GameLog.Exception(new Exception("Upload data exception." + ex.Message));
            }
        }

        /// <summary>
        /// ��ȡ����IP
        /// </summary>
        /// <returns></returns>
        private string GetLocalIp()
        {
            string hostName = Dns.GetHostName();
            IPHostEntry localHost = Dns.GetHostEntry(hostName);
            IPAddress address = localHost.AddressList[0];
            return address.ToString();
        }

        public override void Dispose()
        {
            if (mSendQueue != null) {
                mSendQueue.Clear();
                mSendQueue = null;
            }
            if (mClient != null) {
                mClient.Dispose();
                mClient = null;
            }
            if (mThread != null) {
                mThread.Abort();
                mThread = null;
            }
        }
    }
}