// ========================================
// Copyright (c) 2017 KingSoft, All rights reserved.
// http://www.kingsoft.com
// 
// Framwork
// 
// Filename: LoadStrategy.cs
// Date:     2017/06/09
// Author:   xiangjinbao
// Email:    xiangjinbao@kingsoft.com
// ========================================

using UnityEngine;

namespace xsj.framework
{
    public class LoadStrategy
    {
        private ThreadPriority m_threadPriority;
        private int m_maxLoadCount;

        public ThreadPriority ThreadPriority
        {
            get {
                return m_threadPriority;
            }
        }

        public int MaxLoadCount
        {
            get {
                return m_maxLoadCount;
            }
        }

        public LoadStrategy(ThreadPriority threadPriority, int maxLoadCount)
        {
            this.m_threadPriority = threadPriority;
            this.m_maxLoadCount = maxLoadCount;
        }
    }
}
