using Profile;
using Services.Ads.UnityAds;
using Services.IAP;
using Tool;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;
        private UnityAdsService _adsService;
        private IAPService _iapService;


        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, UnityAdsService _service, IAPService iapService)
        {
            _adsService = _service;
            _iapService = iapService;
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, RewerdedPlay, BuyPlay);
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void RewerdedPlay()
        {
            if (_adsService.IsInitialized) OnAdsRewarded();
            else _adsService.Initialized.AddListener(OnAdsRewarded);
        }

        private void BuyPlay()
        {
            if (_iapService.IsInitialized) OnIapInitialized();
            else _iapService.Initialized.AddListener(OnIapInitialized);
        }
        private void OnAdsRewarded() => _adsService.RewardedPlayer.Play();
        private void OnIapInitialized() => _iapService.Buy("product_1");
        private void StartGame() =>
            _profilePlayer.CurrentState.Value = GameState.Game;

        public void OnDestroy()
        {
            _adsService.Initialized.RemoveListener(OnAdsRewarded);
            _iapService.Initialized.RemoveListener(OnIapInitialized);
        }
    }
}
