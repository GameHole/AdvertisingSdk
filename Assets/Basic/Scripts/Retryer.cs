using System;
using System.Collections.Generic;
using UnityEngine;
namespace MiniGameSDK
{
    public interface IReloader
    {
        int RetryCount { get; }
        int IdCount { get; }
        void Reload(int id);
        Action<bool> onReloaded { get; set; }
    }
    public class Retryer :MonoBehaviour,IRetryer,IRetryerCtrl
    {
        List<RetryValue> values = new List<RetryValue>();
        NetworkReachability netState;
        public int basicCount;
        public bool loadInAwake = true;
        public bool IsRun { get => loadInAwake; set => loadInAwake = value; }

        public void Regist(IReloader action)
        {
            //Debug.Log("Regist = " + action);
            var v = new RetryValue(action,basicCount);
            //v.Load();
            v.Start();
            values.Add(v);
        }
        //private void Awake()
        //{
        //    netState = Application.internetReachability;
        //}
        private void Start()
        {
            netState = Application.internetReachability;
        }
        private void Update()
        {
            if (IsRun)
            {
                foreach (var item in values)
                {
                    if (item.isStart)
                    {
                        item.add += Time.deltaTime;
                        if (item.add >= item.time)
                        {
                            item.add = 0;
                            item.isStart = false;
                            item.Load();
                        }
                    }
                }
            }
            if (Application.internetReachability != netState)
            {
                Debug.Log($"net change from {netState} to {Application.internetReachability}");
                if (netState == NetworkReachability.NotReachable)
                {
                    foreach (var item in values)
                    {
                        if (!item.isLoaded)
                        {
                            //item.Load();
                            item.Start();
                            //item.isLoaded = true;
                        }
                    }
                }
                netState = Application.internetReachability;
            }
        }

        public void Load(IReloader action)
        {
            for (int i = 0; i < values.Count; i++)
            {
                if (values[i].action == action)
                {
                    values[i].Load();
                    break;
                }
            }
        }

        public class RetryValue
        {
            public bool isLoaded = true;
            public float add;
            public float time;
            public bool isStart;
            int _id;
            int retryCount;
            public int basicCount;
            internal int loadId
            {
                get
                {
                    return _id++ % action.IdCount;
                }
            }
            internal IReloader action;
            public RetryValue(IReloader action,int basicCount)
            {
                this.action = action;
                this.basicCount = basicCount;
                this.action.onReloaded += (v) =>
                {
                    isLoaded = v;
                    bool canRetry = basicCount == -1 ? true : retryCount < action.RetryCount + basicCount;
                    if (!v && canRetry)
                    {
                        Restart();
                    }
                    else
                    {
                        Clear();
                    }
                };
            }
            public void Load()
            {
                //Debug.Log("load = " + action);
                //if (action.onReloaded == null)
                //{

                //}
                
                try
                {
                    action.Reload(loadId);
                }
                catch (Exception)
                {
                    Restart();
                }
            }
            public void Start()
            {
                add = 0;
                time = 0;
                isStart = true;
            }
            void Restart()
            {
                retryCount++;
                //Debug.Log(action +" retry count " + retryCount);
                add = 0;
                time = getNextTime(time);
                isStart = true;
            }
            float getNextTime(float basicTime)
            {
                if (basicCount == -1)
                    return 3;
                return basicTime + 1;
            }
            void Clear()
            {
                retryCount = 0;
                add = 0;
                time = 0;
                isStart = false;
            }
        }
    }
}
