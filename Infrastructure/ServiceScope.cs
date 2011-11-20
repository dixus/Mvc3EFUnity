namespace Kreissl.Showcase.Infrastructure
{
    using System;

    /// <summary>
    /// Service Scope Enumeration
    /// </summary>
    [Flags]
    public enum ServiceScope
    {
        /// <summary>
        /// Service wird im Frontend hinzugef�gt
        /// </summary>
        Frontend = 1,

        /// <summary>
        /// Service wird im Backend hinzugef�gt
        /// </summary>
        Backend = 2,

        /// <summary>
        /// Service wird Webservice hinzugef�gt
        /// </summary>
        Webservice = 4,

        /// <summary>
        /// Service wird Frontend und Webservice hinzugef�gt
        /// </summary>
        Web = Frontend | Webservice,

        /// <summary>
        /// Service wird �berall erstellt
        /// </summary>
        Everywhere = Frontend | Backend | Webservice
    }
}
