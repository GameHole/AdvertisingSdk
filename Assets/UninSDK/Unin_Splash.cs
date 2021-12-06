using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using System;

namespace UninSDK
{
    [Refinter.Important(100)]
    public class Unin_Splash : ISplashAd,IInitializable
    {
        InterfaceCntr<ISplashAd> cntr = new InterfaceCntr<ISplashAd>();
        public Action OnClsoe { get; set; }
        ISplashAd cur;
        public void Initialize()
        {
            cntr.Init(GetType());
            cur = cntr.GetNext();
            OnClsoe += () =>
            {
                cur = cntr.GetNext();
            };
            foreach (var item in cntr.instances)
            {
                item.OnClsoe += OnClsoe;
            }
        }

        public void Show()
        {
            cur?.Show();
        }
    }
}
