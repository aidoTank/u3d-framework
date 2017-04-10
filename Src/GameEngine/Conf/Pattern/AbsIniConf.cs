using System;

namespace GameEngine
{
    public abstract class AbsIniConf : Iconf<IniReaderImpl>
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

        public void OnLoad(IniReaderImpl reader)
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
