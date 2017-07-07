// ========================================
// Copyright (c) 2017 KingSoft, All rights reserved.
// http://www.kingsoft.com
// 
// Framwork
// 
// Filename: AssetAssociate.cs
// Date:     2017/06/09
// Author:   xiangjinbao
// Email:    xiangjinbao@kingsoft.com
// ========================================

using System.Collections.Generic;

namespace xsj.framework
{
    /// <summary>
    /// 文件的关联
    /// </summary>
    public class AssetAssociate
    {
        public string Url;

        private Dictionary<string, bool> m_associates = new Dictionary<string, bool>();

        public void SetAssociate(string acPath, bool bMemory)
        {
            if (m_associates.ContainsKey(acPath)) {
                m_associates[acPath] = bMemory;
            } else {
                m_associates.Add(acPath, bMemory);
            }
        }

        public bool GetAAState(string acPath)
        {
            if (HasAA(acPath)) {
                return m_associates[acPath];
            }
            return false;
        }

        public bool HasAA(string acPath)
        {
            return m_associates.ContainsKey(acPath);
        }

        public bool HasAssociateInMemory
        {
            get {
                foreach (KeyValuePair<string, bool> ac in m_associates) {
                    if (ac.Value)
                        return true;
                }
                return false;
            }
        }
    }
}
