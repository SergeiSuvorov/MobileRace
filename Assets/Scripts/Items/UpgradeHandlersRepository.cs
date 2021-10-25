using System;
using System.Collections.Generic;
using Garage;
using Inventory;

namespace Items
{
    public class UpgradeHandlersRepository : BaseController
    {
        public IReadOnlyDictionary<int, IUpgradeCarHandler> UpgradeItems => _upgradeItemsMapById;
        private Dictionary<int, IUpgradeCarHandler> _upgradeItemsMapById = new Dictionary<int, IUpgradeCarHandler>();

        public UpgradeHandlersRepository(
            List<UpgradeItemConfig> upgradeItemConfigs)
        {
            PopulateItems(ref _upgradeItemsMapById, upgradeItemConfigs);
        }
  
        protected override void OnDispose()
        {
            _upgradeItemsMapById.Clear();
            _upgradeItemsMapById = null;
        }

        private void PopulateItems(
            ref Dictionary<int, IUpgradeCarHandler> upgradeHandlersMapByType,
            List<UpgradeItemConfig> configs)
        {
            foreach (var config in configs)
            {
                if (upgradeHandlersMapByType.ContainsKey(config.Id)) 
                    continue;
                
                upgradeHandlersMapByType.Add(config.Id, CreateHandlerByType(config));
            }
        }

        private IUpgradeCarHandler CreateHandlerByType(UpgradeItemConfig config)
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

