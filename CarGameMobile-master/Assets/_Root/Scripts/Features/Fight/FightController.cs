using System;
using Profile;
using TMPro;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.Fight
{
    internal class FightController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Fight/FightView");
        private readonly ProfilePlayer _profilePlayer;
        private readonly FightView _view;
        private readonly Enemy _enemy;

        private PlayerData _money;
        private PlayerData _heath;
        private PlayerData _power;
        private PlayerData _crime;

        private int _allCountMoneyPlayer;
        private int _allCountHealthPlayer;
        private int _allCountPowerPlayer;
        private int _allCountCrimePlayer;


        public FightController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);

            _enemy = new Enemy("Enemy Flappy");

            _money = CreatePlayerData(DataType.Money);
            _heath = CreatePlayerData(DataType.Health);
            _power = CreatePlayerData(DataType.Power);
            _crime = CreatePlayerData(DataType.Crime);

            Subscribe(_view);
        }

        protected override void OnDispose()
        {
            DisposePlayerData(ref _money);
            DisposePlayerData(ref _heath);
            DisposePlayerData(ref _power);
            DisposePlayerData(ref _crime);

            Unsubscribe(_view);
        }


        private FightView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<FightView>();
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


        private void Subscribe(FightView view)
        {
            view.AddMoneyButton.onClick.AddListener(IncreaseMoney);
            view.MinusMoneyButton.onClick.AddListener(DecreaseMoney);

            view.AddHealthButton.onClick.AddListener(IncreaseHealth);
            view.MinusHealthButton.onClick.AddListener(DecreaseHealth);

            view.AddPowerButton.onClick.AddListener(IncreasePower);
            view.MinusPowerButton.onClick.AddListener(DecreasePower);

            view.AddCrimeButton.onClick.AddListener(IncreaseCrime);
            view.MinusCrimeButton.onClick.AddListener(DecreaseCrime);

            view.FightButton.onClick.AddListener(Fight);
            view.EscapeButton.onClick.AddListener(Escape);
        }

        private void Unsubscribe(FightView view)
        {
            view.AddMoneyButton.onClick.RemoveAllListeners();
            view.MinusMoneyButton.onClick.RemoveAllListeners();

            view.AddHealthButton.onClick.RemoveAllListeners();
            view.MinusHealthButton.onClick.RemoveAllListeners();

            view.AddPowerButton.onClick.RemoveAllListeners();
            view.MinusPowerButton.onClick.RemoveAllListeners();

            view.AddCrimeButton.onClick.RemoveAllListeners();
            view.MinusCrimeButton.onClick.RemoveAllListeners();

            view.FightButton.onClick.RemoveAllListeners();
            view.EscapeButton.onClick.RemoveAllListeners();
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
            _view.CountPowerEnemyText.text = $"Enemy Power {enemyPower}";
        }

        private TMP_Text GetTextComponent(DataType dataType) =>
            dataType switch
            {
                DataType.Money => _view.CountMoneyText,
                DataType.Health => _view.CountHealthText,
                DataType.Power => _view.CountPowerText,
                DataType.Crime => _view.CountCrimeText,
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

            _view.EscapeButton.interactable = canUse;
            _view.EscapeButton.gameObject.SetActive(canShow);
        }


        private void Fight()
        {
            int enemyPower = _enemy.CalcPower();
            bool isVictory = _allCountPowerPlayer >= enemyPower;

            string color = isVictory ? "#07FF00" : "#FF0000";
            string message = isVictory ? "Win" : "Lose";

            Debug.Log($"<color={color}>{message}!!!</color>");

            Close();
        }

        private void Escape()
        {
            string color = "#FFB202";
            string message = "Escaped";

            Debug.Log($"<color={color}>{message}!!!</color>");

            Close();
        }

        private void Close() => _profilePlayer.CurrentState.Value = GameState.Game;
    }
}
