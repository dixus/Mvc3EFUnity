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
        /// Service wird im Frontend hinzugefügt
        /// </summary>
        Frontend = 1,

        /// <summary>
        /// Service wird im Backend hinzugefügt
        /// </summary>
        Backend = 2,

        /// <summary>
        /// Service wird Webservice hinzugefügt
        /// </summary>
        Webservice = 4,

        /// <summary>
        /// Service wird Frontend und Webservice hinzugefügt
        /// </summary>
        Web = Frontend | Webservice,

        /// <summary>
        /// Service wird überall erstellt
        /// </summary>
        Everywhere = Frontend | Backend | Webservice
    }
}
