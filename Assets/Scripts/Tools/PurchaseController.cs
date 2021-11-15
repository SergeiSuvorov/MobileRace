using Model;
using Model.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Purchasing;

public class PurchaseController : BaseController
{
    private readonly List<ShopProduct> _products;
    private readonly ProfilePlayer _player;
    private readonly IShop _shop;

    public PurchaseController(List<ShopProduct> products, ProfilePlayer player, IShop shop)
    {
        _products = products;
        _player = player;
        _shop = shop;
        _shop.OnSuccessPurchase += ApplyProductModification;
    }

    private void ApplyProductModification(object sender, PurchaseEventData eventData)
    {
        string productID = eventData.ProductID;
        var product = _products.FirstOrDefault(p => p.Id == productID);
        if (product == null)
            return;

        var modification = product.Modification;

        switch (modification.Type)
        {
            case ModificationType.None:
                break;
            case ModificationType.Credit:
                _player.CreditCount.Value += modification.Value;
                break;
            case ModificationType.Item:
                break;
            default:
                break;
        }
    }
    //private void ApplyProductModification(string productID)
    //{
    //    var product = _products.FirstOrDefault(p => p.Id == productID);
    //    if (product == null)
    //        return;

    //    var modification = product.Modification;

    //    switch (modification.Type)
    //    {
    //        case ModificationType.None:
    //            break;
    //        case ModificationType.Credit:
    //            _player.CreditCount.Value += modification.Value;
    //            break;
    //        case ModificationType.Item:
    //            break;
    //        default:
    //            break;
    //    }

    //}

    protected override void OnDispose()
    {

        _shop.OnSuccessPurchase -= ApplyProductModification;
        base.OnDispose();
    }
}