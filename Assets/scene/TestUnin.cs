using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using System;
using System.Threading.Tasks;

namespace Default
{
	public class TestUnin:MonoBehaviour
	{
        class TestBannerA : IBannerAd,IReloader
        {
            public Action<int> onClose { get; set; }
            public Action onHide { get; set; }

            public int RetryCount => 2;

            public int IdCount => 2;

            public Action<bool> onReloaded { get; set; }

            public event Action onShow;

            public void Hide()
            {
                
            }

            public void Reload(int id)
            {
                Debug.Log($"TestBannerA Load {id}");
                onReloaded?.Invoke(id==0);
            }

            public void Show()
            {
                
            }
        }
        class TestBannerB : IBannerAd, IReloader
        {
            public Action<int> onClose { get; set; }
            public Action onHide { get; set; }

            public int RetryCount => 2;

            public int IdCount => 2;

            public Action<bool> onReloaded { get; set; }

            public event Action onShow;

            public void Hide()
            {

            }

            public async void Reload(int id)
            {
                Debug.Log($"TestBannerB Load {id}");
                onReloaded?.Invoke(id == 0);
                if (id == 0)
                {
                    await Task.Delay(1000);
                    Debug.Log("restart ---");
                    onReloaded?.Invoke(false);
                }
            }

            public void Show()
            {

            }
        }
        private void Start()
        {
            PriorityUnin<IBannerAd> unin = new PriorityUnin<IBannerAd>();
            unin.Add(new TestBannerA());
            unin.Add(new TestBannerB());
            unin.Initialize();
        }
    }
}
