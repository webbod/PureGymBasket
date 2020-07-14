using PureGym.Common;
using PureGym.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PureGym.Models.Invoice
{
    public class GenericInvoice : IIsAnInvoice
    {
        public List<IIsAnItemSummary> BasketItems { get; set; }
        public Money BasketTotal { get; set; }

        public List<IIsAnItemSummary> Vouchers { get; set; }
        public Money VoucherTotal { get; set; }

        public List<IIsAnItemSummary> Offers { get; set; }
        public Money OfferTotal { get; set; }

        public Money GrandTotal
        {
            get
            {
                var total = BasketTotal - VoucherTotal - OfferTotal;
                return total >= 0 ? total : total.ToZero();
            }
        }
    }
}