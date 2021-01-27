using System;

namespace MiniGameSDK
{
    public interface IInterstitialAdAPI : IAdAPI,IInterface
    {
        event Action<bool> onClose;
        void Show();
    }
}
