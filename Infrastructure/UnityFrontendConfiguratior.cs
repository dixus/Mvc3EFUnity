namespace Kreissl.Showcase.Infrastructure
{
    using Microsoft.Practices.Unity;

    /// <summary>
    /// Unity Konfiguration für das Webfrontend
    /// </summary>
    public class UnityFrontendConfiguratior : UnityInitializerBase
    {
        /// <summary>
        /// Gibt den Container zurück
        /// </summary>
        public static IUnityContainer Container
        {
            get
            {
                return GetContainer(ServiceScope.Frontend);
            }
        }
    }
}
