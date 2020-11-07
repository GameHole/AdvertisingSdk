using System;
using System.Collections.Generic;
using UnityEngine;
namespace MiniGameSDK
{
	public interface IRewardAdClicked
	{
        Action OnAdClicked { get; set; }
	}
}
