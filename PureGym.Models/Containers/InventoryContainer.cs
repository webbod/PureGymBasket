using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
using PureGym.Interfaces.Strategies;

namespace PureGym.Models.Containers
{
    /// <summary>
    /// Models a store of Inventory items
    /// </summary>
    public class InventoryContainer<TEntityType> : AGenericIndexedContainer<TEntityType>,
        ISupportsContainerPersistence<TEntityType>
        where TEntityType : IIsAnInventoryItem, new()
    {
        public InventoryContainer(Currency currency) : base(currency)
        {
            TypeOfContainer = TypesOfContainer.Inventory;
        }
    }
}
