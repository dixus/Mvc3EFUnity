// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceAttribute.cs" company="T-Systems Multimedia Solutions GmbH">
//   Copyright (c) Riesaer Str. 5, 01129 Dresden. All rights reserved.
// </copyright>
// <summary>
//   Das RegisterService Attribute zur deklarativen Service-Konfiguration
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Kreissl.Showcase.Infrastructure
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using System.Web;

    #endregion

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ServiceAttribute : ServiceContextAttribute
    {
        #region Constructors and Destructors

        public ServiceAttribute(
            Type registerFrom,
            ServiceBehaviour behaviour = ServiceBehaviour.NewInstance,
            ServiceScope scope = ServiceScope.Web,
            ServiceEnvironemnt environemnt = ServiceEnvironemnt.Anywhere,
            bool addInterfaceInterceptor = false)
            : base(registerFrom, behaviour, scope, environemnt, addInterfaceInterceptor)
        {
        }

        public ServiceAttribute(
           Type registerFrom, string injectionParam,
           ServiceScope scope = ServiceScope.Web)
            : base(registerFrom, ServiceBehaviour.NewInstance, scope, ServiceEnvironemnt.Anywhere)
        {
            this.InjectionParam = injectionParam;
        }


        public ServiceAttribute(
           Type registerFrom, string injectionParam,
             bool addInterfaceInterceptor,
           ServiceScope scope = ServiceScope.Web)
            : base(registerFrom, ServiceBehaviour.NewInstance, scope, ServiceEnvironemnt.Anywhere, addInterfaceInterceptor)
        {
            this.InjectionParam = injectionParam;
        }

        #endregion

        #region Public Methods

        public static IEnumerable<ServiceContextAttribute> GetTypes(ServiceScope scope)
        {
            foreach (var assembly in AssemblyLocator.LoadAll())
            {
                foreach (var type in GetAllServiceTypes(assembly))
                {
                    foreach (var attribute in type.Attributes.Where(a => a.Scope.HasFlag(scope)))
                    {
                        if (ValidateEnvironment(attribute))
                        {
                            attribute.RegisterTo = type.RegisterTo;
                            yield return attribute;
                        }
                    }
                }
            }
        }

        #endregion

        #region Methods

        private static IEnumerable<ReflectedType> GetAllServiceTypes(Assembly assembly)
        {
            var types = new List<ReflectedType>();

            try
            {
                types = (from type in assembly.GetTypes()
                         where
                             type.IsClass
                             &&
                             ((IEnumerable<ServiceAttribute>)type.GetCustomAttributes(typeof(ServiceAttribute), true)).Any()
                         let attributes =
                             (IEnumerable<ServiceAttribute>)type.GetCustomAttributes(typeof(ServiceAttribute), true)
                         select new ReflectedType { RegisterTo = type, Attributes = attributes }).ToList();
            }
            catch (ReflectionTypeLoadException ex)
            {
                foreach (var loadex in ex.LoaderExceptions)
                {
                    System.Diagnostics.Trace.WriteLine(loadex.Message);
                }
            }

            return types;
        }

        private static bool ValidateEnvironment(ServiceContextAttribute context)
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Request.IsLocal && context.Environment == ServiceEnvironemnt.RemoteOnly)
                {
                    return false;
                }

                if (HttpContext.Current.Request.IsLocal && context.Environment == ServiceEnvironemnt.RemoteOnly)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion


        /// <summary>
        /// Intermediate Type
        /// </summary>
        internal class ReflectedType
        {
            #region Public Properties

            public IEnumerable<ServiceAttribute> Attributes { get; set; }

            public Type RegisterTo { get; set; }

            #endregion
        }
    }
}
