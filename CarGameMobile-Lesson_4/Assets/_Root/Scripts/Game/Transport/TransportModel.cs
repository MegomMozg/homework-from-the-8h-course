using Features.Shed.Upgrade;

namespace Game.Transport
{
    internal class TransportModel : IUpgradable
    {
        private readonly float _defaultSpeed;

        public readonly TransportType Type;

        public float Speed { get; set; }


        public TransportModel(float speed, TransportType type)
        {
            _defaultSpeed = speed;
            Speed = speed;
            Type = type;
        }

        public void Restore() =>
            Speed = _defaultSpeed;
    }
}
