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
        public const string HAT002 = "HAT002";
        public const string JUMP01 = "JUMP01";
        public const string JUMP02 = "JUMP02";
        public const string LIGHT1 = "LIGHT1";
        public const string VOUR30 = "VOUR30";
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

            Inventory.Insert(new GenericInventoryItem(WarehouseKeys.HAT001, "Cheap Hat", new Money(10.50m, WarehouseCurrency), StockCategory.Casualwear));
            Inventory.Insert(new GenericInventoryItem(WarehouseKeys.HAT002, "Nice Hat", new Money(25.00m, WarehouseCurrency), StockCategory.Formalwear));
            Inventory.Insert(new GenericInventoryItem(WarehouseKeys.JUMP01, "Cheap Jumper", new Money(26.00m, WarehouseCurrency), StockCategory.Casualwear));
            Inventory.Insert(new GenericInventoryItem(WarehouseKeys.JUMP02, "Fancy Jumper", new Money(54.65m, WarehouseCurrency), StockCategory.Formalwear));
            Inventory.Insert(new GenericInventoryItem(WarehouseKeys.LIGHT1, "Head Light", new Money(3.50m, WarehouseCurrency), StockCategory.HeadGear));
            Inventory.Insert(new GenericInventoryItem(WarehouseKeys.VOUR30, "£30 Gift Voucher", new Money(30.00m, WarehouseCurrency), StockCategory.Vouchers));
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
