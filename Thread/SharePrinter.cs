using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest
{
   public class SharePrinter
    {
        public static int usePrinter = 0;
        public static int computerCount = 3;
        //lock
        private static object UserPrinterLocker = new object();
        //mutex
        private static Mutex mtx = new Mutex();
        //读写锁
        //事件
        //信号量
        //互斥量
        //锁
        public static void TestThread()
        {
            Thread t;
            Random r = new Random();
            for(int i = 0; i < computerCount; i++)
            {
                t = new Thread(ThreadWithInterlocked);
                t.Name = string.Format("Thread {0}",i+1);
                Thread.Sleep(r.Next(3));
                t.Start();
            }
        }
        private static void ThreadWithInterlocked()
        {
            do
            {
                Thread.Sleep(1000);
            } while (!UsePrinter());
        }
        /// <summary>
        ///  sync with interlocked
        /// </summary>
        /// <returns></returns>
        private static bool UsePrinter()
        {
            if(0==Interlocked.Exchange(ref usePrinter, 1))
            {
                Console.WriteLine("{0} Get lock of printer", Thread.CurrentThread.Name);
                //
                // some work
                Thread.Sleep(1000);
                Interlocked.Exchange(ref usePrinter, 0);
                Console.WriteLine("{0} Realse lock of printer", Thread.CurrentThread.Name);
                return true;
            }
            else
            {
                Console.WriteLine("{0} was denied the lock of printer",Thread.CurrentThread.Name);
                return false;
            }
        }
        /// <summary>
        ///  sync with lock
        /// </summary>
        private static void UsePrinterWithLocked()
        {
            lock (UserPrinterLocker)
            {
                Console.WriteLine("{0} Get lock of printer", Thread.CurrentThread.Name);
                //
                // some work
                Thread.Sleep(1000);
                Interlocked.Exchange(ref usePrinter, 0);
                Console.WriteLine("{0} Realse lock of printer", Thread.CurrentThread.Name);
            }
        }
        /// <summary>
        /// sync with moniter
        /// </summary>
        private static void UsePrinterWithMoniter()
        {
            Monitor.Enter(UserPrinterLocker);
            try
            {
                Console.WriteLine("{0} Get lock of printer", Thread.CurrentThread.Name);
                //
                // some work
                Thread.Sleep(1000);
                Interlocked.Exchange(ref usePrinter, 0);
                Console.WriteLine("{0} Realse lock of printer", Thread.CurrentThread.Name);
            }
            finally
            {
                Monitor.Exit(UserPrinterLocker);
            }
        }
        /// <summary>
        /// 互斥量
        /// </summary>
        private static void UsePrinterWithMutex()
        {
            mtx.WaitOne();
            try
            {
                Console.WriteLine("{0} Get lock of printer", Thread.CurrentThread.Name);
                //
                // some work
                Thread.Sleep(1000);
                Interlocked.Exchange(ref usePrinter, 0);
                Console.WriteLine("{0} Realse lock of printer", Thread.CurrentThread.Name);
            }
            finally
            {
                mtx.ReleaseMutex();
            }
        }
    }
}
