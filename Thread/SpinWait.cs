using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest
{
    /// <summary>
    /// 测试spin wait 旋转锁
    /// </summary>
    public class TestSpinWait
    {
       private static LockFreeStack<string> stack = new LockFreeStack<string>();
        public static void Test()
        {
            ThreadPush();
            Thread t1 = new Thread(ThreadPop);
            Thread t2 = new Thread(ThreadPop);
            t1.Start();
            t2.Start();
            
        }
       private static void ThreadPush()
        {
            for(int i = 0; i < 10; i++)
            {
                stack.push($"{i}");
                Console.WriteLine($"{i}进栈");
            }
        }
        private static void ThreadPop()
        {
           for(int i=0;i<10;i++)
            {
                string result;
                if (stack.TryPop(out result))
                {
                    Console.WriteLine($"{result}出栈");
                }
            }
        }

    }
    public class LockFreeStack<T>
    {
        private volatile Node m_head;
        private class Node { public Node next; public T value; }

        public void push(T val) {
            var spin = new SpinWait();
            Node node = new Node() { value = val }, head;
            while (true)
            {
                head = m_head;
                node.next = head;
                if (Interlocked.CompareExchange(ref m_head, node, head) == head) break;
                spin.SpinOnce();
            }

        }
        public bool TryPop(out T result)
        {
            result = default(T);
            var spin = new SpinWait();
            Node head;
            
            while (true) {
                head = m_head;
                if (head == null) return false;
            
                if (Interlocked.CompareExchange(ref m_head,head.next, head) == head)
                {
                    result = head.value;
                    return true;
                }
                spin.SpinOnce();
            }
           
        }
    }
}
