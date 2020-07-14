using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
using PureGym.Interfaces.ShoppingSystem;
using PureGym.Models.Containers;
using PureGym.Models.Entities;
using System;

namespace PureGym.Basket
{
    public class WarehouseKeys
    {
        public const string HAT001 = "HAT001";
        public const string COAT01 = "COAT01";
        public const string SHOE11 = "SHOE11";
        public const string VOU30 = "VOU30";
    }

    public class TestWarehouse : IIsAWarehouse
    {
        protected InventoryContainer<GenericInventoryItem> Inventory;
        protected Currency WarehouseCurrency;

        public void Initalise(Currency currency)
        {
            if (Helper.CheckIfValueIsNotNull(Inventory)) { return; }

            WarehouseCurrency = currency;

            Inventory = new InventoryContainer<GenericInventoryItem>(WarehouseCurrency);

            Inventory.Insert(new GenericInventoryItem(WarehouseKeys.HAT001, "A Nice Hat", new Money(10.50m, WarehouseCurrency), StockCategory.HeadGear));
            Inventory.Insert(new GenericInventoryItem(WarehouseKeys.COAT01, "A Decent Coat", new Money(85.00m, WarehouseCurrency), StockCategory.Formalwear));
            Inventory.Insert(new GenericInventoryItem(WarehouseKeys.SHOE11, "Cheap Shoes", new Money(12.99m, WarehouseCurrency), StockCategory.Footwear));
            Inventory.Insert(new GenericInventoryItem(WarehouseKeys.VOU30, "£30 Gift Voucher", new Money(30.00m, WarehouseCurrency), StockCategory.Vouchers));
        }

        public IIsABasketItem GetItem(string key)
        {
            return GenericBasketItem.CreateFrom<GenericInventoryItem>(Inventory.Find(key), 1);
        }

        protected void Insert(string key, string description, Money price, StockCategory category, Guid id = default(Guid))
        {
            Inventory.Insert(new GenericInventoryItem(key, description, price, category, id));
        }
    }
}
