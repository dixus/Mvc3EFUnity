namespace Kreissl.Showcase.Repository
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    using Kreissl.Showcase.Data.Interfaces;

    #endregion

    /// <summary>
    /// Generisches Repository für das Entity Framework. Die abstrakte
    ///     Klasse wird von einer Entität implementiert.
    /// </summary>
    /// <typeparam name="TEntity">
    /// CodeFirstEntity Typ
    /// </typeparam>
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        #region Constants and Fields

        /// <summary>
        /// Das Db Set
        /// </summary>
        private readonly IDbSet<TEntity> _dbset;

        /// <summary>
        /// Der Db Context
        /// </summary>
        private DbContext _dataContext;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="databaseFactory">
        /// The database factory.
        /// </param>
        protected Repository(IDatabaseFactory databaseFactory)
        {
            this.DatabaseFactory = databaseFactory;
            this._dbset = this.DataContext.Set<TEntity>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Der Db Context
        /// </summary>
        protected DbContext DataContext
        {
            get
            {
                return this._dataContext ?? (this._dataContext = this.DatabaseFactory.GetContext());
            }
        }

        /// <summary>
        ///     Die DB Factory
        /// </summary>
        protected IDatabaseFactory DatabaseFactory { get; private set; }

        /// <summary>
        ///     Das Db Set
        /// </summary>
        protected IDbSet<TEntity> DbSet
        {
            get
            {
                return this._dbset;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Fügt eine Entität zum Db Context hinzu
        /// </summary>
        /// <param name="entity">
        /// neue Entität
        /// </param>
        public virtual void Add(TEntity entity)
        {
            this._dbset.Add(entity);
        }

        /// <summary>
        /// Entfernt eine Entität aus dem Db Set
        /// </summary>
        /// <param name="entity">
        /// zu löschende Entität
        /// </param>
        public virtual void Delete(TEntity entity)
        {
            this._dbset.Remove(entity);
        }

        /// <summary>
        /// Löscht eine Entität mit der Id
        /// </summary>
        /// <param name="id">
        /// Id der Entität
        /// </param>
        public virtual void Delete(long id)
        {
            var entity = this._dbset.Find(id);
            if (entity != null)
            {
                this._dbset.Remove(entity);
            }
        }

        /// <summary>
        /// Löscht aus dem Db Set
        /// </summary>
        /// <param name="predicate">
        /// Prädikat als Expression
        /// </param>
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> objects = this._dbset.Where(predicate).AsEnumerable();
            foreach (TEntity obj in objects)
            {
                this._dbset.Remove(obj);
            }
        }

        /// <summary>
        /// Ermittelt eine Entität
        /// </summary>
        /// <param name="predicate">
        /// Das Prädikat als Expression
        /// </param>
        /// <param name="withTracking">
        /// wenn false, dann kein changetracking. True ist Default
        /// </param>
        /// <returns>
        /// Ergebnis der Abfrage
        /// </returns>
        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate, bool withTracking = true)
        {
            if (withTracking)
                return this._dbset.FirstOrDefault(predicate);

            return this._dbset.Where(predicate).AsNoTracking().FirstOrDefault();
        }

        /// <summary>
        /// Liest die Anzahl aus
        /// </summary>
        /// <param name="predicate">Das Prädikat</param>
        /// <returns>Anzahl Treffer</returns>
        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return this._dbset.Count(predicate);
        }

        /// <summary>
        /// Liefert alle Entitäten
        /// </summary>
        /// <returns>
        /// Liste mit Entitäten
        /// </returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return this._dbset.AsEnumerable();
        }

        /// <summary>
        /// Liefert Entität mit dieser id
        /// </summary>
        /// <param name="id">
        /// Id einer Entität
        /// </param>
        /// <returns>
        /// Das Objekt
        /// </returns>
        public virtual TEntity GetById(long id)
        {
            try
            {
                return this._dbset.Find(id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Gibt Liste zurück
        /// </summary>
        /// <param name="predicate">
        /// Suchprädikat als Expression
        /// </param>
        /// <param name="withTracking">
        /// Für lesende Zugriffe kann hiermit Tracking ausgeschalten werden.
        /// Default ist true also aktiviert.
        /// </param>
        /// <returns>
        /// Liste mit Entitäten
        /// </returns>
        public virtual IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate, bool withTracking = true)
        {
            if (withTracking)
                return this._dbset.Where(predicate);

            return this._dbset.Where(predicate).AsNoTracking();
        }


        /// <summary>
        /// Setzt den Zustand einer Entität auf Modified
        /// </summary>
        /// <param name="entity">
        /// Die Entität
        /// </param>
        public virtual void Update(TEntity entity)
        {
            this._dataContext.Entry(entity).State = EntityState.Modified;
        }


        #endregion

    }
}
