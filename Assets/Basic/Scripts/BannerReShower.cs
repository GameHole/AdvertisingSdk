using System;
using System.Collections.Generic;
using UnityEngine;
namespace MiniGameSDK
{
	public class BannerReShower:MonoBehaviour
	{
        IBannerAd banner;
        public float reShowTime = 30;
        float add;
        bool isRun;
        public bool useReShow;
        private void Start()
        {
            banner = Refinter.Reflection.Get<IBannerAd>();
            banner.onClose += (v) =>
            {
                StartCountDown();
            };
            banner.onHide += () =>
            {
                StopCountDown();
            };
        }
        public void StartCountDown()
        {
            //Debug.Log("StartCountDown");
            add = 0;
            isRun = useReShow;
        }
        public void StopCountDown()
        {
            //Debug.Log("StopCountDown");
            isRun = false;
        }
        private void Update()
        {
            if (isRun)
            {
                add += Time.deltaTime;
                if (add >= reShowTime)
                {
                    add = 0;
                    isRun = false;
                    if (useReShow)
                    {
                        //Debug.Log("auot show");
                        banner?.Show();
                    }
                }
            }
        }
    }
}
