using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
namespace MiniGameSDK
{
#if UNITY_ANDROID
    class AndroidRewardAd : IRewardAdAPI,IRewardAdClicked
    {
        public bool isNotUseAd { get; set; }
        public Action OnAdClicked { get; set ; }

        public event Action<bool> onClose;
        public TaskCompletionSource<bool> tcs;

        public Action<bool> onClosed;
        public void AutoShow(Action<bool> onclose)
        {
            onClosed = onclose + onClose;
            Show();
        }
        public AndroidRewardAd()
        {
            LoadAd();
        }
        async void LoadAd()
        {
            await Task.Delay(200);
            YLHProvider.Get().CallStatic("LoadRewardAd", "mark", new YLHProxy() { ad = this });
        }
        public bool isReady()
        {
            return YLHProvider.Get().CallStatic<bool>("isReady", "mark");
        }

        public void Show()
        {
            if (isNotUseAd)
            {
                onClosed?.Invoke(true);
                tcs?.SetResult(true);
                return;
            }
            YLHProvider.Get().CallStatic("ShowRewardAd", "mark");
        }

        public Task<bool> AutoShowAsync()
        {
            tcs = new TaskCompletionSource<bool>();
            Show();
            return tcs.Task;
        }
    }

    class YLHProxy : AndroidJavaProxy
    {
        public AndroidRewardAd ad;
        public YLHProxy() : base("com.unity.unityylh.IYlhRewardCallBack") { }
        bool isReward;
        void onAdShow()
        {
            Debug.Log("Reward onAdShow");
            isReward = false;
        }

        void onAdClick()
        {
            ad.OnAdClicked?.Invoke();
            Debug.Log("Reward onAdClick");
        }

        void onAdClose()
        {
            ad.onClosed?.Invoke(isReward);
            ad.tcs.SetResult(isReward);
            Debug.Log("Reward onAdClose");
        }

        void onVideoComplete()
        {
            Debug.Log("Reward onVideoComplete");
        }

        void onVideoSkipped()
        {
            Debug.Log("Reward onVideoSkipped");
        }

        void onRewardVerify(bool rewardVerify, int rewardAmount, String rewardName)
        {
            isReward = true;
            Debug.Log("Reward onRewardVerify");
        }
    }
#endif
}

