using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScripts
{
    internal interface IEnemy
    {
        void Update(PlayerData playerData);
    }

    internal class Enemy : IEnemy
    {
        private const float KMoney = 5f;
        private const float KPower = 3;
        private const float KCrime = 3;
        private const float MaxHealthPlayer = 20;
        private const float MaxCrimeEnemy = 5;
        

        private readonly string _name;

        private int _allCountCrimeEnemy;
        private int _moneyPlayer;
        private int _healthPlayer;
        private int _powerPlayer;


        public Enemy(string name) =>
            _name = name;


        public void Update(PlayerData playerData)
        {
            switch (playerData.DataType)
            {
                case DataType.Money:
                    _moneyPlayer = playerData.Value;
                    break;

                case DataType.Health:
                    _healthPlayer = playerData.Value;
                    break;

                case DataType.Power:
                    _powerPlayer = playerData.Value;
                    break;
                case DataType.Crime:
                    _allCountCrimeEnemy = playerData.Value;
                    break;
            }

            Debug.Log($"Notified {_name} change to {playerData}");
        }

        public int CalcPower()
        {
            int kHealth = CalcKHealth();
            float moneyRatio = _moneyPlayer / KMoney;
            float powerRatio = _powerPlayer / KPower;
            float crimeRatio = _allCountCrimeEnemy / KCrime;

            return (int)(moneyRatio + kHealth + powerRatio + crimeRatio);
        }

        private int CalcKHealth() =>
            _healthPlayer > MaxHealthPlayer ? 100 : 5;
        
        public int CalcCrime(Button _passPeacefully, Button _addCrimeEnemy, Button _minusCrimeRate)
        {
            if (_allCountCrimeEnemy >= 3)
            {
                if (_allCountCrimeEnemy <= 5)
                {
                    OnButton(_passPeacefully, false);
                    OnButton(_addCrimeEnemy, !(_allCountCrimeEnemy >= MaxCrimeEnemy));
                }
            }else
            {
                OnButton(_passPeacefully, true);
                OnButton(_minusCrimeRate, !(_allCountCrimeEnemy <= 0));
            }
            return _allCountCrimeEnemy;
        }

        private void OnButton(Button button, bool IsActive)
        {
            button.gameObject.SetActive(IsActive);
        }
    }
}

