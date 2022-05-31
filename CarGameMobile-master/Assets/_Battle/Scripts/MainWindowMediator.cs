using System;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScripts
{
    internal class MainWindowMediator : MonoBehaviour
    {
        [Header("Player Stats")]
        [SerializeField] private TMP_Text _countMoneyText;
        [SerializeField] private TMP_Text _countHealthText;
        [SerializeField] private TMP_Text _countPowerText;

        [Header("Enemy Stats")]
        [SerializeField] private TMP_Text _countPowerEnemyText;
        [SerializeField] private TMP_Text _countCrimeEnemyText;
        

        [Header("Money Buttons")]
        [SerializeField] private Button _addMoneyButton;
        [SerializeField] private Button _minusMoneyButton;

        [Header("Health Buttons")]
        [SerializeField] private Button _addHealthButton;
        [SerializeField] private Button _minusHealthButton;

        [Header("Power Buttons")]
        [SerializeField] private Button _addPowerButton;
        [SerializeField] private Button _minusPowerButton;
        
        [Header("Crimes Buttons")]
        [SerializeField] private Button _addCrimeRate;
        [SerializeField] private Button _minusCrimeRate;
        [SerializeField] private Button _passPeacefully;

        [Header("Other Buttons")]
        [SerializeField] private Button _fightButton;
        
        private int _allCountCrimeEnenmy;
        private int _allCountMoneyPlayer;
        private int _allCountHealthPlayer;
        private int _allCountPowerPlayer;

        private PlayerData _money;
        private PlayerData _heath;
        private PlayerData _power;
        private PlayerData _crime;

        private Enemy _enemy;


        private void Start()
        {
            _enemy = new Enemy("Enemy Flappy");

            _money = CreatePlayerData(DataType.Money);
            _heath = CreatePlayerData(DataType.Health);
            _power = CreatePlayerData(DataType.Power);
            _crime = CreatePlayerData(DataType.Crime);

            Subscribe();
        }

        private void OnDestroy()
        {
            DisposePlayerData(ref _money);
            DisposePlayerData(ref _heath);
            DisposePlayerData(ref _power);

            Unsubscribe();
        }


        private PlayerData CreatePlayerData(DataType dataType)
        {
            PlayerData playerData = new PlayerData(dataType);
            playerData.Attach(_enemy);

            return playerData;
        }

        private void DisposePlayerData(ref PlayerData playerData)
        {
            playerData.Detach(_enemy);
            playerData = null;
        }


        private void Subscribe()
        {
            _addMoneyButton.onClick.AddListener(IncreaseMoney);
            _minusMoneyButton.onClick.AddListener(DecreaseMoney);

            _addHealthButton.onClick.AddListener(IncreaseHealth);
            _minusHealthButton.onClick.AddListener(DecreaseHealth);

            _addPowerButton.onClick.AddListener(IncreasePower);
            _minusPowerButton.onClick.AddListener(DecreasePower);
            
            _addCrimeRate.onClick.AddListener(IncreaseCrime);
            _minusCrimeRate.onClick.AddListener(DecreaseCrime);
            _passPeacefully.onClick.AddListener(OnClickCrime);

            _fightButton.onClick.AddListener(Fight);
        }

        private void Unsubscribe()
        {
            _addMoneyButton.onClick.RemoveAllListeners();
            _minusMoneyButton.onClick.RemoveAllListeners();

            _addHealthButton.onClick.RemoveAllListeners();
            _minusHealthButton.onClick.RemoveAllListeners();

            _addPowerButton.onClick.RemoveAllListeners();
            _minusPowerButton.onClick.RemoveAllListeners();
            
            _addCrimeRate.onClick.RemoveAllListeners();
            _addCrimeRate.onClick.RemoveAllListeners();

            _fightButton.onClick.RemoveAllListeners();
        }


        private void IncreaseMoney() => IncreaseValue(ref _allCountMoneyPlayer, DataType.Money);
        private void DecreaseMoney() => DecreaseValue(ref _allCountMoneyPlayer, DataType.Money);

        private void IncreaseHealth() => IncreaseValue(ref _allCountHealthPlayer, DataType.Health);
        private void DecreaseHealth() => DecreaseValue(ref _allCountHealthPlayer, DataType.Health);

        private void IncreasePower() => IncreaseValue(ref _allCountPowerPlayer, DataType.Power);
        private void DecreasePower() => DecreaseValue(ref _allCountPowerPlayer, DataType.Power);
        
        private void IncreaseCrime() => IncreaseValue(ref _allCountCrimeEnenmy, DataType.Crime);
        private void DecreaseCrime() => DecreaseValue(ref _allCountCrimeEnenmy, DataType.Crime);
        private void IncreaseValue(ref int value, DataType dataType) => AddToValue(ref value, 1, dataType);
        private void DecreaseValue(ref int value, DataType dataType) => AddToValue(ref value, -1, dataType);

        private void AddToValue(ref int value, int addition, DataType dataType)
        {
            value += addition;
            ChangeDataWindow(value, dataType);
        }


        private void ChangeDataWindow(int countChangeData, DataType dataType)
        {
            PlayerData playerData = GetPlayerData(dataType);
            TMP_Text textComponent = GetTextComponent(dataType);
            string text = $"Player {dataType:F} {countChangeData}";

            playerData.Value = countChangeData;
            textComponent.text = text;

            int enemyPower = _enemy.CalcPower();
            int enemyCrime = _enemy.CalcCrime(_passPeacefully, _addCrimeRate, _minusCrimeRate);
            _countPowerEnemyText.text = $"Enemy Power {enemyPower}";
            _countCrimeEnemyText.text = $"Enemy Crime {enemyCrime}";
        }

        private TMP_Text GetTextComponent(DataType dataType) =>
            dataType switch
            {
                DataType.Money => _countMoneyText,
                DataType.Health => _countHealthText,
                DataType.Power => _countPowerText,
                DataType.Crime => _countCrimeEnemyText,
                _ => throw new ArgumentException($"Wrong {nameof(DataType)}")
            };

        private PlayerData GetPlayerData(DataType dataType) =>
            dataType switch
            {
                DataType.Money => _money,
                DataType.Health => _heath,
                DataType.Power => _power,
                DataType.Crime => _crime,
                _ => throw new ArgumentException($"Wrong {nameof(DataType)}")
            };


        private void Fight()
        {
            int enemyPower = _enemy.CalcPower();
            bool isVictory = _allCountPowerPlayer >= enemyPower;

            string color = isVictory ? "#07FF00" : "#FF0000";
            string message = isVictory ? "Win" : "Lose";

            Debug.Log($"<color={color}>{message}!!!</color>");
        }

        

        private void OnClickCrime() => Debug.Log("<color=#07FF00>Pass!!!</color>");
    }
}
