using Features.Shed.Upgrade;

namespace Game.Transport
{
    internal class TransportModel : IUpgradable
    {
        private readonly float _defaultSpeed;
        private readonly float _defualtTransmission;
        private readonly  float _defaultJumpheight;

        public readonly TransportType Type;
        

        public float Speed { get; set; }
        public float Transmisson { get; set; }
        public float JumpHeight { get; set; }


        public TransportModel(float speed, float transmisson, float jumpHeight , TransportType type)
        {
            _defaultJumpheight = jumpHeight;
            _defualtTransmission = transmisson;
            _defaultSpeed = speed;
            Speed = speed;
            Type = type;
        }

        public void Restore()
        {
            Transmisson = _defualtTransmission;
            JumpHeight = _defaultJumpheight;
            Speed = _defaultSpeed;
        }
    }
}
