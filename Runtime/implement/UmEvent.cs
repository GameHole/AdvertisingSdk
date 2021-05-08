using System.Collections.Generic;
using Umeng;
using UnityEngine;
namespace MiniGameSDK
{
    
    public class UmEvent : IAnalyzeEvent
    {
        public UmEvent()
        {
            var paramAc = AScriptableObject.Get<UmParameter>();
            UmParameterBase param = null;
#if UNITY_ANDROID
            param = paramAc.android;
#elif UNITY_IOS
            param = paramAc.ios;
#endif
            if (param.debug)
            {
                Debug.Log($"umeng start appid = {param.appid} channal = {param.channal}");
            }
            GA.StartWithAppKeyAndChannelId(param.appid, param.channal);
            GA.SetLogEnabled(param.debug);
            if (param.debug)
                Debug.Log("umeng end");
        }
        public void SetEvent(string key)
        {
            GA.Event(key);
        }

        public void SetEvent(string key, Dictionary<string, string> value)
        {
            GA.Event(key, value);
        }
        public void SetEvent(string key, params KVPair[] pairs)
        {
            var dic = new Dictionary<string, string>();
            foreach (var item in pairs)
            {
                dic.Add(item.key, item.value);
            }
            GA.Event(key, dic);
        }
    }
}
