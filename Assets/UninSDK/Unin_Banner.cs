using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using System;

namespace UninSDK
{
    [Refinter.Important(100)]
    public class Unin_Banner : IBannerAd,IInitializable
    {
        IBannerAd cur;
        InterfaceCntr<IBannerAd> cntr = new InterfaceCntr<IBannerAd>();
        public Action<int> onClose { get; set; }
        public Action onHide { get ; set; }

        public event Action onShow;

        public void Hide()
        {
            cur?.Hide();
        }

        public void Initialize()
        {
            cntr.Init(GetType());
            cur = cntr.GetNext();
            onClose += (v) =>
            {
                cur = cntr.GetNext();
                //Debug.Log($"banner on close aaaaaaaa {cur}");
            };
            onHide += () =>
            {
                cur = cntr.GetNext();
                //Debug.Log($"banner on hide bbbb {cur}");
            };
            foreach (var item in cntr.instances)
            {
                item.onClose += (v)=>
                {
                    onClose?.Invoke(v);
                    //Debug.Log($"banner on onClose  type::{cur}");
                };
                item.onHide += ()=>
                {
                    onHide?.Invoke();
                    //Debug.Log($"banner on onHide  type::{cur}");
                };
                //item.onClose += onClose;
                //item.onHide += onHide;
            }
        }

        public void Show()
        {
            cur?.Show();
        }
    }
}
