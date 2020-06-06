/***
 * AbsIniConf.cs
 * 
 * @author administrator
 */
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
            
        }

        public abstract void Init();

    }
}
