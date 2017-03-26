/***
 * StringHelper.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public static class StringHelper
    {
        /// <summary>
        /// 标准化路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string Standard(this string path)
        {
            return path.Replace("\\", "/");
        }

        /// <summary>字符串转Byte</summary>
        /// <param name="val">值</param>
        /// <returns></returns>
        public static byte[] ToByte(this string val)
        {
            return System.Text.Encoding.UTF8.GetBytes(val);
        }

        /// <summary>Byte转字符串</summary>
        /// <param name="val">值</param>
        /// <returns></returns>
        public static string String(this byte[] val)
        {
            if (val == null || val.Length <= 0) {
                return string.Empty;
            }
            return System.Text.Encoding.UTF8.GetString(val);
        }
    }
}
