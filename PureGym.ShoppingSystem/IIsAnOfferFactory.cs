using PureGym.Common.Enumerations;
using PureGym.Models.Entities;

namespace PureGym.ShoppingSystem
{
    public interface IIsAnOfferFactory
    {
        void Initalise(Currency currency);

        IIsAnOffer IssueOffer(string key);
    }
}