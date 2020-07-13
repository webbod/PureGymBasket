using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Models.Containers;
using PureGym.Models.Entities;
using PureGym.ShoppingSystem;
using System;

namespace PureGym.Basket
{
    public class TestWarehouse : IIsAWarehouse
    {
        protected InventoryContainer<GenericInventoryItem> Inventory;
        protected Currency WarehouseCurrency;

        public void Initalise(Currency currency)
        {
            if (Helper.CheckIfValueIsNotNull(Inventory)) { return; }

            WarehouseCurrency = currency;

            Inventory = new InventoryContainer<GenericInventoryItem>();

            Inventory.Insert(new GenericInventoryItem("HAT001", "A Nice Hat", new Money(17.50m, WarehouseCurrency), StockCategory.HeadGear));
            Inventory.Insert(new GenericInventoryItem("COAT01", "A Decent Coat", new Money(65m, WarehouseCurrency), StockCategory.Formalwear));
            Inventory.Insert(new GenericInventoryItem("SHOE11", "Cheap Shoes", new Money(12.99m, WarehouseCurrency), StockCategory.Footwear));
        }

        public GenericBasketItem GetItem(string key)
        {
            return GenericBasketItem.CreateFrom<GenericInventoryItem>(Inventory.Find(key), 1);
        }

        protected void Insert(string key, string description, Money price, StockCategory category, Guid id = default(Guid))
        {
            Inventory.Insert(new GenericInventoryItem(key, description, price, category, id));
        }
    }
}
