using System;

namespace MiniGameSDK
{
    public interface IDialogPageAd : IAdAPI,IInterface
    {
        event Action<bool> onClose;
        void Show();
    }
}
