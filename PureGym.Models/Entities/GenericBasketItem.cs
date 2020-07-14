using Newtonsoft.Json;
using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Common.Exceptions;
using PureGym.Interfaces.Common;
using System;

namespace PureGym.Models.Entities
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GenericBasketItem : GenericInventoryItem, IIsABasketItem
    {
        [JsonProperty]
        public int Quantity { get; private set; }

        public GenericBasketItem() : base()
        {
        }

        public GenericBasketItem(string key, string description, Money value, StockCategory category, int quantity, Guid id = default(Guid)) :
            base()
        {
            Init(key, description, value, category, quantity, id);
        }

        public static GenericBasketItem CreateFrom<T>(T item, int quantity, Guid id = default(Guid)) where T : IIsAnInventoryItem
        {
            return new GenericBasketItem(item.Key, item.Description, item.Value, item.Category, quantity, id);
        }


        protected void Init(string key, string description, Money price, StockCategory category, int quantity, Guid id = default(Guid))
        {
            if (HasBeenInitalised) { return; }

            if(quantity <= 0) { throw new QuantityOutOfRangeException($"{nameof(quantity)} {SharedStrings.WasLessThanOne}"); }
            Quantity = quantity;
            
            base.Init(key, description, price, category, id);
        }

        public void Update(IIsAnInventoryItem item = default(IIsAnInventoryItem), int quantity = 1)
        {
            if (!HasBeenInitalised &&  Helper.CheckIfValueIsNotNull(item))
            {
                Init(item.Key, item.Description, item.Value, item.Category, quantity, item.Id);
            }          
        }

        public void UpdateQuantity(int quantity)
        {
            Quantity = quantity;
        }

        public override string ToString() => $"{Quantity} x {Description} @ {Value}";

    }
}
