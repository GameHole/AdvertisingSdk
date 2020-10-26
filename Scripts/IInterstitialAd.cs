using System;

namespace MiniGameSDK
{
    public interface IInterstitialAdAPI : IAdAPI
    {
        event Action<bool> onClose;
        void Show();
    }
}
