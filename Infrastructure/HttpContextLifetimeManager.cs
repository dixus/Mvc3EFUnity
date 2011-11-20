// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpContextLifetimeManager.cs" company="T-Systems Multimedia Solutions GmbH">
//   Copyright (c) Riesaer Str. 5, 01129 Dresden. All rights reserved.
// </copyright>
// <summary>
//   Defines the HttpContextLifetimeManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Kreissl.Showcase.Infrastructure
{
    #region Usings

    using System;

    using System.Web;

    using Microsoft.Practices.Unity;

    #endregion

    /// <summary>
    /// Lifetime Manager für Http Context abhängige Services
    /// </summary>
    /// <typeparam name="T">
    /// der Service Typ
    /// </typeparam>
    public sealed class HttpContextLifetimeManager<T> : LifetimeManager, IDisposable
    {
        #region Public Methods

        /// <summary>
        /// Löscht den Typ T
        /// </summary>
        public void Dispose()
        {
            this.RemoveValue();
        }

        /// <summary>
        /// Liest einen Wert des Typs T aus
        /// </summary>
        /// <returns>
        /// der Wert aus dem Manager
        /// </returns>
        public override object GetValue()
        {
            string t = typeof(T).AssemblyQualifiedName;
            if (t != null)
            {
                return HttpContext.Current.Items[t];
            }

            return null;
        }

        /// <summary>
        /// Entfernt den Typ
        /// </summary>
        public override void RemoveValue()
        {
            string t = typeof(T).AssemblyQualifiedName;
            if (t != null)
            {
                HttpContext.Current.Items.Remove(t);
            }
        }

        /// <summary>
        /// Sett einen Wert
        /// </summary>
        /// <param name="newValue">
        /// neuer Wert
        /// </param>
        public override void SetValue(object newValue)
        {
            string t = typeof(T).AssemblyQualifiedName;
            if (t != null)
            {
                HttpContext.Current.Items[t] = newValue;
            }
        }

        #endregion
    }
}
