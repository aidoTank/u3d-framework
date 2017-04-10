using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine
{
    interface IIniReader : IReader
    {
        string GetString(string section, string key, string defaultValue = null);

        bool GetBool(string section, string key, bool defaultValue = false);

        byte GetByte(string section, string key, byte defaultValue = 0);

        sbyte GetSByte(string section, string key, sbyte defaultValue = 0);

        short GetShort(string section, string key, short defaultValue = 0);

        ushort GetUShort(string section, string key, ushort defaultValue = 0);

        int GetInt(string section, string key, int defaultValue = 0);

        uint GetUInt(string section, string key, uint defaultValue = 0U);

        long GetLong(string section, string key, long defaultValue = 0L);

        ulong GetULong(string section, string key, ulong defaultValue = 0UL);

        float GetFloat(string section, string key, float defaultValue = 0f);

        double GetDouble(string section, string key, double defaultValue = 0.0);
    }
}
