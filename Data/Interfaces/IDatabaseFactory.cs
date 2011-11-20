namespace Kreissl.Showcase.Data.Interfaces
{
    #region Usings

    using System;
    using System.Data.Entity;

    #endregion

    /// <summary>
    /// Factory that holds the dbcontext instance
    /// </summary>
    public interface IDatabaseFactory : IDisposable
    {
        #region Public Methods

        /// <summary>
        /// Initialisiert den Context neu
        /// </summary>
        /// <returns>Initialisierter Db Context</returns>
        DbContext ReinitializeContext();

        /// <summary>
        /// Löscht die Datenbank
        /// </summary>
        void CleanUpDatabase();

        /// <summary>
        /// Gibt den Context zurück. Sofern noch nicht Initialisiert,
        ///     wird dieser erstellt.
        /// </summary>
        /// <returns>
        /// Db Context
        /// </returns>
        DbContext GetContext();
        #endregion
    }
}
