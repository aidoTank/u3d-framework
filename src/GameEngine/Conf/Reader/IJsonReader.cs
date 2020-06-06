/***
 * ConfPool.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public interface IJsonReader : IReader
    {
        string GetString(string key, string defaultValue = null);

        bool GetBool(string key, bool defaultValue = false);

        byte GetByte(string key, byte defaultValue = 0);

        sbyte GetSByte(string key, sbyte defaultValue = 0);

        short GetShort(string key, short defaultValue = 0);

        ushort GetUShort(string key, ushort defaultValue = 0);

        int GetInt(string key, int defaultValue = 0);

        uint GetUInt(string key, uint defaultValue = 0U);

        long GetLong(string key, long defaultValue = 0L);

        ulong GetULong(string key, ulong defaultValue = 0UL);

        float GetFloat(string key, float defaultValue = 0f);

        double GetDouble(string key, double defaultValue = 0.0);

        T GetObject<T>(string key);
    }
}
