using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/***
 * IManager.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public interface IManager
    {
        void Init();

        void Update(uint elapsed);

        void Clear();
    }
}
