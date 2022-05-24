using Game;
using Profile;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    [Header("Initial Settings")]
    [SerializeField] private float _speedCar;
    [SerializeField] private float _TransmissionCar;
    [SerializeField] private float _JumpHieght;
    [SerializeField] private GameState _initialState;
    [SerializeField] private TransportType _transportType;

    [Header("Scene Objects")]
    [SerializeField] private Transform _placeForUi;

    private MainController _mainController;


    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(_speedCar, _TransmissionCar, _JumpHieght, _transportType , _initialState);
        _mainController = new MainController(_placeForUi, profilePlayer);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}
