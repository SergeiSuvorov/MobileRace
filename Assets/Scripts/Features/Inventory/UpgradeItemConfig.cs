using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "Upgrade item", menuName = "Upgrade item")]
    public class UpgradeItemConfig : ScriptableObject, IUnique<int>
    {
        [SerializeField]
        private ItemConfig _itemConfig;
    
        [SerializeField]
        private UpgradeType _upgradeType;

        [SerializeField]
        private float _valueUpgrade;

        [SerializeField]
        private DetailType _detailType;

        public int Id => _itemConfig.Id;

        public ItemConfig ItemConfig => _itemConfig;

        public UpgradeType UpgradeType => _upgradeType;
    
        public float ValueUpgrade => _valueUpgrade;

        public DetailType DetailType => _detailType;
    }

    public enum DetailType
    {
        None,
        Tire,
        Engine,
        Body
    }

    public enum UpgradeType
    {
        None,
        Speed,
        Control
    }
}