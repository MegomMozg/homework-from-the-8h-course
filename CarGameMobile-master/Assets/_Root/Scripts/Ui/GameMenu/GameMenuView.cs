using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Ui
{
    public class GameMenuView : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _exitGameButton;


        public void Init(UnityAction ExitMenu)
        {
            _exitGameButton.onClick.AddListener(ExitMenu);
        }

        public void OnDestroy()
        {
            _exitGameButton.onClick.RemoveAllListeners();
        }
    }
}
