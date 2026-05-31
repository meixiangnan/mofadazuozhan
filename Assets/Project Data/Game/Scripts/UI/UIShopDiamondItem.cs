using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Watermelon
{
    public class UIShopDiamondItem : MonoBehaviour
    {
        [SerializeField] Button buyBtn;
        [SerializeField] int diamondAmount;

        private System.Action onPurchaseSuccess;

        public void Init(System.Action onPurchased)
        {
            onPurchaseSuccess = onPurchased;
            buyBtn.onClick.AddListener(OnBuyClick);
        }

        private void OnBuyClick()
        {
            var diamondModule = GameGlobal.Instance.GetModule<DiamondModule>();
            diamondModule.AddDiamond(diamondAmount);
            FloatingMessage.ShowMessage($"+{diamondAmount} 钻石");
            onPurchaseSuccess?.Invoke();
        }
    }
}
