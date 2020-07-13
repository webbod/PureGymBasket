using Newtonsoft.Json;
using PureGym.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PureGym.Models.Entities
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class AGenericEntity : IIsAnEntity
    {
        [JsonProperty]
        public Guid Id { get; private set; }

        [JsonProperty]
        public string Key { get; private set; }

        public AGenericEntity() { }

        public AGenericEntity(string key, Guid id = default(Guid))
        {
            Init(key, id);
        }

        protected bool HasBeenInitalised()
        {
            return !Id.Equals(default(Guid));
        }

        protected virtual void Init(string key, Guid id = default(Guid))
        {
            if(HasBeenInitalised()) { return; }

            if (string.IsNullOrEmpty(key)) { throw new ArgumentNullException($"{nameof(key)} was empty"); }
            Key = key;

            Id = id.Equals(default(Guid)) ? Guid.NewGuid() : id;

        }
    }
}
