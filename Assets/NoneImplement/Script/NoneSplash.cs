using Refinter;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace MiniGameSDK
{
    [Important(int.MinValue)]
    public class NoneSplash : ISplashAd
    {
        public Action OnClsoe { get; set ; }

        public void Show()
        {
            OnClsoe?.Invoke();
        }
    }
}
