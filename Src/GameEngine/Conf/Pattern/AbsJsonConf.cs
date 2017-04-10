using System;

namespace GameEngine
{
    public abstract class AbsJsonConf : Iconf<JsonReaderImpl>
    {
        private IConfData confData;

        public IConfData ConfData
        {
            set {
                confData = value;
            }
            get {
                return confData;
            }
        }

        public void OnLoad(JsonReaderImpl reader)
        {
            throw new NotImplementedException();
        }

        public void OnUnload(bool isUnload)
        {
            throw new NotImplementedException();
        }

        public abstract void Init();

        public abstract void Close();
    }
}
