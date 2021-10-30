using Inventory;
using Tools;

namespace Garage
{
    public interface IUpgradableCar
    {
        SubscriptionProperty<float> Speed { get; }
        void SetDetail(UpgradeItemConfig upgradeItem);
        void SetSpeedBonus(float speed);
        void Restore();
    }
}
