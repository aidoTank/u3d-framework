using System;
using System.IO;
using UnityEngine;

/***
 * FileLogAppender.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public class FileLogAppender : AbsLogAppender
    {
        private bool isInited = false;

        public string FileName
        {
            get;
            private set;
        }

        public int FileMaxSize
        {
            get;
            private set;
        }

        public string FileDir
        {
            get {
                string bPath = string.Empty;
                if (Application.isMobilePlatform) {
                    bPath = Application.persistentDataPath;
                } else {
                    bPath = Application.dataPath.Replace("Assets", "");
                }
                return string.Format(@"{0}/{1}", bPath, "Log/");
            }
        }

        public string FilePath
        {
            get {
                return string.Format(@"{0}{1}", FileDir, FileName);
            }
        }

        public FileLogAppender()
        {
            this.Init();
        }

        public FileLogAppender(string fileName, int fileMaxSize)
        {
            FileName = fileName;
            FileMaxSize = fileMaxSize;
            this.Init();
        }

        private void Init()
        {
            if (isInited) {
                return;
            }
            isInited = true;
            if (!Directory.Exists(FileDir)) {
                Directory.CreateDirectory(FileDir);
            }
            if (!File.Exists(FilePath)) {
                File.Create(FilePath).Close();
            }
        }

        protected override void OnWrite(string message, string stackTrace)
        {
            try {
                StreamWriter sw = File.AppendText(FilePath);
                sw.WriteLine(message);
                sw.Close();
            }catch(Exception e) {
                throw e;
            }
        }
    }
}