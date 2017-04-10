using LitJson;

namespace GameEngine
{
    public class JsonReaderImpl : IJsonReader
    {
        private JsonData jsonData = new JsonData();

        public bool Open(string name, bool isInternal)
        {
            string text = "资源加载";
            if (string.IsNullOrEmpty(text)) {
                return false;
            }

            text = text.Trim();
            if (text.Length == 0) {
                return false;
            }

            jsonData = JsonMapper.ToObject(text);

            return true;
        }

        public string GetString(string key, string defaultValue = null)
        {
            return jsonData.GetString(key, defaultValue);
        }

        public bool GetBool(string key, bool defaultValue = false)
        {
            return jsonData.GetBool(key, defaultValue);
        }

        public byte GetByte(string key, byte defaultValue = 0)
        {
            return (byte)jsonData.GetInt(key, defaultValue);
        }

        public sbyte GetSByte(string key, sbyte defaultValue = 0)
        {
            return (sbyte)jsonData.GetInt(key, defaultValue);
        }

        public short GetShort(string key, short defaultValue = 0)
        {
            return (short)jsonData.GetInt(key, defaultValue);
        }

        public ushort GetUShort(string key, ushort defaultValue = 0)
        {
            return (ushort)jsonData.GetInt(key, defaultValue);
        }

        public int GetInt(string key, int defaultValue = 0)
        {
            return jsonData.GetInt(key, defaultValue);
        }

        public uint GetUInt(string key, uint defaultValue = 0U)
        {
            return (uint)jsonData.GetInt(key, (int)defaultValue);
        }

        public long GetLong(string key, long defaultValue = 0L)
        {
            return jsonData.GetLong(key, defaultValue);
        }

        public ulong GetULong(string key, ulong defaultValue = 0UL)
        {
            return (ulong)jsonData.GetLong(key, (long)defaultValue);
        }

        public float GetFloat(string key, float defaultValue = 0f)
        {
            return jsonData.GetFloat(key, defaultValue);
        }

        public double GetDouble(string key, double defaultValue = 0.0)
        {
            return jsonData.GetDouble(key, defaultValue);
        }

        public T GetObject<T>(string key)
        {
            return JsonMapper.ToObject<T>(jsonData.ToJson());
        }

        public void Close()
        {
            jsonData = null;
        }
    }
}
