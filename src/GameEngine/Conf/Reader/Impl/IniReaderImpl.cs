using System.Collections.Generic;

/***
 * IniReaderImpl.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public class IniReaderImpl : IIniReader
    {
        private Dictionary<string, Dictionary<string, string>> iniDict;

        public IniReaderImpl()
        {
            this.iniDict = new Dictionary<string, Dictionary<string, string>>();
        }

        public bool Open(string name, bool isInternal)
        {
            string text = "加载资源";
            if (string.IsNullOrEmpty(text)) {
                return false;
            }

            string[] lines = text.Split(new char[] { '\r', '\n' });
            int lineCount = lines.Length;
            if (lineCount == 0) {
                return false;
            }
            string line = null;
            string section = null;

            string[] pair;
            string key;
            string val;

            bool inString1;
            bool inString2;

            for (int i = 0; i < lineCount; ++i) {
                line = lines[i].Trim();

                if (line.Length == 0) {
                    continue;
                }

                if (line.StartsWith("#") || line.StartsWith(";")) {
                    continue;
                }

                if (line.StartsWith("[")) {
                    section = line.Substring(1, line.Length - 2);
                    AddSection(section);
                } else {
                    pair = line.SplitString('=');
                    key = pair[0];
                    val = pair[1].Trim();

                    // 过滤后注释（注意性能）
                    inString1 = inString2 = false;
                    for (int j = 0; j < val.Length; ++j) {
                        if (val[j] == '"') {
                            inString1 = !inString1;
                        }
                        if (val[j] == '\'') {
                            inString2 = !inString2;
                        }

                        if (!inString1 && !inString2 && (val[j] == '#' || val[j] == ';')) {
                            val = val.Substring(0, j - 1).Trim();
                            break;
                        }
                    }

                    AddField(section, key, val);
                }
            }

            return true;
        }

        private void AddSection(string section)
        {
            if (string.IsNullOrEmpty(section)) {
                section = string.Empty;
            }

            if (!iniDict.ContainsKey(section)) {
                iniDict.Add(section, new Dictionary<string, string>());
            }
        }

        private void AddField(string section, string key, string value)
        {
            if (string.IsNullOrEmpty(section)) {
                section = string.Empty;
            }
            if (string.IsNullOrEmpty(key)) {
                key = string.Empty;
            }

            iniDict[section].Add(key, value);
        }

        public string GetString(string section, string key, string defaultValue = null)
        {
            if (string.IsNullOrEmpty(section) || string.IsNullOrEmpty(key)) {
                return defaultValue;
            }

            Dictionary<string, string> values = null;
            if (iniDict.TryGetValue(section, out values)) {
                string value = null;
                if (values.TryGetValue(key, out value)) {
                    return value;
                }
            }
            return defaultValue;
        }

        public bool GetBool(string section, string key, bool defaultValue = false)
        {
            return StringUtils.ToBool(GetString(section, key));
        }

        public byte GetByte(string section, string key, byte defaultValue = 0)
        {
            return StringUtils.ToByte(GetString(section, key));
        }

        public sbyte GetSByte(string section, string key, sbyte defaultValue = 0)
        {
            return StringUtils.ToSByte(GetString(section, key));
        }

        public short GetShort(string section, string key, short defaultValue = 0)
        {
            return StringUtils.ToShort(GetString(section, key));
        }

        public ushort GetUShort(string section, string key, ushort defaultValue = 0)
        {
            return StringUtils.ToUShort(GetString(section, key));
        }

        public int GetInt(string section, string key, int defaultValue = 0)
        {
            return StringUtils.ToInt(GetString(section, key));
        }

        public uint GetUInt(string section, string key, uint defaultValue = 0U)
        {
            return StringUtils.ToUInt(GetString(section, key));
        }

        public long GetLong(string section, string key, long defaultValue = 0L)
        {
            return StringUtils.ToLong(GetString(section, key));
        }

        public ulong GetULong(string section, string key, ulong defaultValue = 0UL)
        {
            return StringUtils.ToULong(GetString(section, key));
        }

        public float GetFloat(string section, string key, float defaultValue = 0f)
        {
            return StringUtils.ToFloat(GetString(section, key));
        }

        public double GetDouble(string section, string key, double defaultValue = 0.0)
        {
            return StringUtils.ToDouble(GetString(section, key));
        }

        public void Close()
        {
            this.iniDict.Clear();
        }
    }
}
