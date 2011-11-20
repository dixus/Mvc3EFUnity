namespace Kreissl.Showcase.Data
{
    #region Usings

    using System;
    using System.Data.Entity;
    using System.Diagnostics;

    using Kreissl.Showcase.Data.Interfaces;

    using Kreissl.Showcase.Infrastructure;

    #endregion

    /// <summary>
    ///   Factory Klasse zur Instanzierung des DB Kontexts
    /// </summary>
    //[Service(typeof(IDatabaseFactory), ServiceBehaviour.NewInstance, ServiceScope.Backend)]
    [Service(typeof(IDatabaseFactory), ServiceBehaviour.Webrequest)]
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        #region Constants and Fields

        /// <summary>
        ///   Daten Kontext
        /// </summary>
        private BookContext _dataContext;

        #endregion


        #region Public Methods

        /// <summary>
        ///   Löscht die Datenbank
        /// </summary>
        public void CleanUpDatabase()
        {
            try
            {
                using (var dataContext = new BookContext())
                {
                    dataContext.Database.Delete();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        /// <summary>
        ///   Gibt den Context zurück. Sofern noch nicht Initialisiert,
        ///   wird dieser erstellt.
        /// </summary>
        /// <returns>
        ///   Db Context
        /// </returns>
        public DbContext GetContext()
        {
            return this._dataContext ?? (this._dataContext = new BookContext());
        }

        /// <summary>
        ///   Initialisiert den Context neu
        /// </summary>
        /// <returns>Initialisierter Db Context</returns>
        public DbContext ReinitializeContext()
        {
            if (this._dataContext != null)
            {
                this._dataContext.Dispose();
            }

            this._dataContext = null;

            return this.GetContext();
        }

        #endregion

        #region Methods

        /// <summary>
        ///   Dispose des Db Contexts
        /// </summary>
        protected override void DisposeCore()
        {
            if (this._dataContext != null)
            {
                this._dataContext.Dispose();
            }
        }

        #endregion
    }
}
