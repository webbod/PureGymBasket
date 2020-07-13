using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
using PureGym.Models.Entities;

namespace PureGym.ShoppingSystem
{
    public interface IIsAnOfferFactory
    {
        void Initalise(Currency currency);

        IIsAnOfferItem IssueOffer(string key);
        IIsAVoucherItem IssueVoucher(string key);
    }
}