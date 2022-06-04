using UnityEngine;

namespace Rewards
{
    internal class InstallView : MonoBehaviour
    {
        [SerializeField] private DailyRewardView _dailyRewardView;
        [SerializeField] private WeeklyRewardView _weeklyRewardView;

        private DailyRewardController _dailyRewardController;
        private WeeklyRewardController _weeklyRewardController;


        private void Awake()
        {
            _dailyRewardController = new DailyRewardController(_dailyRewardView);
            _weeklyRewardController = new WeeklyRewardController(_weeklyRewardView);
        }

        private void Start()
        {
            _weeklyRewardController.Init();
            _dailyRewardController.Init();
        }

        private void OnDestroy()
        {
            _dailyRewardController.Deinit();
            _weeklyRewardController.Deinit();
        }
    }
}
