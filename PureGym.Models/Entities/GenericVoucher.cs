using PureGym.Common;
using PureGym.Interfaces.Common;
using System;

namespace PureGym.Models.Entities
{
    public class GenericVoucher : AGenericEntity, IIsAVoucherItem
    {
        public bool Applied { get; private set; }

        public GenericVoucher() {   }

        public GenericVoucher(string key, string description, Money value, Guid id = default(Guid)) : base()
        {
            Init(key, description, value, id);
        }

        public void ApplyVoucher() { Applied = true; }

        public void DontApplyVoucher() { Applied = false; }

        public override string ToString() => $"{Description} {(Applied ? SharedStrings.Applied : "")}";
        
    }
}
