using Refinter;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace MiniGameSDK
{
    [Important(int.MinValue)]
    public class NoneBanner : IBannerAd
    {
        public Action<int> onClose { get ; set; }
        public Action onHide { get ; set; }

        public void Hide()
        {
            
        }

        public void Show()
        {
            
        }
    }
}
