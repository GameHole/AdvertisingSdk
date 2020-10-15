using System;
using System.Collections.Generic;
using UnityEngine;
namespace MiniGameSDK
{
    public class NoneInsterAd : IInterstitialAdAPI
    {
        public Action<bool> onClose { get; set; }

        public bool isReady()
        {
            return true;
        }

        public void Show()
        {
            onClose?.Invoke(true);
        }
    }
}
