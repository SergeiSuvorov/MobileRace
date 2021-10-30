using Garage;
using Inventory;

namespace Items
{
    public class SpeedUpgradeCarHandler : IUpgradeCarHandler
    {
        private float _speed;
        private UpgradeItemConfig _detailConfig;
        public SpeedUpgradeCarHandler(float speed, UpgradeItemConfig detailConfig)
        {
            _detailConfig = detailConfig;
            _speed = speed;
        }

        public IUpgradableCar Upgrade(IUpgradableCar upgradableCar)
        {
            upgradableCar.Speed.Value += _speed;
            upgradableCar.SetDetail(_detailConfig);
            return upgradableCar;
        }
    }
}
