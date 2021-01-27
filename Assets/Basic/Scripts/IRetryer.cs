using System;
using System.Collections.Generic;
using UnityEngine;
namespace MiniGameSDK
{
	public interface IRetryer:IInterface
	{
        void Regist(IReloader action);
        void Load(IReloader action);
	}
}
