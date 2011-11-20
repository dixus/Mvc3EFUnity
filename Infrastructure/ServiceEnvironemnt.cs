namespace Kreissl.Showcase.Infrastructure
{
    /// <summary>
    /// Service Environemnt
    /// </summary>
    public enum ServiceEnvironemnt
    {
        RemoteOnly = 1,

        LocalOnly = 2,

        Anywhere = RemoteOnly | LocalOnly
    }
}
