using Services.Ads.UnityAds;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        

        public void Init(UnityAction startGame, UnityAction Rewerded)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonStart.onClick.AddListener(Rewerded);
        }
        

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            
        }
        
        
    }
}
