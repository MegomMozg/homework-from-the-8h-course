using Profile;
using Services.Ads.UnityAds;
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
        [SerializeField] private UnityAdsService _adsService;


        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, UnityAdsService _service)
        {
            _adsService = _service;
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, _RewerdedPlay);
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void _RewerdedPlay()
        {
            if (_adsService.IsInitialized) OnAdsRewarded();
            else _adsService.Initialized.AddListener(OnAdsRewarded);
        }
        private void OnAdsRewarded() => _adsService.RewardedPlayer.Play();
        private void StartGame() =>
            _profilePlayer.CurrentState.Value = GameState.Game;

        public void OnDestroy()
        {
            _adsService.Initialized.RemoveListener(OnAdsRewarded);
        }
    }
}
