using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MiniGameSDK
{
    public class YLHSDKInitor : MonoBehaviour
    {
        //public bool notUseAd;
        public bool debug;
        IRewardAdAPI reward;
        void Awake()
        {
            //reward.isNotUseAd = notUseAd;
#if UNITY_ANDROID
            Init();
#endif
        }
#if UNITY_ANDROID
        void Init()
        {
            var souse = AScriptableObject.Get<Values>();
            DynamicPermission.GetPermission(
                "android.permission.READ_PHONE_STATE", 
                "android.permission.ACCESS_COARSE_LOCATION",
                "android.permission.ACCESS_FINE_LOCATION",
                "android.permission.WRITE_EXTERNAL_STORAGE");

            YLHProvider.Get().CallStatic("InitSDK", "mark", souse.appid, debug);
            YLHProvider.Get().CallStatic("InitReward","mark", souse.rewardid);
            YLHProvider.Get().CallStatic("InitInterAd", "mark", souse.insterid);
            YLHProvider.log?.CallStatic("ShowLog", "mark", debug);
        }
#endif
    }
}
