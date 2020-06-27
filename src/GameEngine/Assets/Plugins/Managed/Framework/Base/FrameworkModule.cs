using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    /// <summary>
    /// 游戏框架模块抽象类
    /// </summary>
    public abstract class FrameworkModule
    {
        internal virtual int Priority
        {
            get {
                return 0;
            }
        }

        internal abstract void Update(float elapseSeconds, float realElapseSeconds);

        internal abstract void Shutdown();
    }
}
