﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGameSDK
{
    public struct KVPair
    {
        public string key;
        public string value;
        public KVPair(string k,string v)
        {
            key = k;
            value = v;
        }
    }
    public interface IAnalyzeEvent : IInterface
    {
        void SetEvent(string key);
        void SetEvent(string key, Dictionary<string, string> value);
        void SetEvent(string key, params KVPair[] pairs);
    }
}
