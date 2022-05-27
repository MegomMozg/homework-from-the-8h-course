<<<<<<< Updated upstream
using Game;
using Profile;
=======
using _Root.Scripts;
using Game;
using Profile;
using Tool;
using Unity.VisualScripting;
>>>>>>> Stashed changes
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
<<<<<<< Updated upstream
    [Header("Initial Settings")]
    [SerializeField] private float _speedCar;
    [SerializeField] private GameState _initialState;
    [SerializeField] private TransportType _transportType;

    [Header("Scene Objects")]
    [SerializeField] private Transform _placeForUi;

    private MainController _mainController;


    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(_speedCar, _transportType, _initialState);
=======
    [Header("Scene Objects")]
    [SerializeField] private Transform _placeForUi;

    private EntryPointSettings _settings;
    private MainController _mainController;
    private void Start()
    {
        _settings = Resources.Load<EntryPointSettings>("Configs/EntryPointSettings");
        var profilePlayer = new ProfilePlayer(_settings._speedCar, _settings._transportType, _settings._initialState);
>>>>>>> Stashed changes
        _mainController = new MainController(_placeForUi, profilePlayer);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}
