using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
namespace MiniGameSDK
{
    public class YLHEvent
    {
        public static void SendRegEvent(string ch,bool isSuccess)
        {
            YLHProvider.log?.CallStatic("SendRigEve", "mark", ch, isSuccess);
        }
        public static void SendAchievement(string parm, int lv)
        {
            YLHProvider.log?.CallStatic("onEventAchievement", "mark", parm, lv);
        }
        public static void SendEvent(string key,Dictionary<string,string> values)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in values)
            {
                builder.Append(item.Key);
                builder.Append(":");
                builder.Append(item.Value);
                builder.Append(",");
            }
            if(builder.Length>0)
            builder.Remove(builder.Length - 1, 1);
            Debug.Log(builder.ToString());
            YLHProvider.log?.CallStatic("SendEvent", "mark", key, builder.ToString());
        }
    }
}

