using Newtonsoft.Json;
using PureGym.Common;
using PureGym.Common.Exceptions;
using PureGym.Interfaces.Common;
using System;

namespace PureGym.Models.Entities
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class AGenericEntity : IIsAnEntity
    {
        /// <summary>
        /// A globally unique Id for this specific entity
        /// </summary>
        [JsonProperty]
        public Guid Id { get; private set; }

        /// <summary>
        /// A shared identitier among instances of this type of thing
        /// </summary>
        [JsonProperty]
        public string Key { get; private set; }

        [JsonProperty]
        public string Description { get; private set; }

        [JsonProperty]
        public Money Value { get; private set; }

        protected bool HasBeenInitalised => Helper.CheckIfValueIsNotNull<Guid>(Id);

        public AGenericEntity() { }

        public AGenericEntity(string key, string description, Money value, Guid id = default(Guid))
        {
            Init(key, description, value, id);
        }

        protected virtual void Init(string key, string description, Money value, Guid id = default(Guid))
        {
            if(HasBeenInitalised) { return; }

            Helper.CheckIfValueIsNull(key, nameof(key));
            Helper.CheckIfValueIsNull(description, nameof(description));

            if (value < 0) { throw new ValueOutOfRangeException($"{nameof(value)} {SharedStrings.WasNegative}"); }

            Key = key;
            Value = value;
            Description = description;

            Id = Helper.CheckIfValueIsNotNull<Guid>(id) ? id : Guid.NewGuid();
        }
    }
}
