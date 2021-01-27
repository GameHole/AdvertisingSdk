using System;
using System.Collections.Generic;
using UnityEngine;
namespace MiniGameSDK
{
	public interface ISplashAd:IInterface
	{
        void Show();
        Action OnClsoe { get; set; }
	}
}
