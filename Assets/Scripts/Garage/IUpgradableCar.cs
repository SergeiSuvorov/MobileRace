using Inventory;

namespace Garage
{
    public interface IUpgradableCar
    {
        float Speed { get; set; }
        void SetDetail(UpgradeItemConfig upgradeItem);
    }
}
