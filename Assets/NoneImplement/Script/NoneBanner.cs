using Refinter;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace MiniGameSDK
{
    [Important(-1000)]
    public class NoneBanner : IBannerAd
    {
        public Action<int> onClose { get ; set; }
        public Action onHide { get ; set; }

        public event Action onShow;

        public void Hide()
        {
            onHide?.Invoke();
            onClose?.Invoke(0);
        }

        public void Show()
        {
            onShow?.Invoke();
        }
    }
}
