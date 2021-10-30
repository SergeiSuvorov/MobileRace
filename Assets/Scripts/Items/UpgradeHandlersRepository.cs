using System;
using System.Collections.Generic;
using Garage;
using Inventory;

namespace Items
{
    public class UpgradeHandlersRepository : BaseRepositoty<int, IUpgradeCarHandler, UpgradeItemConfig>
    {
        public UpgradeHandlersRepository(List<UpgradeItemConfig> configs) : base(configs)
        {
        }

        protected override IUpgradeCarHandler CreateValue(UpgradeItemConfig config)
        {
            switch (config.UpgradeType)
            {
                case UpgradeType.Speed:
                    return new SpeedUpgradeCarHandler(config.ValueUpgrade, config);
                default:
                    return StubUpgradeCarHandler.Default;
            }
        }

    }
}

