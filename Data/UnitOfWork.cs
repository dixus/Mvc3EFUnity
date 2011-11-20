namespace Kreissl.Showcase.Data
{
    #region Usings

    using System;

    using Kreissl.Showcase.Data.Interfaces;
    using Kreissl.Showcase.Infrastructure;

    #endregion

    /// <summary>
    /// Service für Implementierung des Unit of Work Patterns, welches
    ///     das kummulierte Verarbeiten von DB Sets mit abschließendem,
    ///     einmaligen Commit ermöglicht.
    /// </summary>
    [Service(typeof(IUnitOfWork), ServiceBehaviour.Webrequest)]
    public class UnitOfWork : IUnitOfWork
    {
        #region Constants and Fields

        /// <summary>
        ///     Enthält den Db Context
        /// </summary>
        private readonly IDatabaseFactory _databaseFactory;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// Initialisiert die Unit Of Work
        /// </summary>
        /// <param name="databaseFactory">
        /// Db Factory
        /// </param>
        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this._databaseFactory = databaseFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Übernehmen der Änderungen in die Datenbank
        /// </summary>
        /// <returns>
        /// True wenn commit erfolgreich
        /// </returns>
        public bool Commit()
        {
            try
            {
                this._databaseFactory.GetContext().SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}
