using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Common.Exceptions;
using PureGym.Interfaces.Common;
using PureGym.Interfaces.Containers;
using PureGym.Interfaces.Strategies;
using PureGym.Models.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PureGym.Models.Containers
{
    /// <summary>
    /// Provides an interface for managing a key-based indexed set of TEntityType items
    /// </summary>
    /// <typeparam name="TEntityType">The type of the items managed by the container</typeparam>
    public abstract class AGenericIndexedContainer<TEntityType> : IIsAnIndexedContainer<TEntityType>,
        ISupportsContainerPersistence<TEntityType>
        where TEntityType : IIsAnEntity, IHasAValue, new()
    {
        public Currency Currency { get; private set; }

        /// <summary>
        /// Physical type of the container items
        /// </summary>
        public Type TypeOfEntity { get; private set; }

        /// <summary>
        /// Logical type of the container
        /// </summary>
        public TypesOfContainer TypeOfContainer { get; protected set; }

        /// <summary>
        /// Manages the contents of the container
        /// </summary>
        private Dictionary<string, TEntityType> Store { get; set; }

        #region Constructor and Initalisation
        public AGenericIndexedContainer(Currency currency)
        {
            Initalise(currency);
        }

        /// <summary>
        /// Initialises the state of the container
        /// </summary>
        protected virtual void Initalise(Currency currency)
        {
            Currency = currency;
            Store = new Dictionary<string, TEntityType>();
            TypeOfEntity = typeof(TEntityType);
        }
        #endregion

        #region Container Management
        /// <returns>The number of items in the container</returns>
        public int Count()
        {
            return Store.Count();
        }

        /// <exception cref="EmptyContainerException"></exception>
        public void CheckIfContainerIsEmpty()
        {
            if (Count() == 0)
            {
                throw new EmptyContainerException(SharedStrings.ContainerWasEmpty);
            }
        }

        /// <param name="key">The key for the element to find</param>
        /// <returns>The specified item from the container</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public TEntityType Find(string key)
        {
            Helper.CheckIfValueIsNull(key, nameof(key));

            if (Store.ContainsKey(key))
            {
                return Store[key];
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        /// <param name="predicate">a function to test each element for a condition</param>
        /// <returns>A list of of the matched items from the container</returns>
        public List<TEntityType> Find(Func<TEntityType, bool> predicate)
        {
            Helper.CheckIfValueIsNull(predicate, nameof(predicate));

            return GetAll().Where(item => predicate(item))?.ToList()
                ?? new List<TEntityType>();
        }

        /// <returns>A list of all of the items in the container</returns>
        public List<TEntityType> GetAll()
        {
            return Store.Values.ToList();
        }

        /// <param name="expression">a function mapping an item to a money value</param>
        /// <returns>The sum of the expression applied to all items</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Money Sum(Func<TEntityType, Money> expression)
        {
            Helper.CheckIfValueIsNull(expression, nameof(expression));
            return GetAll().Select(expression).Sum(Currency);
        }

        /// <summary>
        /// If the item already exists then it will be updated in the container, otherwise it will be inserted
        /// </summary>
        /// <param name="item">The item to insert into the container</param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void Insert(TEntityType item)
        {
            Helper.CheckIfValueIsNull(item, nameof(item));
            Helper.CheckIfValueIsNull(item.Key, nameof(item.Key));

            if (Store.ContainsKey(item.Key))
            {
                Store[item.Key] = item;
            }
            else
            {
                Store.Add(item.Key, item);
            }
        }

        /// <summary>
        /// Removes the value with the specified key
        /// </summary>
        /// <param name="key">The key of the element to remove</param>
        public virtual void Remove(string key)
        {
            Helper.CheckIfValueIsNull(key, nameof(key));

            Store.Remove(key);
        }

        /// <summary>
        /// Removes all items from the container
        /// </summary>
        public void ClearAll()
        {
            Store.Clear();
        }
        #endregion

        #region Rendering
        /// <param name="renderer">A transformation function to map each item to a string</param>
        /// <returns>A List of strings representing each item in the container</returns>
        public virtual List<TOutputType> Render<TOutputType>(Func<List<TEntityType>, List<TOutputType>> renderer)
        {
            Helper.CheckIfValueIsNull(renderer, nameof(renderer));
            CheckIfContainerIsEmpty();

            return renderer(GetAll());
        }

        /// <returns>A summary of each item in the container</returns>
        public virtual List<IIsAnItemSummary> Summarise()
        {
            return GetAll().Select(i => new ItemSummary { Id = i.Id, Key = i.Key, Description = i.ToString() } as IIsAnItemSummary).ToList();
        }

        public virtual Money CalculateTotal(Money runningTotal)
        {
            return GetAll().Sum(i => i.Value, Currency);
        }
        #endregion

        #region ISupportsContainerPersistence - Import/Export
        /// <param name="serializer">A function that transforms data into a list of items</param>
        /// <param name="data">Data to import</param>
        /// <returns>A list of items</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public List<TEntityType> Import<TImportType>(Func<TImportType, List<TEntityType>> importer, TImportType data)
             where TImportType : class
        {
            Helper.CheckIfValueIsNull(importer, nameof(importer));
            Helper.CheckIfValueIsNull(data, nameof(data));

            return importer(data);
        }

        /// <param name="exporter">A functiion that transforms a list of items into data</param>
        /// <returns>The result of the transformation, the caller is responsible for casting it to the correct type</returns>
        /// <exception cref="EmptyContainerException"></exception>
        public TExportType Export<TExportType>(Func<List<TEntityType>, TExportType> exporter)
            where TExportType : class
        {
            Helper.CheckIfValueIsNull(exporter, nameof(exporter));
            CheckIfContainerIsEmpty();

            return exporter(GetAll());
        }
        #endregion
    }
}
