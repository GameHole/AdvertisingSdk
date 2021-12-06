using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
namespace UninSDK
{
    [Refinter.Important(100)]
    public class Unin_MenuralInitor : IMenualInitor,IInitializable
    {
        InterfaceCntr<IMenualInitor> cntr = new InterfaceCntr<IMenualInitor>();
        public void Init()
        {
            foreach (var item in cntr.instances)
            {
                item.Init();
            }
        }

        public void Initialize()
        {
            cntr.Init(GetType());
        }
    }
}
