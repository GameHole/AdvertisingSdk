using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
namespace MiniGameSDK
{
    public abstract class AReward : IRewardAdAPI
    {
        public bool isNotUseAd { get; set; }

        public event Action<bool> onClose;
        TaskCompletionSource<bool> tcs;
        Action<bool> _onclose;
        public void AutoShow(Action<bool> onclose)
        {
            _onclose = onclose;
            ShowInternal();
        }

        public Task<bool> AutoShowAsync()
        {
            if (tcs == null || tcs.Task.IsCompleted)
                tcs = new TaskCompletionSource<bool>();
            ShowInternal();
            return tcs.Task;
        }
        protected void ShowInternal()
        {
            if (isNotUseAd)
            {
                OnClose(true);
            }
            else
            {
                Show();
            }
        }
        protected void OnClose(bool isEnd)
        {
            if (tcs != null && !tcs.Task.IsCompleted)
                tcs.SetResult(isEnd);
            _onclose?.Invoke(isEnd);
            onClose?.Invoke(isEnd);
        }
        protected abstract void Show();
        public abstract bool isReady();
    }
}
