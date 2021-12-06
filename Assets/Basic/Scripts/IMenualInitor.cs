using Refinter;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace MiniGameSDK
{
	public interface IMenualInitor
	{
        void Init();
	}
    public interface IMenuMgr:IInterface
    {
        void Init();
    }
    class MenuMgr : IMenuMgr
    {
        public void Init()
        {
            foreach (var item in Reflection.Interfaces.Values)
            {
                if(item is IMenualInitor menual)
                {
                    menual.Init();
                }
            } 
        }
    }
}
