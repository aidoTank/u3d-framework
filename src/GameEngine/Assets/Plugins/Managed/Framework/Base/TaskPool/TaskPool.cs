using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    /// <summary>
    /// 任务池。
    /// </summary>
    /// <typeparam name="T">任务类型</typeparam>
    public class TaskPool<T> where T : TaskBase
    {
        private readonly Stack<ITaskAgent<T>> m_FreeAgents;
        private readonly LinkedList<ITaskAgent<T>> m_WorkingAgents;
        private readonly LinkedList<T> m_WaitingTasks;
        private bool m_Paused;

        public TaskPool()
        {
            m_FreeAgents = new Stack<ITaskAgent<T>>();
            m_WorkingAgents = new LinkedList<ITaskAgent<T>>();
            m_WaitingTasks = new LinkedList<T>();
            m_Paused = false;
        }

        public bool Paused
        {
            get {
                return m_Paused;
            }
            set {
                m_Paused = value;
            }
        }

        public int FreeAgentCount
        {
            get {
                return m_FreeAgents.Count;
            }
        }

        public int WorkingAgentCount
        {
            get {
                return m_WorkingAgents.Count;
            }
        }

        public int TotalAgentCount
        {
            get {
                return FreeAgentCount + WorkingAgentCount;
            }
        }

        public void AddAgent(ITaskAgent<T> agent)
        {

        }

        public void AddTask(T task)
        {

        }

        public void RemoveTask(int taskId)
        {

        }

        public void RemoveAllTask()
        {

        }
    }
}
