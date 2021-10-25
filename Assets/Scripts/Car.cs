using Garage;
using Inventory;
using Items;
using System;
using System.Collections.Generic;

namespace Model
{
    public class Car : IUpgradableCar
    {
        private float _defaultSpeed;
        private Dictionary<DetailType, UpgradeItemConfig> _detailList = new Dictionary<DetailType, UpgradeItemConfig>();
        public float Speed
        {
            get => _defaultSpeed;
            set => _defaultSpeed = value;
        }

        public Car(float speed)
        {
            _defaultSpeed = speed;

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
    }
}

