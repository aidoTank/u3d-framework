using System;
using System.Text;

/***
 * ByteUtils.cs
 *
 * @author administrator
 */
namespace GameEngine
{
    /// <summary>
    /// 客户端字节操作辅助类（C#默认采用小端）
    /// </summary>
    public static class ByteUtils
    {
        private static bool IsLittleEndian = false;

        /// <summary>
        /// 字节数组转string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ByteArray2String(byte[] s)
        {
            if(s == null || s.Length <= 0) {
                return null;
            }
            return Encoding.UTF8.GetString(s);
        }

        /// <summary>
        /// string转字节数组
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] String2ByteArray(string s)
        {
            if (s == null || s.Length <= 0) {
                return null;
            }
            return Encoding.UTF8.GetBytes(s);
        }

        /// <summary>
        /// 将Short转换为无符号Int
        /// </summary>
        /// <returns>The short2 integer.</returns>
        /// <param name="s">S.</param>
        public static int Short2Integer(short s)
        {
            return 0x0000ffff & s;
        }

        /// <summary>
        /// 转换Short数组为Byte数组
        /// </summary>
        /// <returns>The short array2 byte array.</returns>
        /// <param name="s">S.</param>
        public static byte[] ShortArray2ByteArray(short[] s)
        {
            if (s == null || s.Length <= 0) {
                return null;
            }
            byte[] ret = new byte[s.Length * 2];
            for (int i = 0; i < s.Length; i++) {
                if (IsLittleEndian) {
                    ret[i * 4 + 3] = (byte)(s[i] >> 24);
                    ret[i * 4 + 2] = (byte)(s[i] >> 16);
                    ret[i * 4 + 1] = (byte)(s[i] >> 8);
                    ret[i * 4] = (byte)s[i];
                } else {
                    ret[i * 4] = (byte)(s[i] >> 24);
                    ret[i * 4 + 1] = (byte)(s[i] >> 16);
                    ret[i * 4 + 2] = (byte)(s[i] >> 8);
                    ret[i * 4 + 3] = (byte)s[i];
                }
            }
            return ret;
        }

        /// <summary>
        /// 将Byte数组转换为Int数组
        /// </summary>
        /// <returns>The byte array2 int array.</returns>
        /// <param name="s">S.</param>
        public static int[] ByteArray2IntArray(byte[] s)
        {
            if (s == null || s.Length <= 0) {
                return null;
            }
            int[] ret = new int[s.Length / 4];
            for (int i = 0; i < ret.Length; i++) {
                ret[i] = ToInt(s[i * 4], s[i * 4 + 1], s[i * 4 + 2], s[i * 4 + 3]);
            }
            return ret;
        }

        /// <summary>
        /// 字节数组转短整形
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static short ByteArray2Short(byte[] s)
        {
            if (s == null || s.Length <= 0) {
                return default(short);
            }

            return (short)ToInt(s[0], s[1]);
        }

        /// <summary>
        /// 将字节数组转换成int
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ByteArray2Int(byte[] s)
        {
            if(s == null || s.Length <= 0) {
                return default(int);
            }

            return ToInt(s[0], s[1], s[2], s[3]);
        }

        public static byte[] Short2ByteArray(short s)
        {
            byte[] ret = new byte[2];

            if (IsLittleEndian) {
                ret[1] = (byte)(s >> 8);
                ret[0] = (byte)s;
            } else {
                ret[0] = (byte)(s >> 8);
                ret[1] = (byte)s;
            }

            return ret;
        }

        /// <summary>
        /// 将int转换为字节数组
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] Int2ByteArray(int s)
        {
            byte[] ret = new byte[4];

            if (IsLittleEndian) {
                ret[3] = (byte)(s >> 24);
                ret[2] = (byte)(s >> 16);
                ret[1] = (byte)(s >> 8);
                ret[0] = (byte)s;
            } else {
                ret[0] = (byte)(s >> 24);
                ret[1] = (byte)(s >> 16);
                ret[2] = (byte)(s >> 8);
                ret[3] = (byte)s;
            }

            return ret;
        }

        /// <summary>
        /// 将Byte数组转换成Short数组
        /// </summary>
        /// <returns>The byte array2 short array.</returns>
        /// <param name="s">S.</param>
        public static short[] ByteArray2ShortArray(byte[] s)
        {
            if (s == null || s.Length <= 0) {
                return null;
            }
            short[] ret = new short[s.Length / 2];
            for (int i = 0; i < ret.Length; i++) {
                ret[i] = (short)ToInt(s[i * 2], s[i * 2 + 1]);
            }
            return ret;
        }

        /// <summary>
        /// 将2-Byte转换成Short
        /// </summary>
        /// <returns>The short.</returns>
        /// <param name="a1">A1.</param>
        /// <param name="a2">A2.</param>
        public static int ToInt(byte a1, byte a2)
        {
            if (IsLittleEndian) {
                return (a2 << 8) & 0x0000ff00 | a1 & 0x000000ff;
            } else {
                return (a1 << 8) & 0x0000ff00 | a2 & 0x000000ff;
            }
        }

        /// <summary>
        /// 将4-Byte转换成1-Int
        /// </summary>
        /// <returns>The int.</returns>
        /// <param name="a1">A1.</param>
        /// <param name="a2">A2.</param>
        /// <param name="a3">A3.</param>
        /// <param name="a4">A4.</param>
        public static int ToInt(byte a1, byte a2, byte a3, byte a4)
        {
            if (IsLittleEndian) {
                return (a4 << 24) | (a3 << 16) & 0x00ff0000 | (a2 << 8) & 0x0000ff00 | a1 & 0x000000ff;
            } else {
                return (a1 << 24) | (a2 << 16) & 0x00ff0000 | (a3 << 8) & 0x0000ff00 | a4 & 0x000000ff;
            }
        }
    }
}