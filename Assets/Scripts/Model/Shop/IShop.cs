using System;
using Tools;

namespace Model.Shop
{
    public interface IShop
    {
        void Buy(string id);
        string GetCost(string productID);
        void RestorePurchase();

        event EventHandler<PurchaseEventData> OnSuccessPurchase;
        //IReadOnlySubscriptionProperty<string> OnSuccessPurchase { get; }
        IReadOnlySubscriptionAction OnFailedPurchase { get; }
    }

    public class PurchaseEventData
    {
        public PurchaseEventData(string productID)
        {
            ProductID = productID;
        }

        public string ProductID { get; }
     
    }

}