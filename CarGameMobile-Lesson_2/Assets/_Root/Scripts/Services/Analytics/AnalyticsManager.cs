using UnityEngine;
using Services.Analytics.UnityAnalytics;
using Unity.VisualScripting;
using UnityEngine.UIElements;

namespace Services.Analytics
{
    internal class AnalyticsManager : MonoBehaviour
    {
        private IAnalyticsService[] _services;
         
        private void Awake() =>
            _services = new IAnalyticsService[]
            {
                new UnityAnalyticsService() 
            };

        public void SendGameStarted() =>
            SendEvent("GameStarted");

        private void SendEvent(string eventName)
        {
            for (int i = 0; i < _services.Length; i++)
                _services[i].SendEvent(eventName);
        }
    }
}