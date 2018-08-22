using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest
{
    public class AutoEventLock
    {
        //private static AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        private static ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        public static void Test()
        {
            Thread t1 = new Thread(ThreadPro1);
            Thread t2 = new Thread(ThreadPro2);
            t1.Start();
            t2.Start();
            // autoResetEvent.Reset();
            manualResetEvent.Reset();
            Console.WriteLine("main thread end...");
        }
        private static void ThreadPro1()
        {
           //Console.WriteLine("pro1 wait...");
           // autoResetEvent.WaitOne();
            Console.WriteLine("pro1 start ...");
            Thread.Sleep(2000);
            //autoResetEvent.Set();
            manualResetEvent.Set();
           // manualResetEvent.Reset();
            //autoResetEvent.Reset();
        }
        private static  void ThreadPro2()
        {
            Console.WriteLine("pro2 wait...");
            // autoResetEvent.WaitOne();
            manualResetEvent.WaitOne();
            Console.WriteLine("pro2 start...");
            Thread.Sleep(2000);
            //autoResetEvent.Set();
            manualResetEvent.Set();
        }
    }
  
}
