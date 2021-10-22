using System;
using UnityEngine.Purchasing;

namespace Model.Shop
{
    [Serializable]
    public class ShopProduct
    {
        public string Id;
        public ProductType CurrentProductType;
        public ProductModification Modification;
    }
}