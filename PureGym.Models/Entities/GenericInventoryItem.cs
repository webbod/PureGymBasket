using Newtonsoft.Json;
using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
using System;

namespace PureGym.Models.Entities
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GenericInventoryItem : AGenericEntity, IIsAnInventoryItem, IHasAValue
    {
        [JsonProperty]
        public StockCategory Category { get; private set; }

        public GenericInventoryItem() : base()
        {
        }

        public GenericInventoryItem(string key, string description, Money value, StockCategory category, Guid id = default(Guid)) :
            base()
        {
            Init(key, description, value, category, id);
        }

        protected void Init(string key, string description, Money value, StockCategory category, Guid id = default(Guid))
        {
            if (HasBeenInitalised) { return; }

            Helper.CheckIfValueIsNotNull(category);
            Category = category;

            base.Init(key, description, value, id);
        }
    }
}
