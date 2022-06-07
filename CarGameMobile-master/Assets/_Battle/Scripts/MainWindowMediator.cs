using System;
using TMPro;
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
        [SerializeField] private TMP_Text _countCrimeText;

        [Header("Enemy Stats")]
        [SerializeField] private TMP_Text _countPowerEnemyText;

        [Header("Money Buttons")]
        [SerializeField] private Button _addMoneyButton;
        [SerializeField] private Button _minusMoneyButton;

        [Header("Health Buttons")]
        [SerializeField] private Button _addHealthButton;
        [SerializeField] private Button _minusHealthButton;

        [Header("Power Buttons")]
        [SerializeField] private Button _addPowerButton;
        [SerializeField] private Button _minusPowerButton;

        [Header("Crime Buttons")]
        [SerializeField] private Button _addCrimeButton;
        [SerializeField] private Button _minusCrimeButton;

        [Header("Other Buttons")]
        [SerializeField] private Button _fightButton;
        [SerializeField] private Button _escapeButton;

        private int _allCountMoneyPlayer;
        private int _allCountHealthPlayer;
        private int _allCountPowerPlayer;
        private int _allCountCrimePlayer;

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
            DisposePlayerData(ref _crime);

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

            _addCrimeButton.onClick.AddListener(IncreaseCrime);
            _minusCrimeButton.onClick.AddListener(DecreaseCrime);

            _fightButton.onClick.AddListener(Fight);
            _escapeButton.onClick.AddListener(Escape);
        }

        private void Unsubscribe()
        {
            _addMoneyButton.onClick.RemoveAllListeners();
            _minusMoneyButton.onClick.RemoveAllListeners();

            _addHealthButton.onClick.RemoveAllListeners();
            _minusHealthButton.onClick.RemoveAllListeners();

            _addPowerButton.onClick.RemoveAllListeners();
            _minusPowerButton.onClick.RemoveAllListeners();

            _addCrimeButton.onClick.RemoveAllListeners();
            _minusCrimeButton.onClick.RemoveAllListeners();

            _fightButton.onClick.RemoveAllListeners();
            _escapeButton.onClick.RemoveAllListeners();
        }


        private void IncreaseMoney() => IncreaseValue(ref _allCountMoneyPlayer, DataType.Money);
        private void DecreaseMoney() => DecreaseValue(ref _allCountMoneyPlayer, DataType.Money);

        private void IncreaseHealth() => IncreaseValue(ref _allCountHealthPlayer, DataType.Health);
        private void DecreaseHealth() => DecreaseValue(ref _allCountHealthPlayer, DataType.Health);

        private void IncreasePower() => IncreaseValue(ref _allCountPowerPlayer, DataType.Power);
        private void DecreasePower() => DecreaseValue(ref _allCountPowerPlayer, DataType.Power);

        private void IncreaseCrime() => IncreaseValue(ref _allCountCrimePlayer, DataType.Crime);
        private void DecreaseCrime() => DecreaseValue(ref _allCountCrimePlayer, DataType.Crime);

        private void IncreaseValue(ref int value, DataType dataType) => AddToValue(ref value, 1, dataType);
        private void DecreaseValue(ref int value, DataType dataType) => AddToValue(ref value, -1, dataType);

        private void AddToValue(ref int value, int addition, DataType dataType)
        {
            value += addition;
            UpdateEscapeButtonVisibility();
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
            _countPowerEnemyText.text = $"Enemy Power {enemyPower}";
        }

        private TMP_Text GetTextComponent(DataType dataType) =>
            dataType switch
            {
                DataType.Money => _countMoneyText,
                DataType.Health => _countHealthText,
                DataType.Power => _countPowerText,
                DataType.Crime => _countCrimeText,
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

        private void UpdateEscapeButtonVisibility()
        {
            const int minCrimeToUse = 0;
            const int maxCrimeToUse = 2;
            const int minCrimeToShow = 0;
            const int maxCrimeToShow = 5;

            bool canUse = minCrimeToUse <= _allCountCrimePlayer && _allCountCrimePlayer <= maxCrimeToUse;
            bool canShow = minCrimeToShow <= _allCountCrimePlayer && _allCountCrimePlayer <= maxCrimeToShow;

            _escapeButton.interactable = canUse;
            _escapeButton.gameObject.SetActive(canShow);
        }


        private void Fight()
        {
            int enemyPower = _enemy.CalcPower();
            bool isVictory = _allCountPowerPlayer >= enemyPower;

            string color = isVictory ? "#07FF00" : "#FF0000";
            string message = isVictory ? "Win" : "Lose";

            Debug.Log($"<color={color}>{message}!!!</color>");
        }

        private void Escape()
        {
            string color = "#FFB202";
            string message = "Escaped";

            Debug.Log($"<color={color}>{message}!!!</color>");
        }
    }
}
