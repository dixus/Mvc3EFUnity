namespace Kreissl.Showcase.Infrastructure
{
    /// <summary>
    /// Definert den Lebenszyklus des Service
    /// </summary>
public enum ServiceBehaviour
{
    /// <summary>
    /// Transient bzw. immer neue Instanz
    /// </summary>
    NewInstance,

    /// <summary>
    /// Container Controlled LifetimeManager
    /// </summary>
    Singleton,

    /// <summary>
    /// Request Based
    /// </summary>
    Webrequest
}
}
