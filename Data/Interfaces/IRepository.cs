// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepository.cs" company="T-Systems Multimedia Solutions GmbH">
//   Copyright (c) Riesaer Str. 5, 01129 Dresden. All rights reserved.
// </copyright>
// <summary>
//   Defines the IRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Kreissl.Showcase.Data.Interfaces
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    #endregion

    /// <summary>
    /// Zentrales Interface für Repositories
    /// </summary>
    /// <typeparam name="T">
    /// Entity Type
    /// </typeparam>
    public interface IRepository<T>
        where T : class
    {
        #region Public Methods

        /// <summary>
        /// Liest die Anzahl aus
        /// </summary>
        /// <param name="predicate">Das Prädikat</param>
        /// <returns>Anzahl Treffer</returns>
        int Count(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Fügt eine Entität in das DbSet ein
        /// </summary>
        /// <param name="entity">
        /// Die Entität
        /// </param>
        void Add(T entity);

        /// <summary>
        /// Entfernt eine Entität aus dem DbSet
        /// </summary>
        /// <param name="entity">
        /// Die Entität
        /// </param>
        void Delete(T entity);

        /// <summary>
        /// Enternt eine Entität anhand der Id aus dem DbSet
        /// </summary>
        /// <param name="id">
        /// Die Entität
        /// </param>
        void Delete(long id);

        /// <summary>
        /// Enternt Entitäten anhand eines Prädikates aus dem DbSet
        /// </summary>
        /// <param name="predicate">
        /// Das Prädikat
        /// </param>
        void Delete(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Ermittelt eine Entität anhand eines Prädikates
        /// </summary>
        /// <param name="predicate">
        /// Das Abfrageprädikat
        /// </param>
        /// <param name="withTracking">
        /// wenn false, dann kein changetracking. True ist Default
        /// </param>
        /// <returns>
        /// Die Entität
        /// </returns>
        T Get(Expression<Func<T, bool>> predicate, bool withTracking = true);

        /// <summary>
        /// Liefert eine Liste mit allen Entitäten des DbSets
        /// </summary>
        /// <returns>
        /// Liste mit Entitäten
        /// </returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Ermittelt eine Entität anhand der Id
        /// </summary>
        /// <param name="id">
        /// Die Entität Id
        /// </param>
        /// <returns>
        /// Entität mit Id
        /// </returns>
        T GetById(long id);

        /// <summary>
        /// Ermittelt Entitäten anhand eines Prädikates aus dem DbSet
        /// </summary>
        /// <param name="predicate">
        /// Das Abfrageprädikat
        /// </param>
        /// <param name="withTracking">
        /// Für lesende Zugriffe kann hiermit Tracking ausgeschalten werden.
        /// Default ist true also aktiviert.
        /// </param>
        /// <returns>
        /// Liste mit Entitäten
        /// </returns>
        IEnumerable<T> GetMany(Expression<Func<T, bool>> predicate, bool withTracking = true);

        /// <summary>
        /// Setzt den Zustand einer Entität auf Modified
        /// </summary>
        /// <param name="entity">
        /// Die Entität
        /// </param>
        void Update(T entity);
        #endregion
    }
}
