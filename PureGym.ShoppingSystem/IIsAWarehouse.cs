using PureGym.Common.Enumerations;
using PureGym.Models.Entities;

namespace PureGym.ShoppingSystem
{
    public interface IIsAWarehouse
    {
        void Initalise(Currency currency);

        GenericBasketItem GetItem(string key);
    }
}