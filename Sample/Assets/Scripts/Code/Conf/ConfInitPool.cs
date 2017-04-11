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
            ConfPool.InitTabConf<TestDataTab, TestTabConf>();
            ConfPool.InitTabConf<CreatePlayerTabData, CreatePlayerTabConf>();
            ConfPool.InitTabConf<PlayerDataTabData, PlayerDataTabConf>();
            ConfPool.InitTabConf<PlayerSkillTabData, PlayerSkillTabConf>();
            ConfPool.InitTabConf<SceneTabData, SceneTabConf>();
        }

        private static void InitAllIni()
        {
            
        }

        private static void InitAllJson()
        {
            
        }
    }
}
