using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using System;

namespace UninSDK
{
    [Refinter.Important(100)]
    public class Unin_Inter : IInterstitialAdAPI,IInitializable
    {
        InterfaceCntr<IInterstitialAdAPI> cntr = new InterfaceCntr<IInterstitialAdAPI>();
        public event Action<bool> onClose;
        IInterstitialAdAPI curr;
        public void Initialize()
        {
            cntr.Init(GetType());
            curr = cntr.GetNext();
            onClose += (v) =>
            {
                curr = cntr.GetNext();
            };
            foreach (var item in cntr.instances)
            {
                item.onClose += onClose;
            }
        }
        public bool isReady()
        {
            return curr != null && curr.isReady();
        }

        public void Show()
        {
            curr?.Show();
        }
    }
}
