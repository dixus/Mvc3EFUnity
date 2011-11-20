namespace Kreissl.Showcase.Data
{
    #region Usings

    using System;

    #endregion

    /// <summary>
    /// Standard Dispose Implementierung
    /// </summary>
    public class Disposable : IDisposable
    {
        #region Constants and Fields

        /// <summary>
        /// true wenn disposed
        /// </summary>
        private bool _isDisposed;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Finalizes an instance of the <see cref="Disposable"/> class.
        /// </summary>
        ~Disposable()
        {
            this.Dispose(false);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Dispose zum überschreiben
        /// </summary>
        protected virtual void DisposeCore()
        {
        }

        /// <summary>
        /// Ruft DisposeCore auf
        /// </summary>
        /// <param name="disposing">
        /// true wenn disposing
        /// </param>
        private void Dispose(bool disposing)
        {
            if (!this._isDisposed && disposing)
            {
                this.DisposeCore();
            }

            this._isDisposed = true;
        }

        #endregion
    }
}
