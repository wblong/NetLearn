using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest
{
   public class SpinLockTest
    {
        public static void Test()
        {
            SpinLock spinlock = new SpinLock();
            StringBuilder sb = new StringBuilder();
            Action action = () => {
                bool gotoLock = false;
                for(int i = 0; i < 100; i++)
                {
                    gotoLock = false;
                    try
                    {
                        spinlock.Enter(ref gotoLock);
                        sb.Append($"{i},");
                    }
                    finally
                    {
                        if(gotoLock)
                            spinlock.Exit();
                        //
                    }
                }
            };
            Parallel.Invoke(action, action, action);
            Console.WriteLine("输出：{0}",sb.ToString());
        }
    }
}
