using System.Text;

/***
 * ResourceInfo.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public class ResourceInfo
    {
        // 分隔符
        private const char SEPARATOR = '|';

        // 资源ID
        private int id;
        // 资源名称（全路径）
        private string name;
        // 文件大小
        private int fileSize;
        // MD5校验码
        private string md5;

        public string MD5 {
            get { return md5; }
            set { md5 = value; }
        }

        public int FileSize {
            get { return fileSize; }
            set { fileSize = value; }
        }

        public string Name {
            get { return name; }
            set { name = value; }
        }

        public int ID {
            get { return id; }
            set { id = value; }
        }

        public string Encoder()
        {
            StringBuilder sb = new StringBuilder(64);
            sb.Append(ID).Append(SEPARATOR);
            sb.Append(Name).Append(SEPARATOR);
            sb.Append(FileSize).Append(SEPARATOR);
            sb.Append(MD5);
            return sb.ToString().Trim();
        }

        public void Decoder(string line)
        {
            if (!string.IsNullOrEmpty(line)) {
                string[] elements = line.Split(SEPARATOR);
                this.ID = StringUtils.ToInt(elements[0]);
                this.Name = elements[1].Trim();
                this.FileSize = StringUtils.ToInt(elements[2]);
                this.MD5 = elements[3].Trim();
            }
        }
    }
}
