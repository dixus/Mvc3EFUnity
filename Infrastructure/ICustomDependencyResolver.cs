namespace Kreissl.Showcase.Infrastructure
{
    #region Usings

    using System;

    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// Erweiterung zum Dependency Resolver
    /// </summary>
    public interface ICustomDependencyResolver : IDependencyResolver
    {
        #region Public Methods

        /// <summary>
        /// Sucht einen Service im Container anhand des Interfaces
        /// </summary>
        /// <param name="type">
        /// Interface Typ
        /// </param>
        /// <returns>
        /// Service Instanz
        /// </returns>
        object GetServiceByInterface(Type type);
        #endregion
    }
}
