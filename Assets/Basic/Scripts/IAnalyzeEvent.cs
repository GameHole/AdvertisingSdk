using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGameSDK
{
    public interface IAnalyzeEvent : IInterface
    {
        void SetEvent(string key);
        void SetEvent(string key, Dictionary<string, string> value);
    }
}
