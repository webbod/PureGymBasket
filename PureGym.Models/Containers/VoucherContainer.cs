using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Common.Exceptions;
using PureGym.Interfaces.Common;
using PureGym.Interfaces.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PureGym.Models.Containers
{
    /// <summary>
    /// Models a store of Voucher items
    /// </summary>
    public class VoucherContainer<TEntityType> : AGenericIndexedContainer<TEntityType>,
        ISupportsContainerPersistence<TEntityType>
        where TEntityType : IIsAVoucherItem, new()
    {
        public VoucherContainer() : base()
        {
            TypeOfContainer = TypesOfContainer.Voucher;
        }

        /// <summary>
        /// Inserts a voucher
        /// Each vouchers can only be used once
        /// </summary>
        /// <exception cref="VoucherAlreadyAppliedException"></exception>
        public override void Insert(TEntityType obj)
        {
            if(Find(obj.Key)?.Equals(default(TEntityType)) == true)
            {
                throw new VoucherAlreadyAppliedException($"{obj.Key} {SharedStrings.AlreadyUsed}");
            }

            base.Insert(obj);
        }
    }
}
