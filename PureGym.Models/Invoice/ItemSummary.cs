using System;
using PureGym.Common;
using PureGym.Interfaces.Common;

namespace PureGym.Models.Invoice
{
    public struct ItemSummary : IIsAnItemSummary
    {
        public string Key { get; set; }

        public string Description { get; set; }

        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public Money Total { get; set; }

    }
}
