using System;
using System.Collections.Generic;
using UnityEngine;
namespace MiniGameSDK
{
	public class PriorityUnin<T>:IReloader,IInitializable where T:class,IInterface
	{
        public List<T> cntr = new List<T>();
        int c;
        public T current;
        int idx;
        int retryCount;
        public int RetryCount => retryCount;

        public int IdCount => c;

        public Action<bool> onReloaded { get; set; }

        public void Add(T value)
        {
            cntr.Add(value);
        }
       
        public virtual void Initialize()
        {
            if (cntr.Count <1)
            {
                throw new ArgumentException("PriorityUnin must contain last then 2 item to work," +
                    "please call 'Add()' to add item in 'Prepare()' methord");
            }
            c = 0;
            retryCount = 0;
            for (int i = 0; i < cntr.Count; i++)
            {
                var load = cntr[i] as IReloader;
                if (load != null)
                {
                    c += load.IdCount;
                    retryCount += load.RetryCount;
                    load.onReloaded += (v) =>
                    {
                        onReloaded?.Invoke(v);
                    };
                }
            }
            onReloaded += (isSuccess) =>
            {
                if (isSuccess)
                {
                    current = GetItem(idx - 1);
                    idx = 0;
                }
            };
            foreach (var item in cntr)
            {
                (item as IInitializable)?.Initialize();
            }
            Refinter.Reflection.Get<IRetryer>()?.Regist(this);
        }

        T GetItem(int idx)
        {
            for (int i = 0; i < cntr.Count; i++)
            {
                var load = cntr[i] as IReloader;
                if (load != null)
                {
                    idx -= load.IdCount;
                }
                if (idx < 0)
                {
                    return cntr[i];
                }
            }
            return cntr[cntr.Count - 1];
        }
        public virtual void Reload(int id)
        {
            current = null;
            //Debug.Log($"unin idx::{idx}");
            var next = GetItem(idx++);
            if (next is IReloader reloader)
            {
                reloader.Reload(id % reloader.IdCount);
            }
            else
            {
                onReloaded?.Invoke(true);
            }
        }
    }
}
