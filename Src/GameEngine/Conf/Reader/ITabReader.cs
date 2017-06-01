using System;

/***
 * ConfPool.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public interface ITabReader : IReader
    {
        int RowCount
        {
            get;
        }

        int ColumnCount
        {
            get;
        }

        ITabRow this[int rowIndex]
        {
            get;
        }

        bool ExistColumn(string columnName);

        int ColumnIndex(string columnName);

        string ColumnName(int columnIndex);

        void ParseRow(Action<ITabRow> action);
    }

    public interface ITabRow
    {
        int ColumnCount
        {
            get;
        }

        bool ExistColumn(string columnName);

        int ColumnIndex(string columnName);

        string ColumnName(int columnIndex);

        string GetString(int columnIndex, string defaultValue = "");

        string GetString(string columnName, string defaultValue = "");

        char GetChar(int columnIndex, char defaultValue = '\0');

        char GetChar(string columnName, char defaultValue = '\0');

        short GetShort(int columnIndex, short defaultValue = 0);

        short GetShort(string columnName, short defaultValue = 0);

        ushort GetUShort(int columnIndex, ushort defaultValue = 0);

        ushort GetUShort(string columnName, ushort defaultValue = 0);

        int GetInt(int columnIndex, int defaultValue = 0);

        int GetInt(string columnName, int defaultValue = 0);

        uint GetUInt(int columnIndex, uint defaultValue = 0);

        uint GetUInt(string columnName, uint defaultValue = 0);

        long GetLong(int columnIndex, long defaultValue = 0L);

        long GetLong(string columnName, long defaultValue = 0L);

        ulong GetULong(int columnIndex, ulong defaultValue = 0L);

        ulong GetULong(string columnName, ulong defaultValue = 0L);

        float GetFloat(int columnIndex, float defaultValue = .0f);

        float GetFloat(string columnName, float defaultValue = .0f);

        double GetDouble(int columnIndex, double defaultValue = .0f);

        double GetDouble(string columnName, double defaultValue = .0f);

        bool GetBool(int columnIndex, bool defaultValue = false);

        bool GetBool(string columnName, bool defaultValue = false);

        int[] GetIntArray(int columnIndex, char separator = '|');

        int[] GetIntArray(string columnName, char separator = '|');

        uint[] GetUIntArray(int columnIndex, char separator = '|');

        uint[] GetUIntArray(string columnName, char separator = '|');

        short[] GetShortArray(int columnIndex, char separator = '|');

        short[] GetShortArray(string columnName, char separator = '|');

        ushort[] GetUShortArray(int columnIndex, char separator = '|');

        ushort[] GetUShortArray(string columnName, char separator = '|');

        float[] GetFloatArray(int columnIndex, char separator = '|');

        float[] GetFloatArray(string columnName, char separator = '|');

        string[] GetStringArray(int columnIndex, char separator = '|');

        string[] GetStringArray(string columnName, char separator = '|');

        double[] GetDoubleArray(int columnIndex, char separator = '|');

        double[] GetDoubleArray(string columnName, char separator = '|');
    }
}
