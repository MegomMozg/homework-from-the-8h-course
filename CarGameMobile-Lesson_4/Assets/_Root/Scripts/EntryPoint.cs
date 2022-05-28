using _Root.Scripts;
using Game;
using Profile;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    
    [Header("Scene Objects")]
    [SerializeField] private Transform _placeForUi;

    [SerializeField] private EntryPointSettings _settings;

    private MainController _mainController;


    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(_settings._speedCar, _settings._transportType, _settings._initialState);
        _mainController = new MainController(_placeForUi, profilePlayer);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}
