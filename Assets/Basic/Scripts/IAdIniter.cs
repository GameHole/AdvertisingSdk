using System;
using System.Collections.Generic;
using UnityEngine;
namespace MiniGameSDK
{
	public interface IAdIniter:IInterface
	{
       event Action<int> onInited;
	}
}
