using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField]
        private Button _buttonStart;
        [SerializeField] private Button _showRewardedButton;
        [SerializeField] private Text _creditText;

        [SerializeField] private Button _creditPackButton;
        [SerializeField] private Button _bigCreditPackButton;
        [SerializeField] private Button _garageButton;

        private UnityAction<string> _purchaseReqested; 
        public void Init(UnityAction startGame, UnityAction rewardAdRequested,UnityAction<string> purchaseReqested, UnityAction openGarage)
        {
            _buttonStart.onClick.AddListener(startGame);
            _showRewardedButton?.onClick.AddListener(rewardAdRequested);
            
            _purchaseReqested = purchaseReqested;
            _garageButton?.onClick.AddListener(openGarage);
            _creditPackButton?.onClick.AddListener(onCreditPackButtonClick);
            _bigCreditPackButton?.onClick.AddListener(onBigCreditPackButtonClick);
        }

        private void onCreditPackButtonClick()
        {
            PurchaseBuy("creditPack");
        }
        private void onBigCreditPackButtonClick()
        {
            PurchaseBuy("bigCreditPack");
        }
        private void PurchaseBuy(string productID)
        {
            _purchaseReqested?.Invoke(productID); 
        }

        public void UpdateCredit(int creditValue)
        {
            if(_creditText!=null)
            {
                _creditText.text = creditValue.ToString();
            }
        }
        protected void OnDestroy()
        {
            _buttonStart.onClick?.RemoveAllListeners();
            _showRewardedButton?.onClick.RemoveAllListeners();
            _garageButton?.onClick.RemoveAllListeners();
            _creditPackButton?.onClick.RemoveAllListeners();
            _bigCreditPackButton?.onClick.RemoveAllListeners();
        }
    }
}

