using System.IO;

/***
 * FileLogAppender.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public class FileLogAppender : AbsLogAppender
    {
        public string Filename
        {
            get;
            private set;
        }

        public int FileMaxSize
        {
            get;
            private set;
        }

        public bool IsNewFile
        {
            get;
            private set;
        }

        public string FileDir
        {
            get {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Application.persistentDataPath + "/Log/";
#elif UNITY_IPHONE && !UNITY_EDITOR
                return Application.persistentDataPath + "/Log/";
#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR
                return Application.dataPath + "/Log/";
#else
                return "./Log/";
#endif
            }
        }

        public string FilePath
        {
            get {
                return string.Format("{0}{1}", FileDir, Filename);
            }
        }

        private bool isInited = false;

        private void Init()
        {
            if (!isInited) {
                isInited = true;

                if (!Directory.Exists(FileDir)) {
                    Directory.CreateDirectory(FileDir);
                } else {
                    if (IsNewFile && File.Exists(FilePath)) {
                        File.Delete(FilePath);
                    }
                }

                if (!File.Exists(FilePath)) {
                    FileStream fs = File.Open(FilePath, FileMode.OpenOrCreate);
                    if (fs != null) {
                        long Length = fs.Length;
                        fs.Close();

                        if (Length > FileMaxSize) {
                            File.Delete(FilePath);
                        }
                    }
                }
            }
        }

        public FileLogAppender(string filename, int fileMaxSize, bool isCreateNewFile)
        {
            Filename = filename;
            FileMaxSize = fileMaxSize;
            IsNewFile = isCreateNewFile;
        }

        protected override void OnWrite(string msg, string stackTrace)
        {
            Init();
            using (StreamWriter sw = new StreamWriter(FilePath, true)) {
                sw.WriteLine(msg);
            }
        }
    }
}