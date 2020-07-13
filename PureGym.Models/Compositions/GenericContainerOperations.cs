using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
using PureGym.Interfaces.Strategies;
using PureGym.Models.Containers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PureGym.Models.Compositions
{
    public static class GenericContainerOperations
    {
        #region Generic collection operations

        /// <typeparam name="TEntityType">The type of the the entities in the container</typeparam>
        /// <param name="container">target of this operation</param>
        /// <param name="item">item to add to the container</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddToContainer<TEntityType>(AGenericIndexedContainer<TEntityType> container, TEntityType item)
            where TEntityType : class, IIsAnEntity, IHasAValue, new()
        {
            Helper.CheckIfValueIsNull(item, nameof(item));
            container.Insert(item);
        }

        /// <param name="container">target of this operation</param>
        /// <param name="key">the key of the element to remove</param>
        /// <typeparam name="TEntityType">The type of the the entities in the container</typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        public static void RemoveFromContainer<TEntityType>(AGenericIndexedContainer<TEntityType> container, string key)
             where TEntityType : class, IIsAnEntity, IHasAValue, new()
        {
            Helper.CheckIfValueIsNull(key, nameof(key));
            container.Remove(key);
        }

        /// <param name="container">target of this operation</param>
        /// <param name="predicate">a function to filter the items in the container</param>
        /// <typeparam name="TEntityType">The type of the the entities in the container</typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        public static List<TEntityType> FilterContainer<TEntityType>(AGenericIndexedContainer<TEntityType> container, Func<TEntityType,bool> predicate)
             where TEntityType : class, IIsAnEntity, IHasAValue, new()
        {
            Helper.CheckIfValueIsNull(predicate, nameof(predicate));
            return container.Find(predicate);
        }

        /// <typeparam name="TEntityType">The type of the the entities in the container</typeparam>
        /// <param name="container">target of this operation</param>
        /// <param name="expression">a function that calculates the value of an item</param>
        /// <returns>The value of the items in the container</returns>
        public static Money CalculateContainerTotal<TEntityType>(AGenericIndexedContainer<TEntityType> container, Func<TEntityType, Money> expression)
            where TEntityType : class, IIsAnEntity, IHasAValue, new()
        {
            Helper.CheckIfValueIsNull(expression, nameof(expression));
            return container.Sum(expression);
        }

        /// <summary>
        /// Imports data into the container - replacing what was already there
        /// </summary>
        /// <param name="container">target of this operation</param>
        /// <param name="strategy">the persistence strategy to use for import</param>
        /// <param name="data">a representation of the container in a different schema</param>
        /// <typeparam name="TEntityType">The type of the the entities in the container</typeparam>
        /// <typeparam name="TImportType">The type of the data parameter</typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ImportContainer<TEntityType, TImportType>(AGenericIndexedContainer<TEntityType> container, IIsAContainerPersistenceStrategy strategy, TImportType data)
            where TImportType : class
            where TEntityType : class, IIsAnEntity, IHasAValue, new()
        {
            Helper.CheckIfValueIsNull(data, nameof(data));
            Helper.CheckIfValueIsNull(strategy, nameof(strategy));

            container.Import<TImportType>(strategy.ImportData<TEntityType, TImportType>(container.TypeOfContainer), data);
        }

        /// <summary>
        /// Exports the contents of the container
        /// </summary>
        /// <param name="container">target of this operation</param>
        /// <param name="strategy">the persistence strategy to use for export</param>
        /// <typeparam name="TEntityType">The type of the the entities in the container</typeparam>
        /// <typeparam name="TExportType">The type of the exported data</typeparam>
        /// <returns>a representation of the container in a different schema</returns>
        public static TExportType ExportContainer<TEntityType, TExportType>(AGenericIndexedContainer<TEntityType> container, IIsAContainerPersistenceStrategy strategy)
            where TExportType : class
            where TEntityType : class, IIsAnEntity, IHasAValue, new()
        {
            Helper.CheckIfValueIsNull(strategy, nameof(strategy));

            return container.Export<TExportType>(strategy.ExportData<TEntityType, TExportType>(container.TypeOfContainer));
        }

        /// <summary>
        /// Renders the contents of the container
        /// </summary>
        /// <param name="container">target of this operation</param>
        /// <param name="strategy">the presentation strategy to use for export</param>
        /// <typeparam name="TEntityType">The type of the the entities in the container</typeparam>
        /// <typeparam name="TOutputType">The type of the rendered data</typeparam>
        /// <returns>a visual representation of the container</returns>
        public static List<TOutputType> RenderContainer<TEntityType, TOutputType>(AGenericIndexedContainer<TEntityType> container, IIsAContainerPresentationStrategy strategy)
            where TOutputType : class
            where TEntityType : class, IIsAnEntity, IHasAValue, new()
        {
            Helper.CheckIfValueIsNull(strategy, nameof(strategy));

            return container.Render<TOutputType>(strategy.Render<TEntityType, TOutputType>(container.TypeOfContainer));
        }
        #endregion
    }
}
