using JoostenProductions;
using UnityEngine;

namespace Game.InputLogic
{
    internal class InputKeyboard : BaseInputView
    {
        [SerializeField] private float _inputMultiplier = 1f;
        private void Start() =>
            UpdateManager.SubscribeToUpdate(Move);

        private void OnDestroy() =>
            UpdateManager.UnsubscribeFromUpdate(Move);

        private void Move()
        {
            Vector2 direction = CalcDirection();
            float moveValue = _inputMultiplier * Time.deltaTime * direction.x;

            float abs = Mathf.Abs(moveValue);
            float sign = Mathf.Sign(moveValue);

            if (sign > 0)
                OnRightMove(abs);
            else
                OnLeftMove(abs);
        }

        private Vector2 CalcDirection()
        {
            const float normalizedMagnitude = 0;

            Vector2 direction = Vector2.zero;
            direction.x = Input.GetAxis("Horizontal");

            if (direction.sqrMagnitude > normalizedMagnitude)
                direction.Normalize();

            return direction;
        }
    }
}

