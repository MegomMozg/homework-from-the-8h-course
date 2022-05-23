using Services.Ads.UnityAds;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _ButtonIAP;
        

        public void Init(UnityAction startGame, UnityAction Rewerded, UnityAction IAPButton)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonStart.onClick.AddListener(Rewerded);
            
            _ButtonIAP.onClick.AddListener(IAPButton);
        }
        

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _ButtonIAP.onClick.RemoveAllListeners();
            
        }
        
        
    }
}
