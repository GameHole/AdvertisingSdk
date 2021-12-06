using Refinter;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace MiniGameSDK
{
    class InterfaceCntr<T>
    {
        public List<T> instances = new List<T>();
        int idx;
        public void Init(Type exp)
        {
            int thisIndex = ReflectEx.GetIndex(exp);
            foreach (var item in ReflectEx.FindImpl(typeof(T)))
            {
                //if (item == exp)
                //    continue;
                int othIdx = ReflectEx.GetIndex(item);
                if (othIdx == -1000 || thisIndex <= othIdx) continue;
                var inst = instance(item);
                //Debug.Log($"type::{typeof(T)} inst {inst}" );
                if (inst != null)
                {
                    foreach (var inter in Reflection.Interfaces.Values)
                    {
                        ReflectEx.Inject(inst, inter);
                    }
                    //(inst as IInitializable)?.Initialize();
                    instances.Add(inst);
                }
            }
            //if (instances.Count > 1)
            //{
            //    for (int i = 0; i < instances.Count; i++)
            //    {
            //        if (ReflectEx.GetIndex(instances[i].GetType()) == -1000)
            //        {
            //            instances.Remove(instances[i]);
            //            break;
            //        }
            //    }
            //}
            foreach (var item in instances)
            {
                Debug.Log($"InterfaceCntr.log::interface{typeof(T)},inst::{item}");
            }
        }
        public T GetNext()
        {
            if (instances.Count == 0)
                return default;
            return instances[idx++ % instances.Count];
        }
        T instance(Type type)
        {
            if (type.IsSubclassOf(typeof(MonoBehaviour)))
            {
                return (T)ReflectEx.FindIMonoImpl(type);
            }
            else
            {
                return (T)Activator.CreateInstance(type);
            }
        }
    }
}
