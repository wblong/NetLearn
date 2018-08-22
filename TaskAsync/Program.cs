using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAsync
{
    class Program
    {
        static void Main(string[] args)
        {
            //action、func、task
            Task<int> task = new Task<int>(a => { return (int)a + 1; }, 1);
            task.Start();
            Console.WriteLine(task.Result);
            //func
            Task<int> task1 = new Task<int>((a)=> { return (int)a+ 1; },2);
            task1.Start();
            Console.WriteLine(task1.Result);
            //action
            Task task2 = new Task((a)=> { Console.WriteLine(a); },"a");
            task2.Start();
            CancellationTokenSource souce = new CancellationTokenSource();
            CancellationToken token = souce.Token;
            var t = Do.ExcuteAsync(token);
            Thread.Sleep(3000);   //挂起 3 秒
           // souce.Cancel();    //传达取消请求
            t.Wait(token);
            Console.WriteLine($"{nameof(token.IsCancellationRequested)}:{token.IsCancellationRequested}");
            Console.ReadLine();
        }
        internal class Do
        {
            /// <summary>
            /// task 任务
            /// </summary>
            /// <param name="token"></param>
            /// <returns></returns>
            public static async Task ExcuteAsync(CancellationToken token)
            {
                if (token.IsCancellationRequested)
                    return;
                await Task.Run(()=> { CircleOutput(token); });
                //await Task.Run(()=>CircleOutput(token),token);
               // await Task.Run(()=>CircleOutput(token));
            }
            /// <summary>
            ///CircleOutput 
            /// </summary>
            /// <param name="token"></param>
            private static void CircleOutput(CancellationToken token) {
                Console.WriteLine($"{nameof(CircleOutput)}开始执行！");
                for (int i = 0; i < 5; i++) {
                    if (token.IsCancellationRequested)
                        return;
                    Console.WriteLine($"{i+1}/5 finished!");
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
