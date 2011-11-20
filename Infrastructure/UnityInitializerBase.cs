namespace Kreissl.Showcase.Infrastructure
{
    #region Usings

    using System;
    using System.Web;

    using Microsoft.Practices.Unity;

    #endregion

    /// <summary>
    ///   The unity initializer base.
    /// </summary>
    public abstract class UnityInitializerBase
    {
        #region Constants and Fields

        /// <summary>
        ///   Lock Objekt
        /// </summary>
        protected static readonly object Mlock = new object();

        /// <summary>
        ///   Der Container
        /// </summary>
        protected static volatile IUnityContainer container;

        #endregion

        #region Public Methods

        /// <summary>
        ///   löscht unityContainer aktuell nur für tests benötigt
        /// </summary>
        public static void CleanUpContainer()
        {
            if (container != null)
            {
                lock (Mlock)
                {
                    if (container != null)
                    {
                        container.Dispose();
                        container = null;
                    }
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///   Der Unity Container
        /// </summary>
        /// <param name = "scope">Service scope</param>
        /// <returns>Unity Container</returns>
        protected static IUnityContainer GetContainer(ServiceScope scope)
        {
            if (container == null)
            {
                lock (Mlock)
                {
                    if (container == null)
                    {
                        container = RegisterDeclaredServices(scope);
                    }
                }
            }

            return container;
        }

        /// <summary>
        ///   Registriert alle Services, welche im angegebenen Scope über das ServiceAttribut
        ///   definiert sind.
        /// </summary>
        /// <param name = "scope">Service Scope</param>
        /// <returns>Der initialisierte Container</returns>
        protected static IUnityContainer RegisterDeclaredServices(ServiceScope scope)
        {
            var unityContainer = new UnityContainer();

            foreach (ServiceContextAttribute type in ServiceAttribute.GetTypes(scope))
            {
                switch (type.Behaviour)
                {
                    case ServiceBehaviour.NewInstance:
                        {
                            if (!string.IsNullOrEmpty(type.InjectionParam))
                                unityContainer.RegisterType(type.RegisterFrom, type.RegisterTo, type.InjectionParam);
                            else
                                unityContainer.RegisterType(type.RegisterFrom, type.RegisterTo);
                            break;
                        }

                    case ServiceBehaviour.Singleton:
                        {
                            var lfmr = new ContainerControlledLifetimeManager();
                            unityContainer.RegisterType(type.RegisterFrom, type.RegisterTo, lfmr);
                            break;
                        }

                    case ServiceBehaviour.Webrequest:
                        {
                            if (scope == ServiceScope.Frontend)
                            {
                                RegisterPerWebRequest(type.RegisterFrom, type.RegisterTo, unityContainer);
                            }
                            else
                            {
                                var lfmr = new ContainerControlledLifetimeManager();
                                unityContainer.RegisterType(type.RegisterFrom, type.RegisterTo, lfmr);
                            }

                            break;
                        }
                }
            }

            return unityContainer;
        }

        /// <summary>
        ///   Registriert eine Instanz für einen Web Request
        /// </summary>
        /// <param name = "from">
        ///   Interface Typ
        /// </param>
        /// <param name = "to">
        ///   Instanze Typ
        /// </param>
        /// <param name = "unitycontainer">
        ///   Der Unity Container
        /// </param>
        /// <remarks>
        ///   Wenn der HttpContext null ist, wird anstelle des HttpContextLifetimeManager
        ///   der TestMethodeLifetimeManager verwendet.
        /// </remarks>
        protected static void RegisterPerWebRequest(Type from, Type to, IUnityContainer unitycontainer)
        {
            if (HttpContext.Current != null)
            {
                var lfmgr = typeof(HttpContextLifetimeManager<>);
                var generic = lfmgr.MakeGenericType(new[] { @from });
                unitycontainer.RegisterType(@from, to, (LifetimeManager)Activator.CreateInstance(generic));
            }
            else
            {
                var lfmgr = typeof(TestMethodeLifetimeManager<>);
                var generic = lfmgr.MakeGenericType(new[] { @from });
                unitycontainer.RegisterType(@from, to, (LifetimeManager)Activator.CreateInstance(generic));
            }
        }

        #endregion
    }
}
