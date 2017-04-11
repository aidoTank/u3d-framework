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
            return StringUtils.SplitString(line, '\t');
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
            int count = ColumnCount;
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

        public string GetString(int columnIndex, string defaultValue)
        {
            if (columnIndex >= row.Length || columnIndex < 0) {
                return defaultValue;
            }

            return row[columnIndex];
        }

        public char GetChar(int columnIndex, char defaultValue)
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

        public short GetShort(int columnIndex, short defaultValue)
        {
            if (columnIndex >= row.Length || columnIndex < 0) {
                return defaultValue;
            }

            short v = defaultValue;

            short.TryParse(row[columnIndex], out v);

            return v;
        }

        public int GetInt(int columnIndex, int defaultValue)
        {
            if (columnIndex >= row.Length || columnIndex < 0) {
                return defaultValue;
            }

            int v = defaultValue;

            int.TryParse(row[columnIndex], out v);

            return v;
        }

        public long GetLong(int columnIndex, long defaultValue)
        {
            if (columnIndex >= row.Length || columnIndex < 0) {
                return defaultValue;
            }

            long v = defaultValue;

            long.TryParse(row[columnIndex], out v);

            return v;
        }

        public float GetFloat(int columnIndex, float defaultValue)
        {
            if (columnIndex >= row.Length || columnIndex < 0) {
                return defaultValue;
            }

            float v = defaultValue;

            float.TryParse(row[columnIndex], out v);

            return v;
        }

        public bool GetBool(int columnIndex, bool defaultValue)
        {
            if (columnIndex >= row.Length || columnIndex < 0) {
                return defaultValue;
            }

            int iv = 0;
            if (int.TryParse(row[columnIndex], out iv)) {
                return iv == 1;
            }

            bool v = defaultValue;

            bool.TryParse(row[columnIndex], out v);

            return v;
        }

        public string GetString(string columnName, string defaultValue)
        {
            return GetString(ColumnIndex(columnName), defaultValue);
        }

        public char GetChar(string columnName, char defaultValue)
        {
            return GetChar(ColumnIndex(columnName), defaultValue);
        }

        public short GetShort(string columnName, short defaultValue)
        {
            return GetShort(ColumnIndex(columnName), defaultValue);
        }

        public int GetInt(string columnName, int defaultValue)
        {
            return GetInt(ColumnIndex(columnName), defaultValue);
        }

        public long GetLong(string columnName, long defaultValue)
        {
            return GetLong(ColumnIndex(columnName), defaultValue);
        }

        public float GetFloat(string columnName, float defaultValue)
        {
            return GetFloat(ColumnIndex(columnName), defaultValue);
        }

        public bool GetBool(string columnName, bool defaultValue)
        {
            return GetBool(ColumnIndex(columnName), defaultValue);
        }

        public string[] GetStringArray(int columnIndex, char separator = ',')
        {
            var str = GetString(columnIndex, string.Empty);
            var v = StringUtils.SplitString(str, separator);
            return v ?? EmptyStringArray;
        }

        public string[] GetStringArray(string columnName, char separator = ',')
        {
            return GetStringArray(ColumnIndex(columnName), separator);
        }

        public int[] GetIntArray(int columnName, char separator = ',')
        {
            string str = GetString(columnName, string.Empty);
            var v = StringUtils.ToIntArray(str, separator);
            return v ?? EmptyIntArray;
        }

        public int[] GetIntArray(string columnName, char separator = ',')
        {
            return GetIntArray(ColumnIndex(columnName), separator);
        }
    }
}
