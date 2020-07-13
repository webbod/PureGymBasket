using Newtonsoft.Json;
using PureGym.Common;
using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
using PureGym.Interfaces.Containers;
using PureGym.Interfaces.Strategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace PureGym.Basket.Strategies.Persistence
{
    /// <summary>
    /// Generates methods for serialising and deserialising the contents of a container to Json
    /// </summary>
    public class JsonStrategyFactory : IIsAContainerPersistenceStrategy
    {

        /// <param name="container">The container to return a function for</param>
        /// <returns>A function that deserializes an list of items from a JSON string</returns>
        /// <typeparam name="TEntityType">The type of entity in the container</typeparam>
        /// <typeparam name="TImportType">The type of the data</typeparam>
        public Func<TImportType, List<TEntityType>> ImportData<TEntityType, TImportType>(TypesOfContainer container)
            where TImportType : class
        {
            // ignore TypeOfContainer - for now all of the containers work in the same fashion

            if(typeof(TImportType) != typeof(string)) { throw new InvalidCastException($"The import {typeof(TImportType).Name} is invalid this strategy requires string"); }

            return new Func<TImportType, List<TEntityType>>((data) =>
            {
                Helper.CheckIfValueIsNull(data, nameof(data));

                var output = JsonConvert.DeserializeObject<List<TEntityType>>(data as string);
                return output;
            });
        }

        /// <param name="container">The container to return a function for</param>
        /// <returns>A function that serializes an list of items to a JSON string</returns>
        /// <typeparam name="TEntityType">The type of entity in the container</typeparam>
        /// <typeparam name="TImportType">The type of the data</typeparam>
        public Func<List<TEntityType>, TOutputType> ExportData<TEntityType, TOutputType>(TypesOfContainer container)
        where TOutputType : class
        {
            // ignore TypeOfContainer - for now all of the containers work in the same fashion

            return new Func<List<TEntityType>, TOutputType>((data) =>
            {
                Helper.CheckIfValueIsNull(data, nameof(data));

                var output = JsonConvert.SerializeObject(data);
                return output as TOutputType;
            });
        }

    }
}
