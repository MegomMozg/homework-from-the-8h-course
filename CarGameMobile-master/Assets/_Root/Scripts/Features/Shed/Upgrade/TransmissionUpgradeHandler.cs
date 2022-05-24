namespace Features.Shed.Upgrade
{
    internal class TransmissionUpgradeHandler : IUpgradeHandler
    {
        private readonly float _transmisson;
        private readonly float _JumpHeight;

        public TransmissionUpgradeHandler(float Transmisson, float jumpHeight)
        {
            _transmisson = Transmisson;
            _JumpHeight = jumpHeight;

        }

        public void Upgrade(IUpgradable upgradable)
        {
            upgradable.Transmisson += _transmisson;
            upgradable.JumpHeight += _JumpHeight;
        }
    }
}