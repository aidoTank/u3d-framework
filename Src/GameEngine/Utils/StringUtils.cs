using System;

/***
 * StringUtil.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public static class StringUtils
    {
        public static string[] SplitString(string value, char separator = ' ')
        {
            string[] values = value.Split(separator);
            for (int i = 0, len = values.Length; i < len; ++i) {
                values[i] = values[i].Trim();
            }
            return values;
        }

        public static bool ToBool(string value, bool defaultValue = false)
        {
            if (!string.IsNullOrEmpty(value)) {
                var ret = defaultValue;
                if (Boolean.TryParse(value, out ret)) {
                    return ret;
                }
            }
            return defaultValue;
        }

        public static char ToChar(string value, char defaultValue = Char.MinValue)
        {
            if (!string.IsNullOrEmpty(value)) {
                var ret = defaultValue;
                if (Char.TryParse(value, out ret)) {
                    return ret;
                }
            }
            return defaultValue;
        }

        public static byte ToByte(string value, byte defaultValue = 0)
        {
            if (!string.IsNullOrEmpty(value)) {
                var ret = defaultValue;
                if (Byte.TryParse(value, out ret)) {
                    return ret;
                }
            }
            return defaultValue;
        }

        public static sbyte ToSByte(string value, sbyte defaultValue = 0)
        {
            if (!string.IsNullOrEmpty(value)) {
                var ret = defaultValue;
                if (SByte.TryParse(value, out ret)) {
                    return ret;
                }
            }
            return defaultValue;
        }

        public static short ToShort(string value, short defaultValue = 0)
        {
            if (!string.IsNullOrEmpty(value)) {
                var ret = defaultValue;
                if (Int16.TryParse(value, out ret)) {
                    return ret;
                }
            }
            return defaultValue;
        }

        public static ushort ToUShort(string value, ushort defaultValue = 0)
        {
            if (!string.IsNullOrEmpty(value)) {
                var ret = defaultValue;
                if (UInt16.TryParse(value, out ret)) {
                    return ret;
                }
            }
            return defaultValue;
        }

        public static int ToInt(string value, int defaultValue = 0)
        {
            if (!string.IsNullOrEmpty(value)) {
                var ret = defaultValue;
                if (Int32.TryParse(value, out ret)) {
                    return ret;
                }
            }
            return defaultValue;
        }

        public static uint ToUInt(string value, uint defaultValue = 0U)
        {
            if (!string.IsNullOrEmpty(value)) {
                var ret = defaultValue;
                if (UInt32.TryParse(value, out ret)) {
                    return ret;
                }
            }
            return defaultValue;
        }

        public static long ToLong(string value, long defaultValue = 0L)
        {
            if (!string.IsNullOrEmpty(value)) {
                var ret = defaultValue;
                if (Int64.TryParse(value, out ret)) {
                    return ret;
                }
            }
            return defaultValue;
        }

        public static ulong ToULong(string value, ulong defaultValue = 0UL)
        {
            if (!string.IsNullOrEmpty(value)) {
                var ret = defaultValue;
                if (UInt64.TryParse(value, out ret)) {
                    return ret;
                }
            }
            return defaultValue;
        }

        public static float ToFloat(string value, float defaultValue = 0f)
        {
            if (!string.IsNullOrEmpty(value)) {
                var ret = defaultValue;
                if (Single.TryParse(value, out ret)) {
                    return ret;
                }
            }
            return defaultValue;
        }

        public static double ToDouble(string value, double defaultValue = 0.0)
        {
            if (!string.IsNullOrEmpty(value)) {
                var ret = defaultValue;
                if (Double.TryParse(value, out ret)) {
                    return ret;
                }
            }
            return defaultValue;
        }

        public static decimal ToDecimal(string value, decimal defaultValue = Decimal.Zero)
        {
            if (!string.IsNullOrEmpty(value)) {
                var ret = defaultValue;
                if (Decimal.TryParse(value, out ret)) {
                    return ret;
                }
            }
            return defaultValue;
        }

        public static string[] ToStringArray(string value, char separator = ',')
        {
            if (!string.IsNullOrEmpty(value)) {
                return StringUtils.SplitString(value, separator);
            }
            return null;
        }

        public static bool[] ToBoolArray(string value, char separator = ',')
        {
            string[] strArray = ToStringArray(value, separator);
            if (null != strArray) {
                int len = strArray.Length;
                bool[] array = new bool[len];
                for (int i = 0; i < len; ++i) {
                    try {
                        array[i] = Convert.ToBoolean(strArray[i]);
                    } catch (FormatException) {
                        return null;
                    }
                }
                return array;
            }
            return null;
        }

        public static byte[] ToByteArray(string value, char separator = ',')
        {
            string[] strArray = ToStringArray(value, separator);
            if (null != strArray) {
                int len = strArray.Length;
                byte[] array = new byte[len];
                for (int i = 0; i < len; ++i) {
                    try {
                        array[i] = Convert.ToByte(strArray[i]);
                    } catch (FormatException) {
                        return null;
                    }
                }
                return array;
            }
            return null;
        }

        public static sbyte[] ToSByteArray(string value, char separator = ',')
        {
            string[] strArray = ToStringArray(value, separator);
            if (null != strArray) {
                int len = strArray.Length;
                sbyte[] array = new sbyte[len];
                for (int i = 0; i < len; ++i) {
                    try {
                        array[i] = Convert.ToSByte(strArray[i]);
                    } catch (FormatException) {
                        return null;
                    }
                }
                return array;
            }
            return null;
        }

        public static short[] ToShortArray(string value, char separator = ',')
        {
            string[] strArray = ToStringArray(value, separator);
            if (null != strArray) {
                int len = strArray.Length;
                short[] array = new short[len];
                for (int i = 0; i < len; ++i) {
                    try {
                        array[i] = Convert.ToInt16(strArray[i]);
                    } catch (FormatException) {
                        return null;
                    }
                }
                return array;
            }
            return null;
        }

        public static ushort[] ToUShortArray(string value, char separator = ',')
        {
            string[] strArray = ToStringArray(value, separator);
            if (null != strArray) {
                int len = strArray.Length;
                ushort[] array = new ushort[len];
                for (int i = 0; i < len; ++i) {
                    try {
                        array[i] = Convert.ToUInt16(strArray[i]);
                    } catch (FormatException) {
                        return null;
                    }
                }
                return array;
            }
            return null;
        }

        public static int[] ToIntArray(string value, char separator = ',')
        {
            string[] strArray = ToStringArray(value, separator);
            if (null != strArray) {
                int len = strArray.Length;
                int[] array = new int[len];
                for (int i = 0; i < len; ++i) {
                    try {
                        array[i] = Convert.ToInt32(strArray[i]);
                    } catch (FormatException) {
                        return null;
                    }
                }
                return array;
            }
            return null;
        }

        public static float[] ToFloatArray(string value, char separator = ',')
        {
            string[] strArray = ToStringArray(value, separator);
            if (null != strArray) {
                int len = strArray.Length;
                float[] array = new float[len];
                for (int i = 0; i < len; ++i) {
                    try {
                        array[i] = Convert.ToSingle(strArray[i]);
                    } catch (FormatException) {
                        return null;
                    }
                }
                return array;
            }
            return null;
        }

        public static double[] ToDoubleArray(string value, char separator = ',')
        {
            string[] strArray = ToStringArray(value, separator);
            if (null != strArray) {
                int len = strArray.Length;
                double[] array = new double[len];
                for (int i = 0; i < len; ++i) {
                    try {
                        array[i] = Convert.ToDouble(strArray[i]);
                    } catch (FormatException) {
                        return null;
                    }
                }
                return array;
            }
            return null;
        }

        public static bool IsIpAddress(string value)
        {
            var byteArray = ToByteArray(value, '.');
            if (null != byteArray && byteArray.Length == 4) {
                for (var i = 0; i < byteArray.Length; ++i) {
                    if (byteArray[i] < 0 || byteArray[i] > 255) {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
