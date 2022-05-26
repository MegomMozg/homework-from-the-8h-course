using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private string _productId;

        [Header("Buttons")]
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonShed;
        [SerializeField] private Button _buttonAdsReward;
        [SerializeField] private Button _buttonBuyProduct;


        public void Init(UnityAction startGame, UnityAction settings, UnityAction shed,
            UnityAction adsReward, UnityAction<string> buyProduct)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(settings);
            _buttonShed.onClick.AddListener(shed);
            _buttonAdsReward.onClick.AddListener(adsReward);
            _buttonBuyProduct.onClick.AddListener(() => buyProduct(_productId));
        }

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonShed.onClick.RemoveAllListeners();
            _buttonAdsReward.onClick.RemoveAllListeners();
            _buttonBuyProduct.onClick.RemoveAllListeners();
        }
    }
}
