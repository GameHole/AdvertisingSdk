﻿using System;
using System.Collections.Generic;
using UnityEngine;
namespace MiniGameSDK
{
    public class NoneInsterAd : IInterstitialAdAPI
    {
        public event Action<bool> onClose;

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
