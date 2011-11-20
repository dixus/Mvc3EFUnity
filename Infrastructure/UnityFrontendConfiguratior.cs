namespace Kreissl.Showcase.Infrastructure
{
    using Microsoft.Practices.Unity;

    /// <summary>
    /// Unity Konfiguration f�r das Webfrontend
    /// </summary>
    public class UnityFrontendConfiguratior : UnityInitializerBase
    {
        /// <summary>
        /// Gibt den Container zur�ck
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
