using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
using PureGym.Interfaces.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PureGym.Models.Containers
{
    /// <summary>
    /// Models a store of Inventory items
    /// </summary>
    public class InventoryContainer<TEntityType> : AGenericIndexedContainer<TEntityType>,
        ISupportsContainerPersistence<TEntityType>
        where TEntityType : IIsAnInventoryItem, new()
    {
        public InventoryContainer() : base()
        {
            TypeOfContainer = TypesOfContainer.Inventory;
        }

        public object Any()
        {
            throw new NotImplementedException();
        }
    }
}
