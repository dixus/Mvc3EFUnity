// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="T-Systems Multimedia Solutions GmbH">
//   Copyright (c) Riesaer Str. 5, 01129 Dresden. All rights reserved.
// </copyright>
// <summary>
//   Defines the IUnitOfWork type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Kreissl.Showcase.Data.Interfaces
{
    /// <summary>
    /// Service für Implementierung des Unit of Work Patterns
    /// </summary>
    public interface IUnitOfWork
    {
        #region Public Methods

        /// <summary>
        /// Übernehmen der Änderungen in die Datenbank
        /// </summary>
        /// <returns>
        /// True wenn commit erfolgreich
        /// </returns>
        bool Commit();
        #endregion
    }
}
