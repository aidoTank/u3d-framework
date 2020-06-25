using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public interface ITaskAgent<T> where T : TaskBase 
    {
        T Task
        {
            get;
        }

        void Init();

        void Update(float elapseSeconds, float realElapseSeconds);

        TaskStatus Start(T task);

        void Reset();

        void Shotdown();
    }
}
