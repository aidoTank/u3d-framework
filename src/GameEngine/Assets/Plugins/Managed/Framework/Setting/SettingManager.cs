﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public class SettingManager : FrameworkModule, ISettingManager
    {
        private ISettingHelper m_SettingHelper;

        public SettingManager()
        {
            m_SettingHelper = new DefaultSettingHelper();
        }

        public int Count
        {
            get {
                return m_SettingHelper.Count;
            }
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        internal override void Shutdown()
        {
            Save();
        }

        public bool Load()
        {
            return m_SettingHelper.Load();
        }

        public bool Save()
        {
            return m_SettingHelper.Save();
        }

        public string[] GetAllSettingNames()
        {
            return m_SettingHelper.GetAllSettingNames();
        }

        public void GetAllSettingNames(List<string> results)
        {
            m_SettingHelper.GetAllSettingNames(results);
        }

        public bool HasSetting(string settingName)
        {
            if (string.IsNullOrEmpty(settingName)) {
                throw new FrameworkException("Setting name is invalid.");
            }
            return m_SettingHelper.HasSetting(settingName);
        }

        public bool RemoveSetting(string settingName)
        {
            if (string.IsNullOrEmpty(settingName)) {
                throw new FrameworkException("Setting name is invalid.");
            }
            return m_SettingHelper.RemoveSetting(settingName);
        }

        public void RemoveAllSettings()
        {
            m_SettingHelper.RemoveAllSettings();
        }

        public bool GetBool(string settingName)
        {
            if (string.IsNullOrEmpty(settingName)) {
                throw new FrameworkException("Setting name is invalid.");
            }
            return m_SettingHelper.GetBool(settingName);
        }

        public bool GetBool(string settingName, bool defaultValue)
        {
            if (string.IsNullOrEmpty(settingName)) {
                throw new FrameworkException("Setting name is invalid.");
            }
            return m_SettingHelper.GetBool(settingName, defaultValue);
        }

        public void SetBool(string settingName, bool value)
        {
            if (string.IsNullOrEmpty(settingName)) {
                throw new FrameworkException("Setting name is invalid.");
            }
            m_SettingHelper.SetBool(settingName, value);
        }

        public int GetInt(string settingName)
        {
            if (string.IsNullOrEmpty(settingName)) {
                throw new FrameworkException("Setting name is invalid.");
            }
            return m_SettingHelper.GetInt(settingName);
        }

        public int GetInt(string settingName, int defaultValue)
        {
            if (string.IsNullOrEmpty(settingName)) {
                throw new FrameworkException("Setting name is invalid.");
            }
            return m_SettingHelper.GetInt(settingName, defaultValue);
        }

        public void SetInt(string settingName, int value)
        {
            if (string.IsNullOrEmpty(settingName)) {
                throw new FrameworkException("Setting name is invalid.");
            }
            m_SettingHelper.SetInt(settingName, value);
        }

        public float GetFloat(string settingName)
        {
            if (string.IsNullOrEmpty(settingName)) {
                throw new FrameworkException("Setting name is invalid.");
            }
            return m_SettingHelper.GetFloat(settingName);
        }

        public float GetFloat(string settingName, float defaultValue)
        {
            if (string.IsNullOrEmpty(settingName)) {
                throw new FrameworkException("Setting name is invalid.");
            }
            return m_SettingHelper.GetFloat(settingName, defaultValue);
        }

        public void SetFloat(string settingName, float value)
        {
            if (string.IsNullOrEmpty(settingName)) {
                throw new FrameworkException("Setting name is invalid.");
            }
            m_SettingHelper.SetFloat(settingName, value);
        }

        public string GetString(string settingName)
        {
            if (string.IsNullOrEmpty(settingName)) {
                throw new FrameworkException("Setting name is invalid.");
            }
            return m_SettingHelper.GetString(settingName);
        }

        public string GetString(string settingName, string defaultValue)
        {
            if (string.IsNullOrEmpty(settingName)) {
                throw new FrameworkException("Setting name is invalid.");
            }
            return m_SettingHelper.GetString(settingName, defaultValue);
        }

        public void SetString(string settingName, string value)
        {
            if (string.IsNullOrEmpty(settingName)) {
                throw new FrameworkException("Setting name is invalid.");
            }
            m_SettingHelper.SetString(settingName, value);
        }

        public T GetObject<T>(string settingName)
        {
            if (string.IsNullOrEmpty(settingName)) {
                throw new FrameworkException("Setting name is invalid.");
            }
            return m_SettingHelper.GetObject<T>(settingName);
        }

        public object GetObject(Type objectType, string settingName)
        {
            if (objectType == null) {
                throw new FrameworkException("Object type is invalid.");
            }
            if (string.IsNullOrEmpty(settingName)) {
                throw new FrameworkException("Setting name is invalid.");
            }
            return m_SettingHelper.GetObject(objectType, settingName);
        }

        public T GetObject<T>(string settingName, T defaultObj)
        {
            if (string.IsNullOrEmpty(settingName)) {
                throw new FrameworkException("Setting name is invalid.");
            }
            return m_SettingHelper.GetObject(settingName, defaultObj);
        }

        public object GetObject(Type objectType, string settingName, object defaultObj)
        {
            if (objectType == null) {
                throw new FrameworkException("Object type is invalid.");
            }
            if (string.IsNullOrEmpty(settingName)) {
                throw new FrameworkException("Setting name is invalid.");
            }
            return m_SettingHelper.GetObject(objectType, settingName, defaultObj);
        }

        public void SetObject<T>(string settingName, T obj)
        {
            if (string.IsNullOrEmpty(settingName)) {
                throw new FrameworkException("Setting name is invalid.");
            }
            m_SettingHelper.SetObject(settingName, obj);
        }

        public void SetObject(string settingName, object obj)
        {
            if (string.IsNullOrEmpty(settingName)) {
                throw new FrameworkException("Setting name is invalid.");
            }
            m_SettingHelper.SetObject(settingName, obj);
        }
    }
}
