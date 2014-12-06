using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LD31
{
    class Program
    {
        static void Main(string[] args)
        {
            Graphics.GraphicsManager.Init();
            while (true)
            {
                 Graphics.GraphicsManager.Update();
                Thread.Sleep(1);
            }
        }
    }
}
