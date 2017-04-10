using LitJson;
using UnityEngine;

namespace GameEngine
{
    public static class JsonHelper
    {
        public static bool HashKey(this JsonData jd, string key)
        {
            return jd.Keys.Contains(key);
        }

        public static JsonData GetObject(this JsonData jd, string key)
        {
            if (jd.Keys.Contains(key)) {
                return jd[key];
            }
            return null;
        }

        public static float GetFloat(this JsonData jd, string key, float defaultValue = 0f)
        {
            if (jd.Keys.Contains(key)) {
                return jd[key].ToFloat(defaultValue);
            }
            return defaultValue;
        }

        public static double GetDouble(this JsonData jd, string key, double defaultValue = 0.0)
        {
            if (jd.Keys.Contains(key)) {
                return jd[key].ToDouble(defaultValue);
            }
            return defaultValue;
        }

        public static int GetInt(this JsonData jd, string key, int defaultValue = 0)
        {
            if (jd.Keys.Contains(key)) {
                return jd[key].ToInt(defaultValue);
            }
            return defaultValue;
        }

        public static long GetLong(this JsonData jd, string key, long defaultValue = 0L)
        {
            if (jd.Keys.Contains(key)) {
                return jd[key].ToLong(defaultValue);
            }
            return defaultValue;
        }

        public static bool GetBool(this JsonData jd, string key, bool defaultValue = false)
        {
            if (jd.Keys.Contains(key)) {
                return jd[key].ToBool(defaultValue);
            }
            return defaultValue;
        }

        public static string GetString(this JsonData jd, string key, string defaultValue = null)
        {
            if (jd.Keys.Contains(key)) {
                return jd[key].ToString(defaultValue);
            }
            return defaultValue;
        }

        public static Vector2 GetVector2(this JsonData jd, string key)
        {
            if (jd.Keys.Contains(key)) {
                if ((jd = jd[key]).IsArray && jd.Count >= 2) {
                    return new Vector2(jd[0].ToFloat(), jd[1].ToFloat());
                }

                if ((jd = jd[key]).IsObject) {
                    if (jd.Keys.Contains("x") && jd.Keys.Contains("y")) {
                        return new Vector2(jd["x"].ToFloat(), jd["y"].ToFloat());
                    }
                    if (jd.Keys.Contains("X") && jd.Keys.Contains("Y")) {
                        return new Vector2(jd["X"].ToFloat(), jd["Y"].ToFloat());
                    }
                }
            }

            return Vector2.zero;
        }

        public static Vector3 GetVector3(this JsonData jd, string key)
        {
            if (jd.Keys.Contains(key)) {
                if ((jd = jd[key]).IsArray && jd.Count >= 3) {
                    return new Vector3(jd[0].ToFloat(), jd[1].ToFloat(), jd[2].ToFloat());
                }

                if ((jd = jd[key]).IsObject) {
                    if (jd.Keys.Contains("x") && jd.Keys.Contains("y") && jd.Keys.Contains("z")) {
                        return new Vector3(jd["x"].ToFloat(), jd["y"].ToFloat(), jd["z"].ToFloat());
                    }
                    if (jd.Keys.Contains("X") && jd.Keys.Contains("Y") && jd.Keys.Contains("Z")) {
                        return new Vector3(jd["X"].ToFloat(), jd["Y"].ToFloat(), jd["Z"].ToFloat());
                    }
                }
            }

            return Vector3.zero;
        }

        public static float ToFloat(this JsonData jd, float defaultValue = 0f)
        {
            if (null != jd) {
                if (jd.IsDouble)
                    return (float)(double)jd;
                else if (jd.IsLong)
                    return (float)(long)jd;
                else if (jd.IsInt)
                    return (float)(int)jd;
            }
            return defaultValue;
        }

        public static double ToDouble(this JsonData jd, double defaultValue = 0.0)
        {
            if (null != jd) {
                if (jd.IsDouble)
                    return (double)jd;
                else if (jd.IsLong)
                    return (float)(long)jd;
                else if (jd.IsInt)
                    return (double)(int)jd;
            }
            return defaultValue;
        }

        public static int ToInt(this JsonData jd, int defaultValue = 0)
        {
            if (null != jd) {
                if (jd.IsInt)
                    return (int)jd;
            }
            return defaultValue;
        }

        public static long ToLong(this JsonData jd, long defaultValue = 0L)
        {
            if (null != jd) {
                if (jd.IsLong)
                    return (long)jd;
                else if (jd.IsInt)
                    return (long)(int)jd;
            }
            return defaultValue;
        }

        public static bool ToBool(this JsonData jd, bool defaultValue = false)
        {
            if (null != jd) {
                if (jd.IsBoolean)
                    return (bool)jd;
                else if (jd.IsDouble)
                    return (double)jd > 0;
                else if (jd.IsLong)
                    return (long)jd > 0;
                else if (jd.IsInt)
                    return (int)jd > 0;
                else if (jd.IsString) {
                    string str = ((string)jd).ToLower();
                    return ("true" == str || "yes" == str);
                }
            }
            return defaultValue;
        }

        public static string ToString(this JsonData jd, string defaultValue = null)
        {
            if (null != jd) {
                if (jd.IsString)
                    return (string)jd;
            }
            return defaultValue;
        }
    }
}
