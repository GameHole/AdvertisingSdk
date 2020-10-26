using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
namespace MiniGameSDK
{
#if UNITY_ANDROID
    public class YHLInterAD:IInterstitialAdAPI
    {
        public Action<bool> _onclose;
        public YHLInterAD()
        {
            LoadAd();
        }

        public event Action<bool> onClose
        {
            add => _onclose += value;
            remove => _onclose -= value;
        }

        public bool isReady()
        {
            return YLHProvider.Get().CallStatic<bool>("isInterReady", "mark");
        }

        public void Show()
        {
            YLHProvider.Get().CallStatic("ShowInterAd", "mark");
        }
        private async void LoadAd()
        {
            await Task.Delay(200);
            YLHProvider.Get().CallStatic("LoadInterAd", "mark", new InterAndroid() { ad = this });
        }
    }

    class InterAndroid : AndroidJavaProxy//, IInterstitialAdAPI
    {
        public YHLInterAD ad;

        public InterAndroid() : base("com.unity.unityylh.IYlhInterCallBack")
        {
        }
        void onAdShow()
        {

        }

        void onAdClick()
        {

        }

        void onAdDismiss()
        {
            ad._onclose?.Invoke(true);
        }
    }
#endif
}

