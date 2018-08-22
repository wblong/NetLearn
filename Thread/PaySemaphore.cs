using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest
{
    /// <summary>
    /// 信号量支付系统测试
    /// </summary>
    public class PaySemaphore
    {
        private static Semaphore idleCasheses = new Semaphore(0, 3);
        public static void TestPay()
        {
            ParameterizedThreadStart start = new ParameterizedThreadStart(Pay);
            for(int i = 1; i <= 10; i++)
            {
                Thread t = new Thread(start);
                t.Start(i);
            }
            Thread.Sleep(1000);
            idleCasheses.Release(3);
        }
        private static void Pay(object obj) {
            Console.WriteLine("{0} wait to buy tickets",obj);
            idleCasheses.WaitOne();
            Console.WriteLine("{0} buy a ticket", obj);
            Console.WriteLine("{0} finished to buy a ticket",obj);
            idleCasheses.Release();
        }
    }
}
