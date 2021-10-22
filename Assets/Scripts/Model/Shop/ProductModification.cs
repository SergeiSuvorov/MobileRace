using System;

namespace Model.Shop
{
    [Serializable]
    public class ProductModification
    {
        public ModificationType Type;
        public int Value;
    }
}