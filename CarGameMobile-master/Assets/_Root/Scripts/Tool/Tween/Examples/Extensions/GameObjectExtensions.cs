using Tool.Tween.Examples.Lerp;
using UnityEngine;

namespace Tool.Tween.Examples.Extensions
{
    internal static class GameObjectExtensions
    {
        public static void Move(this GameObject go, Vector3 startPosition, Vector3 endPosition, float duration)
        {
            if (go.TryGetComponent<VectorLerpComponent>(out var component))
                component.Play(startPosition, endPosition, duration);
        }
    }
}
