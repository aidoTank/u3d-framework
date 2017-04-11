using GameEngine;

namespace GameCode
{
    public class ConfInitPool
    {
        public static void InitConf()
        {
            InitAllTab();
            InitAllIni();
            InitAllJson();
        }

        private static void InitAllTab()
        {
            ConfPool.InitTabConf<CreatePlayerTab, CreatePlayerTabConf>();
            ConfPool.InitTabConf<PlayerDataTab, PlayerDataTabConf>();
            ConfPool.InitTabConf<PlayerSkillTab, PlayerSkillTabConf>();
            ConfPool.InitTabConf<SceneTab, SceneTabConf>();
        }

        private static void InitAllIni()
        {
            
        }

        private static void InitAllJson()
        {
            
        }
    }
}
