using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
using PureGym.Interfaces.Strategies;
using PureGym.Models.Invoice;
using System.Collections.Generic;
using System.Linq;

namespace PureGym.Models.Containers
{
    /// <summary>
    /// Models a store of Basket items
    /// </summary>
    public class BasketContainer<TEntityType> : AGenericIndexedContainer<TEntityType>, 
        ISupportsContainerPersistence<TEntityType> 
        where TEntityType : IIsABasketItem, new()
    {

        public BasketContainer(Currency currency) : base(currency)
        {
            TypeOfContainer = TypesOfContainer.Basket;
        }

        /// <summary>
        /// Inserts a new item or updates the quantity on an existing one
        /// </summary>
        public override void Insert(TEntityType obj)
        {
            try
            {
                var existingObj = Find(obj.Key);
                existingObj.Update(quantity: obj.Quantity);
                base.Insert(existingObj);
            }
            catch(KeyNotFoundException)
            {
                base.Insert(obj);
            }
        }

        /// <returns>A summary of each item in the container</returns>
        public override List<IIsAnItemSummary> Summarise()
        {
            return GetAll().Select(i => new ItemSummary { Id = i.Id, Key = i.Key, Description = i.ToString(), Quantity = i.Quantity, Total = i.Value * i.Quantity } as IIsAnItemSummary).ToList();
        }

        public override Money CalculateTotal(Money runningTotal)
        {
            return GetAll().Sum(i => i.Value * i.Quantity , Currency);
        }
    }
}
