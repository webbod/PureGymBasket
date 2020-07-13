using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
using PureGym.Interfaces.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PureGym.Basket.Strategies.Presentation
{
    public class TextStrategyFactory : IIsAContainerPresentationStrategy
    {
        public Func<List<TEntityType>, List<TOutputType>> Render<TEntityType, TOutputType>(TypesOfContainer typeOfContainer)
            where TEntityType : class, IIsAnEntity, IHasAValue, new()
            where TOutputType : class
        {
            switch (typeOfContainer) {

                case TypesOfContainer.Basket:
                    return new Func<List<TEntityType>, List<TOutputType>>((list) => list.Select(i => { var item = i as IIsABasketItem; return $"{item.Quantity} x {item.Description} @ {item.Price}" as TOutputType; }).ToList());

                case TypesOfContainer.Offer:
                    return new Func<List<TEntityType>, List<TOutputType>>((list) => list.Select(i => { var item = i as IIsAnOfferItem; return $"{item.Description} {(item.CanBeApplied ? "Applied" : "")}" as TOutputType; }).ToList());

                case TypesOfContainer.Voucher:
                    return new Func<List<TEntityType>, List<TOutputType>>((list) => list.Select(i => { var item = i as IIsAVoucherItem; return $"{item.Description} {(item.Applied ? "Applied" : "")}" as TOutputType; }).ToList());

                case TypesOfContainer.Inventory:
                    return new Func<List<TEntityType>, List<TOutputType>>((list) => list.Select(i => { var item = i as IIsAnInventoryItem; return $"{item.Description} @ {item.Price}" as TOutputType; }).ToList());

                default:
                    throw new NotImplementedException($"{nameof(Render)} does not have a renderer for {typeOfContainer.ToString()}");
            }
        }
    }
}
