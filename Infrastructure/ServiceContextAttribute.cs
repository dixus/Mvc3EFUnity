namespace Kreissl.Showcase.Infrastructure
{
    using System;

    /// <summary>
    /// Basis für das Service Registrierungsattribut
    /// </summary>
    public abstract class ServiceContextAttribute : Attribute
    {
        protected ServiceContextAttribute(
            Type registerFrom,
            ServiceBehaviour behaviour = ServiceBehaviour.NewInstance,
            ServiceScope scope = ServiceScope.Web,
            ServiceEnvironemnt environemnt = ServiceEnvironemnt.Anywhere,
            bool addInterfaceInterceptor = false)
        {
            this.RegisterFrom = registerFrom;
            this.Behaviour = behaviour;
            this.Scope = scope;
            this.AddInterfaceInterceptor = addInterfaceInterceptor;
            this.Environment = environemnt;
        }

        #region Public Properties

        /// <summary>
        ///     Interface Interceptor einfügen
        /// </summary>
        public bool AddInterfaceInterceptor { get; set; }

        /// <summary>
        ///     Lebenszyklus des Service
        /// </summary>
        public ServiceBehaviour Behaviour { get; set; }

        /// <summary>
        ///     Umgebung des Service
        /// </summary>
        public ServiceEnvironemnt Environment { get; set; }

        /// <summary>
        ///     Injection Param
        /// </summary>
        public string InjectionParam { get; set; }

        /// <summary>
        ///     Mappender Typ (Interface)
        /// </summary>
        public Type RegisterFrom { get; set; }

        /// <summary>
        ///     Typ der Impementierung
        /// </summary>
        public Type RegisterTo { get; set; }

        /// <summary>
        ///     Scope des Service
        /// </summary>
        public ServiceScope Scope { get; set; }

        #endregion
    }
}
