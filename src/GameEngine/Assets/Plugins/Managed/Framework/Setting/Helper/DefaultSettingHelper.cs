using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public class DefaultSettingHelper : SettingHelperBase
    {
        public override int Count => throw new NotImplementedException();

        public override string[] GetAllSettingNames()
        {
            throw new NotImplementedException();
        }

        public override void GetAllSettingNames(List<string> results)
        {
            throw new NotImplementedException();
        }

        public override bool GetBool(string settingName)
        {
            throw new NotImplementedException();
        }

        public override bool GetBool(string settingName, bool defaultValue)
        {
            throw new NotImplementedException();
        }

        public override float GetFloat(string settingName)
        {
            throw new NotImplementedException();
        }

        public override float GetFloat(string settingName, float defaultValue)
        {
            throw new NotImplementedException();
        }

        public override int GetInt(string settingName)
        {
            throw new NotImplementedException();
        }

        public override int GetInt(string settingName, int defaultValue)
        {
            throw new NotImplementedException();
        }

        public override T GetObject<T>(string settingName)
        {
            throw new NotImplementedException();
        }

        public override object GetObject(Type objectType, string settingName)
        {
            throw new NotImplementedException();
        }

        public override T GetObject<T>(string settingName, T defaultObj)
        {
            throw new NotImplementedException();
        }

        public override object GetObject(Type objectType, string settingName, object defaultObj)
        {
            throw new NotImplementedException();
        }

        public override string GetString(string settingName)
        {
            throw new NotImplementedException();
        }

        public override string GetString(string settingName, string defaultValue)
        {
            throw new NotImplementedException();
        }

        public override bool HasSetting(string settingName)
        {
            throw new NotImplementedException();
        }

        public override bool Load()
        {
            throw new NotImplementedException();
        }

        public override void RemoveAllSettings()
        {
            throw new NotImplementedException();
        }

        public override bool RemoveSetting(string settingName)
        {
            throw new NotImplementedException();
        }

        public override bool Save()
        {
            throw new NotImplementedException();
        }

        public override void SetBool(string settingName, bool value)
        {
            throw new NotImplementedException();
        }

        public override void SetFloat(string settingName, float value)
        {
            throw new NotImplementedException();
        }

        public override void SetInt(string settingName, int value)
        {
            throw new NotImplementedException();
        }

        public override void SetObject<T>(string settingName, T obj)
        {
            throw new NotImplementedException();
        }

        public override void SetObject(string settingName, object obj)
        {
            throw new NotImplementedException();
        }

        public override void SetString(string settingName, string value)
        {
            throw new NotImplementedException();
        }
    }
}
