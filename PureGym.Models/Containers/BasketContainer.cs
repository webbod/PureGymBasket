using Newtonsoft.Json;
using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
using PureGym.Interfaces.Containers;
using PureGym.Interfaces.Strategies;
using PureGym.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        /// Quantity is the only mutable property
        /// </summary>
        public override void Insert(TEntityType obj)
        {
            try
            {
                var existingObj = Find(obj.Key);

                if (existingObj?.Equals(default(TEntityType)) == true)
                {
                    existingObj.Update(quantity: obj.Quantity);
                    base.Insert(existingObj);
                }
            }
            catch(KeyNotFoundException)
            {
                base.Insert(obj);
            }
        }
    }
}
