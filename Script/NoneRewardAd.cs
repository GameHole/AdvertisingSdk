using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
namespace MiniGameSDK
{
    public class NoneRewardAd : IRewardAdAPI
    {
        public bool isNotUseAd { get; set; }
        public Action<bool> onClose { get; set; }

        public void AutoShow(Action<bool> onclose)
        {
            onclose?.Invoke(true);
        }

        public Task<bool> AutoShowAsync()
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            tcs.SetResult(true);
            return tcs.Task;
        }

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
