namespace Kreissl.Showcase.Infrastructure
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using Microsoft.Practices.Unity;

    #endregion

    /// <summary>
    /// Generischer LifeTime Manager für Testmethoden
    /// </summary>
    /// <typeparam name="T">
    /// Typ des Service
    /// </typeparam>
    public sealed class TestMethodeLifetimeManager<T> : TestMethodeLifetimeManager, IDisposable
    {
        #region Public Methods

        /// <summary>
        /// Dispose entfernt einen Typ
        /// </summary>
        public void Dispose()
        {
            this.RemoveValue();
        }

        /// <summary>
        /// Gibt Objekt aus Manager zurück
        /// </summary>
        /// <returns>
        /// Typ aus Liste
        /// </returns>
        public override object GetValue()
        {
            string t = typeof(T).AssemblyQualifiedName;
            if (t != null && Objects.ContainsKey(t))
            {
                return (T)Objects[t];
            }

            return null;
        }

        /// <summary>
        /// Entfernt die Instanz
        /// </summary>
        public override void RemoveValue()
        {
            string t = typeof(T).AssemblyQualifiedName;
            if (t != null && Objects.ContainsKey(t))
            {
                Objects.Remove(t);
            }
        }

        /// <summary>
        /// Setzt eine Instanz
        /// </summary>
        /// <param name="newValue">
        /// Neuer Wert
        /// </param>
        public override void SetValue(object newValue)
        {
            string t = typeof(T).AssemblyQualifiedName;
            if (t != null && Objects.ContainsKey(t))
            {
                Objects.Remove(t);
            }

            if (t != null)
            {
                Objects.Add(t, newValue);
            }
        }

        #endregion
    }

    /// <summary>
    /// LifeTime Manager für Testmethoden
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Aufteilung auf verschiee Files geht nicht da andere klasse generische ableitung ist.")]
    public abstract class TestMethodeLifetimeManager : LifetimeManager
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref = "TestMethodeLifetimeManager" /> class.
        /// </summary>
        static TestMethodeLifetimeManager()
        {
            Objects = new Dictionary<string, object>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Enthält die Objekte
        /// </summary>
        protected static Dictionary<string, object> Objects { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Entfernt alle Instanzen
        /// </summary>
        public static void ResetLifeTimeManager()
        {
            foreach (var o in Objects)
            {
                if (o.Value is IDisposable)
                {
                    ((IDisposable)o.Value).Dispose();
                }
            }

            Objects = new Dictionary<string, object>();
        }

        #endregion
    }
}
