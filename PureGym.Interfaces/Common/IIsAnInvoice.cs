using System.Collections.Generic;
using PureGym.Common;

namespace PureGym.Interfaces.Common
{
    public interface IIsAnInvoice
    {
        List<IIsAnItemSummary> BasketItems { get; set; }
        Money BasketTotal { get; set; }
        Money GrandTotal { get; }
        List<IIsAnItemSummary> Offers { get; set; }
        Money OfferTotal { get; set; }
        List<IIsAnItemSummary> Vouchers { get; set; }
        Money VoucherTotal { get; set; }
    }
}