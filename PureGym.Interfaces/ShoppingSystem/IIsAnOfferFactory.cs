using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;

namespace PureGym.Interfaces.ShoppingSystem
{
    public interface IIsAnOfferFactory
    {
        void Initalise(Currency currency);

        IIsAnOfferItem IssueOffer(string key);

        IIsAVoucherItem IssueVoucher(string key);
    }
}