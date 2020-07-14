using System.Collections.Generic;

namespace PureGym.Interfaces.Common
{
    public interface IIsAnOfferItem : IIsAnEntity, IHasAValue
    {
        /// <summary>
        /// Explains why the offer can't be used
        /// </summary>
        string Reason { get; }

        /// <summary>
        /// Indicates that the offer can be used
        /// </summary>
        bool CanBeApplied { get; }

        /// <summary>
        /// Tests if the offer can be used
        /// </summary>
        void SeeIfItCanBeAppliedTo(List<IIsABasketItem> basket);
    }

}
