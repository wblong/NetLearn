using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest
{
    //public delegate void ThreadStart();
    class Program
    {
       
        static void Main(string[] args)
        {

            //SharePrinter.TestThread();
            // PaySemaphore.TestPay();
            //SpinLockTest.Test();
            TestSpinWait.Test();
            Console.Read();

        }
    }
}
