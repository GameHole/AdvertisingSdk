using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
namespace MiniGameSDK
{
    public interface IAdAPI: IInterface
    {
        bool isReady();
        void Show();
    }
    public interface IRewardAdAPI: IAdAPI
    {
        bool isNotUseAd { get; set; }
        Action<bool> onClose { set; }
        void AutoShow(Action<bool> onclose);
        Task<bool> AutoShowAsync();
    }
   
}

