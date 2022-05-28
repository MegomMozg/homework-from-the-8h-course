using Game;
using Profile;
using UnityEngine;

namespace _Root.Scripts
{
    [CreateAssetMenu(fileName = nameof(EntryPointSettings), menuName = "Configs/"+ nameof(EntryPointSettings))]
    internal class EntryPointSettings : ScriptableObject
    {
        [Header("Initial Settings")]
        [SerializeField]
        public float _speedCar;
        [SerializeField] public GameState _initialState;
        [SerializeField] public TransportType _transportType;

    }
}