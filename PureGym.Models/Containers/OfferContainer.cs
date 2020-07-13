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
    /// Models a store of Offer Items
    /// </summary>
    public class OfferContainer<TEntityType> : AGenericIndexedContainer<TEntityType>,
       ISupportsContainerPersistence<TEntityType>
        where TEntityType : IIsAnOfferItem, new()
    {

        public OfferContainer() : base()
        {
            TypeOfContainer = TypesOfContainer.Offer;
        }

        /// <summary>
        /// Inserts an offer
        /// There can only be one offer applied at a time
        /// </summary>
        /// <exception cref="TooManyVouchersException"></exception>
        public override void Insert(TEntityType obj)
        {
            if (Count() != 0)
            {
                throw new TooManyVouchersException($"{SharedStrings.OnlyOneAllowed}");
            }

            base.Insert(obj);
        }
    }
}
