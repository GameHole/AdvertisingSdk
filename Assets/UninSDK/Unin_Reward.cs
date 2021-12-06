using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using Refinter;
using System;
using System.Threading.Tasks;

namespace UninSDK
{
    [Important(100)]
    public class Unin_Reward :IRewardAdAPI,IInitializable
    {
        InterfaceCntr<IRewardAdAPI> cntr = new InterfaceCntr<IRewardAdAPI>();
        bool debug;
        public bool isNotUseAd
        {
            get => debug;
            set
            {
                debug = value;
                foreach (var item in cntr.instances)
                {
                    item.isNotUseAd = value;
                }
            }
        }
        public event Action<bool> onClose;
        IRewardAdAPI cur;
        public void AutoShow(Action<bool> onclose)
        {
            //Debug.Log(cur);
            cur?.AutoShow(onclose);
        }

        public async Task<bool> AutoShowAsync()
        {
            if (cur == null)
                return true;
            bool r = await cur.AutoShowAsync();
            cur = cntr.GetNext();
            //onClose?.Invoke(r);
            return r;
        }

        public void Initialize()
        {
            cntr.Init(GetType());
            cur = cntr.GetNext();
            onClose += (v) =>
            {
                cur = cntr.GetNext();
            };
            foreach (var item in cntr.instances)
            {
                item.onClose += (v) =>
                {
                    onClose?.Invoke(v);
                }; 
            }
        }

        public bool isReady()
        {
            return cur != null && cur.isReady();
        }
    }
}
