namespace GameEngine
{
    public class ConfFactory : Singleton<ConfFactory>, IConfFactory
    {
        public bool Load<T>(string file, Iconf<T> conf, bool isInternal) where T : IReader, new()
        {
            bool result = true;

            if (conf == null) {
                return false;
            }

            if (string.IsNullOrEmpty(file)) {
                return false;
            }

            T reader = new T();
            if(reader != null) {
                result = reader.Open(file, isInternal);
            }

            conf.OnLoad(reader);

            return result;
        }

        public bool UnLoad<T>(Iconf<T> conf, bool isUnload = true) where T : IReader
        {
            bool result = true;

            if (conf == null) {
                return false;
            }

            conf.OnUnload(isUnload);

            return result;
        }

        public static bool LoadConf<T>(string file, Iconf<T> conf, bool isInternal) where T : IReader, new()
        {
            return Instance.Load<T>(file, conf, isInternal);
        }

        public static bool UnloadConf<T>(Iconf<T> conf, bool isUnload = true) where T : IReader
        {
            return Instance.UnLoad(conf, isUnload);
        }
    }
}
