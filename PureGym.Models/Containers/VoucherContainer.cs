using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Common.Exceptions;
using PureGym.Interfaces.Common;
using PureGym.Interfaces.Strategies;
using PureGym.Models.Invoice;
using System.Collections.Generic;
using System.Linq;

namespace PureGym.Models.Containers
{
    /// <summary>
    /// Models a store of Voucher items
    /// </summary>
    public class VoucherContainer<TEntityType> : AGenericIndexedContainer<TEntityType>,
        ISupportsContainerPersistence<TEntityType>
        where TEntityType : IIsAVoucherItem, new()
    {
        public VoucherContainer(Currency currency) : base(currency)
        {
            TypeOfContainer = TypesOfContainer.Voucher;
        }

        //TODO Find a way to stop the exact same voucher being applied over and over again
        /// <summary>
        /// Inserts a voucher
        /// </summary>
        /// <exception cref="VoucherAlreadyAppliedException"></exception>
        public override void Insert(TEntityType obj)
        {
            base.Insert(obj);
        }

        /// <returns>A summary of each item in the container</returns>
        public override List<IIsAnItemSummary> Summarise()
        {
            return GetAll().Select(i => new ItemSummary { Id = i.Id, Key = i.Key, Description = i.ToString(), Quantity = 1, Total = i.Value } as IIsAnItemSummary).ToList();
        }

        public override Money CalculateTotal(Money runningTotal)
        {
            GetAll().ForEach(i => {
                if (runningTotal > 0)
                {
                    i.ApplyVoucher();
                    runningTotal -= i.Value;
                } else
                {
                    i.DontApplyVoucher();
                 }
            });

            return Find(i => i.Applied).Sum(i => i.Value, Currency);
        }
    }
}
