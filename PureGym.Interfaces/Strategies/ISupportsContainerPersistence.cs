using PureGym.Interfaces.Common;
using System;
using System.Collections.Generic;

namespace PureGym.Interfaces.Strategies
{
    /// <summary>
    /// Indicates that a container supports persistence through a serialization/deserialization metaphor
    /// </summary>
    /// <typeparam name="TEntityType">The type of items in the container</typeparam>
    public interface ISupportsContainerPersistence<TEntityType> where TEntityType : IIsAnEntity, new()
    {
        /// <typeparam name="TImportType">The type of the data parameter</typeparam>
        /// <param name="importer">a function to transform data into a list of items</param>
        /// <param name="data">a representation of the list in a different schema</param>
        /// <returns>list of items</returns>
        List<TEntityType> Import<TImportType>(Func<TImportType, List<TEntityType>> importer, TImportType data) where TImportType : class;

        /// <typeparam name="TExportType">The type of the exported data</typeparam>
        /// <param name="exporter">a function to transform a list of items into an object</param>
        /// <returns>transformed data</returns>
        TExportType Export<TExportType>(Func<List<TEntityType>, TExportType> exporter) where TExportType : class;
    }
}
