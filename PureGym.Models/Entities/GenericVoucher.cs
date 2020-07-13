using PureGym.Common;
using PureGym.Common.Exceptions;
using PureGym.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PureGym.Models.Entities
{
    public class GenericVoucher : AGenericEntity, IIsAVoucherItem
    {
        public string Description { get; private set; }

        public Money Cost { get; private set; }

        public bool Applied { get; private set; }

        public GenericVoucher() {   }

        public GenericVoucher(string key, string description, Money cost, Guid id = default(Guid)) :
            base(key, id)
        {
            Init(key, description, cost, id);
        }

        protected void Init(string key, string description, Money cost, Guid id = default(Guid))
        {
            if (HasBeenInitalised()) { return; }

            if (cost.Value < 0) { throw new PriceOutOfRangeException($"{nameof(cost)} {SharedStrings.WasNegative}"); }
            Cost = cost;

            if (string.IsNullOrEmpty(description)) { throw new ArgumentNullException($"{nameof(description)} {SharedStrings.WasEmpty}"); }
            Description = description;

            Applied = false;

            base.Init(key, id);
        }
    }
}
