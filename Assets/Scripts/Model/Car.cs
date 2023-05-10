using Garage;
using Inventory;
using Items;
using System;
using System.Collections.Generic;
using Tools;

namespace Model
{
    public class Car : IUpgradableCar
    {
        private float _defaultSpeed;
        private Dictionary<DetailType, UpgradeItemConfig> _detailList = new Dictionary<DetailType, UpgradeItemConfig>();
        private float _bonusSpeed=0f;
        public SubscriptionProperty<float> Speed { get; }
        
        public Car(float speed)
        {
            Speed = new SubscriptionProperty<float>();
            Speed.Value = speed;

            int enumCount= Enum.GetNames(typeof(DetailType)).Length;
            for(int i=0;i< enumCount; i++ )
            {
                _detailList.Add((DetailType)i,null);
            }
        }

        public void SetDetail(UpgradeItemConfig upgradeItem)
        {
            var key = upgradeItem.DetailType;
            _detailList[key] = upgradeItem;
        }

        public void Restore()
        {
            Speed.Value = _defaultSpeed;
        }
        public void SetSpeedBonus(float speed)
        {
            Speed.Value += speed;
        }
    }
}

