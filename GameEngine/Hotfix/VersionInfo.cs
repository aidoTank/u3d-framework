using System.Text;

/***
 * VersionInfo.cs
 * 
 * @author abaojin
 */
namespace GameEngine
{
    public class VersionInfo
    {
        // 分隔符
        private const char SEPARATOR = '_';

        // 应用名称
        private string appName;
        // 应用编号
        private int appVersion;
        // 资源编号
        private string resourceVersion;

        public string AppName {
            get { return appName; }
            set { appName = value; }
        }

        public int AppVersion {
            get { return appVersion; }
            set { appVersion = value; }
        }

        public string ResourceVersion {
            get { return resourceVersion; }
            set { resourceVersion = value; }
        }

        public string Encoder()
        {
            StringBuilder sb = new StringBuilder(64);
            sb.Append(AppName).Append(SEPARATOR);
            sb.Append(AppVersion).Append(SEPARATOR);
            sb.Append(ResourceVersion);
            return sb.ToString().Trim();
        }

        public void Decoder(string lineString)
        {
            if (string.IsNullOrEmpty(lineString)) {
                return;
            }
            string[] eles = lineString.Split(SEPARATOR);
            this.AppName = eles[0].Trim();
            this.AppVersion = StringUtils.ToInt(eles[1]);
            this.ResourceVersion = eles[2].Trim();
        }

        /// <summary>
        /// 返回资源版本号
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}-v: {1}{2}{3}", AppName, AppVersion, SEPARATOR, ResourceVersion);
        }
    }
}
