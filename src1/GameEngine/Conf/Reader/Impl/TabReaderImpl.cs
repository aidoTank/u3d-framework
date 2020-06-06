using System.Collections.Generic;
using System;
using UnityEngine;

/***
 * TabReaderImpl.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public class TabReaderImpl : ITabReader
    {
        private List<string> columnNames = new List<string>();
        private List<string[]> tabValues = new List<string[]>();
        private Dictionary<string, int> column2Index = new Dictionary<string, int>();

        private TabRowImpl tabRow;

        public TabReaderImpl()
        {
            this.tabRow = new TabRowImpl(this);
        }

        public int RowCount
        {
            get {
                return tabValues.Count;
            }
        }

        public int ColumnCount
        {
            get {
                return columnNames.Count;
            }
        }

        public ITabRow this[int rowIndex]
        {
            get {
                tabRow.Row = tabValues[rowIndex];
                return tabRow;
            }
        }

        /// <summary>
        /// name-名称
        /// type-类型
        /// #注释
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public bool Open(string fileName, bool isInternal)
        {
            TextAsset textAsset =  Resources.Load<TextAsset>(fileName);
            if(textAsset == null) {
                return false;
            }

            string text = textAsset.text;
            if (string.IsNullOrEmpty(text)) {
                return false;
            }

            string[] lines = text.Split(new char[] { '\n' });

            int lineCount = lines.Length;
            if (lineCount == 0) {
                return false;
            }

            string name;
            string[] names = SplitLine(lines[0]);
            for (int i = 0, count = names.Length; i < count; ++i) {
                name = names[i].Trim();
                if (name.Length > 0) {
                    column2Index.Add(name, i);
                    columnNames.Add(name);
                }
            }

            string line;
            for (int i = 2; i < lineCount; ++i) {
                line = lines[i].Trim();
                if (line.Length > 0 && !line.StartsWith("#")) {
                    tabValues.Add(SplitLine(lines[i]));
                }
            }

            return true;
        }

        private static string[] SplitLine(string line)
        {
            return line.SplitString('\t');
        }

        public bool ExistColumn(string columnName)
        {
            return column2Index.ContainsKey(columnName);
        }

        public int ColumnIndex(string columnName)
        {
            int index;
            if (!column2Index.TryGetValue(columnName, out index)) {
                return -1;
            }
            return index;
        }

        public string ColumnName(int columnIndex)
        {
            if (columnIndex >= columnNames.Count || columnIndex < 0) {
                return null;
            }
            return columnNames[columnIndex];
        }

        public void ParseRow(Action<ITabRow> action)
        {
            int count = RowCount;
            for (int i = 0; i < count; ++i) {
                tabRow.Row = tabValues[i];
                action(tabRow);
            }
        }

        public void Close()
        {
            column2Index.Clear();
            columnNames.Clear();
            tabValues.Clear();
        }
    }

    public class TabRowImpl : ITabRow
    {
        private static readonly int[] EmptyIntArray = { };
        private static readonly string[] EmptyStringArray = { };

        private TabReaderImpl reader;
        private string[] row;

        public string[] Row
        {
            get {
                return row;
            }
            set {
                row = value;
            }
        }

        public TabRowImpl(TabReaderImpl reader)
        {
            this.reader = reader;
        }

        public int ColumnCount
        {
            get {
                return reader.ColumnCount;
            }
        }

        public bool ExistColumn(string columnName)
        {
            return reader.ExistColumn(columnName);
        }

        public int ColumnIndex(string columnName)
        {
            return reader.ColumnIndex(columnName);
        }

        public string ColumnName(int columnIndex)
        {
            return reader.ColumnName(columnIndex);
        }

        public string GetString(int columnIndex, string defaultValue = "")
        {
            if (columnIndex >= row.Length || columnIndex < 0) {
                return defaultValue;
            }

            return row[columnIndex];
        }

        public string GetString(string columnName, string defaultValue = "")
        {
            return GetString(ColumnIndex(columnName), defaultValue);
        }

        public char GetChar(int columnIndex, char defaultValue = '\0')
        {
            if (columnIndex >= row.Length || columnIndex < 0) {
                return defaultValue;
            }

            string v = row[columnIndex];
            if (string.IsNullOrEmpty(v)) {
                return defaultValue;
            }

            return v[0];
        }

        public char GetChar(string columnName, char defaultValue = '\0')
        {
            return GetChar(ColumnIndex(columnName), defaultValue);
        }

        public short GetShort(int columnIndex, short defaultValue = 0)
        {
            if (columnIndex >= row.Length || columnIndex < 0) {
                return defaultValue;
            }

            short v = defaultValue;

            short.TryParse(row[columnIndex], out v);

            return v;
        }

        public short GetShort(string columnName, short defaultValue = 0)
        {
            return GetShort(ColumnIndex(columnName), defaultValue);
        }

        public ushort GetUShort(int columnIndex, ushort defaultValue = 0)
        {
            if (columnIndex >= row.Length || columnIndex < 0) {
                return defaultValue;
            }

            ushort v = defaultValue;

            ushort.TryParse(row[columnIndex], out v);

            return v;
        }

        public ushort GetUShort(string columnName, ushort defaultValue = 0)
        {
            return GetUShort(ColumnIndex(columnName), defaultValue);
        }

        public int GetInt(int columnIndex, int defaultValue = 0)
        {
            if (columnIndex >= row.Length || columnIndex < 0) {
                return defaultValue;
            }

            int v = defaultValue;

            int.TryParse(row[columnIndex], out v);

            return v;
        }

        public int GetInt(string columnName, int defaultValue = 0)
        {
            return GetInt(ColumnIndex(columnName), defaultValue);
        }

        public uint GetUInt(int columnIndex, uint defaultValue = 0)
        {
            if (columnIndex >= row.Length || columnIndex < 0) {
                return defaultValue;
            }

            uint v = defaultValue;

            uint.TryParse(row[columnIndex], out v);

            return v;
        }

        public uint GetUInt(string columnName, uint defaultValue = 0)
        {
            return GetUInt(ColumnIndex(columnName), defaultValue);
        }

        public long GetLong(int columnIndex, long defaultValue = 0L)
        {
            if (columnIndex >= row.Length || columnIndex < 0) {
                return defaultValue;
            }

            long v = defaultValue;

            long.TryParse(row[columnIndex], out v);

            return v;
        }

        public long GetLong(string columnName, long defaultValue = 0L)
        {
            return GetLong(ColumnIndex(columnName), defaultValue);
        }

        public ulong GetULong(int columnIndex, ulong defaultValue = 0L)
        {
            if (columnIndex >= row.Length || columnIndex < 0) {
                return defaultValue;
            }

            ulong v = defaultValue;

            ulong.TryParse(row[columnIndex], out v);

            return v;
        }

        public ulong GetULong(string columnName, ulong defaultValue = 0L)
        {
            return GetULong(ColumnIndex(columnName), defaultValue);
        }

        public float GetFloat(int columnIndex, float defaultValue = .0f)
        {
            if (columnIndex >= row.Length || columnIndex < 0) {
                return defaultValue;
            }

            float v = defaultValue;

            float.TryParse(row[columnIndex], out v);

            return v;
        }

        public float GetFloat(string columnName, float defaultValue = .0f)
        {
            return GetFloat(ColumnIndex(columnName), defaultValue);
        }

        public double GetDouble(int columnIndex, double defaultValue = .0f)
        {
            if (columnIndex >= row.Length || columnIndex < 0) {
                return defaultValue;
            }

            double v = defaultValue;

            double.TryParse(row[columnIndex], out v);

            return v;
        }

        public double GetDouble(string columnName, double defaultValue = .0f)
        {
            return GetDouble(ColumnIndex(columnName), defaultValue);
        }

        public bool GetBool(int columnIndex, bool defaultValue = false)
        {
            if (columnIndex >= row.Length || columnIndex < 0) {
                return defaultValue;
            }

            bool v = defaultValue;

            bool.TryParse(row[columnIndex], out v);

            return v;
        }

        public bool GetBool(string columnName, bool defaultValue = false)
        {
            return GetBool(ColumnIndex(columnName), defaultValue);
        }

        public int[] GetIntArray(int columnIndex, char separator = '|')
        {
            string str = GetString(columnIndex, string.Empty);
            int[] arr = str.SplitString<int>(separator);
            return arr ?? new int[] { };
        }

        public int[] GetIntArray(string columnName, char separator = '|')
        {
            return GetIntArray(ColumnIndex(columnName), separator);
        }

        public uint[] GetUIntArray(int columnIndex, char separator = '|')
        {
            string str = GetString(columnIndex, string.Empty);
            uint[] arr = str.SplitString<uint>(separator);
            return arr ?? new uint[] { };
        }

        public uint[] GetUIntArray(string columnName, char separator = '|')
        {
            return GetUIntArray(ColumnIndex(columnName), separator);
        }

        public short[] GetShortArray(int columnIndex, char separator = '|')
        {
            string str = GetString(columnIndex, string.Empty);
            short[] arr = str.SplitString<short>(separator);
            return arr ?? new short[] { };
        }

        public short[] GetShortArray(string columnName, char separator = '|')
        {
            return GetShortArray(ColumnIndex(columnName), separator);
        }

        public ushort[] GetUShortArray(int columnIndex, char separator = '|')
        {
            string str = GetString(columnIndex, string.Empty);
            ushort[] arr = str.SplitString<ushort>(separator);
            return arr ?? new ushort[] { };
        }

        public ushort[] GetUShortArray(string columnName, char separator = '|')
        {
            return GetUShortArray(ColumnIndex(columnName), separator);
        }

        public float[] GetFloatArray(int columnIndex, char separator = '|')
        {
            string str = GetString(columnIndex, string.Empty);
            float[] arr = str.SplitString<float>(separator);
            return arr ?? new float[] { };
        }

        public float[] GetFloatArray(string columnName, char separator = '|')
        {
            return GetFloatArray(ColumnIndex(columnName), separator);
        }

        public string[] GetStringArray(int columnIndex, char separator = '|')
        {
            string str = GetString(columnIndex, string.Empty);
            string[] arr = str.SplitString(separator);
            return arr ?? new string[] { };
        }

        public string[] GetStringArray(string columnName, char separator = '|')
        {
            return GetStringArray(ColumnIndex(columnName), separator);
        }

        public double[] GetDoubleArray(int columnIndex, char separator = '|')
        {
            string str = GetString(columnIndex, string.Empty);
            double[] arr = str.SplitString<double>(separator);
            return arr ?? new double[] { };
        }

        public double[] GetDoubleArray(string columnName, char separator = '|')
        {
            return GetDoubleArray(ColumnIndex(columnName), separator);
        }
    }
}
