namespace Kreissl.Showcase.Infrastructure
{
    #region Usings

    using System;
    using System.Collections.Generic;

    using Microsoft.Practices.Unity;

    #endregion

    /// <summary>
    /// Dependency Resolver für Unity
    /// </summary>
    public class UnityDependencyResolver : ICustomDependencyResolver
    {
        #region Constants and Fields

        /// <summary>
        /// Der Container
        /// </summary>
        private readonly IUnityContainer _container;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityDependencyResolver"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public UnityDependencyResolver(IUnityContainer container)
        {
            this._container = container;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Ermittelt einen Service im dem Container
        /// </summary>
        /// <param name="serviceType">
        /// Typ des Service
        /// </param>
        /// <returns>
        /// Instanz wenn gefunden, sonst null
        /// </returns>
        public object GetService(Type serviceType)
        {
            try
            {
                return this._container.Resolve(serviceType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return null;
        }

        /// <summary>
        /// Ermittelt eine Instanz anhand des Interfaces
        /// </summary>
        /// <param name="type">
        /// Interface Typ
        /// </param>
        /// <returns>
        /// Instanz wenn gefunden, sonst null
        /// </returns>
        public object GetServiceByInterface(Type type)
        {
            try
            {
                foreach (ContainerRegistration reg in this._container.Registrations)
                {
                    if (type.IsAssignableFrom(reg.MappedToType))
                    {
                        return this._container.Resolve(reg.RegisteredType);
                    }
                }
            }
            catch (Exception)
            {
            }

            return null;
        }

        /// <summary>
        /// Ermittelt alle Services eines bestimmten Registrierungstyps bzw. Interfaces
        /// </summary>
        /// <param name="serviceType">
        /// Registrierter Typ
        /// </param>
        /// <returns>
        /// Liste mit gefundenen Instanzen
        /// </returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return this._container.ResolveAll(serviceType);
            }
            catch
            {
                return new List<object>();
            }
        }

        #endregion
    }
}
