/***
 * AbsJsonConf.cs
 * 
 * @author administrator
 */
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
            
        }

        public abstract void Init();
    }
}
