using Profile;
using UnityEngine;
using Services.IAP;
using Services.Analytics;
using Services.Ads.UnityAds;
using UnityEngine.Serialization;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const GameState InitialState = GameState.Start;

    [SerializeField] private Transform _placeForUi;
    [SerializeField] private IAPService _iapService;
    [SerializeField] private UnityAdsService _adsService;
    [SerializeField] private AnalyticsManager _analytics;
    [SerializeField] private Object _Bombs;

    private MainController _mainController;


    private void Awake()
    {
        
        var profilePlayer = new ProfilePlayer(SpeedCar, InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer, _adsService, _iapService);

        // if (_adsService.IsInitialized) OnAdsInitialized();
        // else _adsService.Initialized.AddListener(OnAdsInitialized);
        //
        // if (_iapService.IsInitialized) OnIapInitialized();
        // else _iapService.Initialized.AddListener(OnIapInitialized);

        _Bombs = Resources.Load("Prefabs/Bombs");
        Object objectView = Object.Instantiate(_Bombs);
        _analytics.SendGameStarted();
        
    }

    private void OnDestroy()
    {
        //_iapService.Initialized.RemoveListener(OnIapInitialized);
        _mainController.Dispose();
    }


    // private void OnAdsInitialized() => _adsService.InterstitialPlayer.Play();
    // private void OnIapInitialized() => _iapService.Buy("product_1");
}
