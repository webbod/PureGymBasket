using PureGym.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PureGym.Interfaces.Common
{
    public interface IIsAnOfferItem : IIsAnEntity, IHasAValue
    {
        string Description { get; }

        string Message { get; }

        Money Value { get; }

        bool CanBeApplied { get; }

        void SeeIfItCanBeAppliedTo(List<IIsABasketItem> basket);
    }

}
