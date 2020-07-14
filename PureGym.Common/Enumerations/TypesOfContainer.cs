using System;

namespace PureGym.Common.Enumerations
{
    [Flags]
    public enum TypesOfContainer
    {
        /// <summary>
        /// Stores IIsABasketItem
        /// </summary>
        Basket,

        /// <summary>
        /// Stores IIsAVoucherItem
        /// </summary>
        Voucher,

        /// <summary>
        /// Stores IIsAnOfferItem
        /// </summary>
        Offer,

        /// <summary>
        /// Stores IIsAnInventoryItem
        /// </summary>
        Inventory,

        /// <summary>
        /// A High
        /// </summary>
        Shop,

        /// <summary>
        /// Basket, Offer and Voucher containers
        /// </summary>
        AllShoppingBasketContainers = TypesOfContainer.Basket | TypesOfContainer.Offer | TypesOfContainer.Voucher
    }
}
