using Refinter;
using System.Collections.Generic;
using UnityEngine;
namespace MiniGameSDK
{
    [Important(-1000)]
    public class NoneEvent : IAnalyzeEvent
    {
        public void SetEvent(string key)
        {
            
        }

        public void SetEvent(string key, Dictionary<string, string> value)
        {
            
        }

        public void SetEvent(string key, params KVPair[] pairs)
        {
            
        }
    }
}
