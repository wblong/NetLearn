using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest
{
    /// <summary>
    /// 线程安全的缓存,读写锁；
    /// </summary>
    public class ReadWriteLockTest
    {
        private ReaderWriterLockSlim RWLockSlim = new ReaderWriterLockSlim();
        private Dictionary<int, string> innerCache = new Dictionary<int, string>();
        /// <summary>
        /// Read
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Read(int key)
        {
            RWLockSlim.EnterReadLock();
            try
            {
                return innerCache[key];
            }
            finally
            {
                RWLockSlim.ExitReadLock();
            }
        }
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(int key,string value)
        {
            RWLockSlim.EnterWriteLock();
            try
            {
                innerCache.Add(key, value);
            }
            finally
            {
                RWLockSlim.ExitReadLock();
            }
        }
        /// <summary>
        /// 超时版添加值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool AddWithTimeOut(int key,string value,int timeout)
        {
            if (RWLockSlim.TryEnterWriteLock(timeout))
            {
                try
                {
                    innerCache.Add(key, value);
                }
                finally
                {
                    RWLockSlim.ExitWriteLock();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public AddOrUpdateStatus AddOrUpdate(int key,string value)
        {
            RWLockSlim.EnterUpgradeableReadLock();
            try
            {
                string result=null;
                if(innerCache.TryGetValue(key,out result))
                {
                    if (result == value)
                        return AddOrUpdateStatus.UnChanged;
                    else
                    {
                        RWLockSlim.EnterWriteLock();
                        try
                        {
                            innerCache[key] = value;
                        }
                        finally
                        {
                            RWLockSlim.ExitWriteLock();
                        }
                        return AddOrUpdateStatus.Updated;
                    }
                }
                else
                {
                    RWLockSlim.EnterWriteLock();
                    try
                    {
                        innerCache.Add(key, value);

                    }
                    finally
                    {
                        RWLockSlim.ExitWriteLock();
                    }
                    return AddOrUpdateStatus.Added;
                }
                
            }
            finally
            {
                RWLockSlim.ExitUpgradeableReadLock();
            }
        }
        public enum AddOrUpdateStatus
        {
            Added,
            Updated,
            UnChanged
        };
    }
}
