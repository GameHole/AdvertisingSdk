using System;
using System.Collections.Generic;
using UnityEngine;
namespace MiniGameSDK
{
	public interface IBannerAd:IInterface
	{
        void Hide();
        void Show();
        Action<int> onClose { get; set; }
        Action onHide { get; set; }
        event Action onShow;

    }
}
