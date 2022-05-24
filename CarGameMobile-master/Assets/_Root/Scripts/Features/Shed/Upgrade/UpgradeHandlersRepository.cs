using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.iOS;

namespace Features.Shed.Upgrade
{
    internal interface IUpgradeHandlersRepository : IRepository
    {
        IReadOnlyDictionary<string, IUpgradeHandler> Items { get; }
    }

    internal class UpgradeHandlersRepository
        : Repository<string, IUpgradeHandler, UpgradeItemConfig>, IUpgradeHandlersRepository
    {
        public UpgradeHandlersRepository(IEnumerable<UpgradeItemConfig> configs) : base(configs)
        { }

        protected override string GetKey(UpgradeItemConfig config) =>
            config.Id;

        protected override IUpgradeHandler CreateItem(UpgradeItemConfig config)
        {
            return config.Type switch
            {
                UpgradeType.Speed => new SpeedUpgradeHandler(config.Value),
                UpgradeType.Transmission => new TransmissionUpgradeHandler(config.Value, config.Value),
                _ => StubUpgradeHandler.Default,
                
            };
        }
    }
}
