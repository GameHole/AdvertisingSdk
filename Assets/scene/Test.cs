using System.Collections.Generic;
using UnityEngine;
namespace MiniGameSDK
{
	public class Test:MonoBehaviour
	{
        IRewardAdAPI reward;
        public void Show()
        {
            Debug.Log(reward);
            reward.AutoShowAsync();
        }
	}
}
