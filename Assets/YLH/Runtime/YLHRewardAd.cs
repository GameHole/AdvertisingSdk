using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
namespace MiniGameSDK
{
    class RewardMono:MonoBehaviour
    {
        public AndroidRewardAd android;
        private void OnApplicationFocus(bool focus)
        {
            //Debug.Log(focus);
            //Debug.Log(android.isShowed);
            if (focus && android.isShowed)
            {
                android.LoadAd();
                //await Task.Delay(200);
                //android.ShowInternal();
            }
        }
    }
#if UNITY_ANDROID
    class AndroidRewardAd : IRewardAdAPI,IRewardAdClicked
    {
        internal RewardMono mono;
        public bool isNotUseAd { get; set; }
        public Action OnAdClicked { get; set ; }
        internal bool isShowed;
        public event Action<bool> onClose;
        public TaskCompletionSource<bool> tcs;
        YLHProxy proxy;
        public Action<bool> onClosed;
        public void OnClosedInternal(bool v)
        {
            onClose?.Invoke(v);
        }
        public void AutoShow(Action<bool> onclose)
        {
            this.onClosed = onclose;
            Show();
        }
        public AndroidRewardAd()
        {
            Init();
            LoadAd();
        }
        void Init()
        {
            var o = new GameObject();
            o.hideFlags = HideFlags.DontSave | HideFlags.HideInHierarchy;
            mono = o.AddComponent<RewardMono>();
            mono.android = this;
            proxy = new YLHProxy()
            {
                ad = this
            };
        }
        internal async void LoadAd()
        {
            await Task.Delay(200);
            YLHProvider.Get().CallStatic("LoadRewardAd", "mark", proxy);
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
                onClosed = null;
                if (tcs != null && !tcs.Task.IsCompleted)
                    tcs.SetResult(true);
                OnClosedInternal(true);
                return;
            }
            isShowed = true;
            ShowInternal();
        }
        internal void ShowInternal()
        {
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
            ad.onClosed = null;
            ad.OnClosedInternal(isReward);
            ad.isShowed = false;
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

