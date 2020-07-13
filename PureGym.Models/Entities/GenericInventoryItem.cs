using Newtonsoft.Json;
using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Common.Exceptions;
using PureGym.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PureGym.Models.Entities
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GenericInventoryItem : AGenericEntity, IIsAnInventoryItem, IHasAValue
    {
        [JsonProperty]
        public string Description { get; private set; }

        [JsonProperty]
        public Money Price { get; private set; }

        [JsonProperty]
        public StockCategory Category { get; private set; }

        public GenericInventoryItem() : base()
        {
        }

        public GenericInventoryItem(string key, string description, Money price, StockCategory category, Guid id = default(Guid)) :
            base()
        {
            Init(key, description, price, category, id);
        }

        protected void Init(string key, string description, Money price, StockCategory category, Guid id = default(Guid))
        {
            if (HasBeenInitalised()) { return; }

            if (price.Value < 0) { throw new PriceOutOfRangeException($"{nameof(price)} {SharedStrings.WasNegative}"); }
            Price = price;

            if (string.IsNullOrEmpty(description)) { throw new ArgumentNullException($"{nameof(description)} {SharedStrings.WasEmpty}"); }
            Description = description;

            Category = category;

            base.Init(key, id);
        }
    }
}
