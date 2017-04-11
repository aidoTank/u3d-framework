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

        int GetInt(int columnIndex, int defaultValue = 0);

        int GetInt(string columnName, int defaultValue = 0);

        long GetLong(int columnIndex, long defaultValue = 0L);

        long GetLong(string columnName, long defaultValue = 0L);

        float GetFloat(int columnIndex, float defaultValue = .0f);

        float GetFloat(string columnName, float defaultValue = .0f);

        bool GetBool(int columnIndex, bool defaultValue = false);

        bool GetBool(string columnName, bool defaultValue = false);

        string[] GetStringArray(int columnIndex, char separator = ',');

        string[] GetStringArray(string columnName, char separator = ',');

        int[] GetIntArray(int columnIndex, char separator = ',');

        int[] GetIntArray(string columnName, char separator = ',');
    }
}
