using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MiniGameSDK
{
    public static class YLHProvider
    {
        static AndroidJavaClass ylh;
        static AndroidJavaClass _log;
        public static AndroidJavaClass log {
            get
            {
                if (_log == null)
                    _log = new AndroidJavaClass("com.unity.unityylh.YLHLog");
                return _log;
            }
        }
        public static AndroidJavaClass Get()
        {
            if (ylh == null)
                ylh = new AndroidJavaClass("com.unity.unityylh.AggregateAdvertising");
            return ylh;
        }
    }
}

