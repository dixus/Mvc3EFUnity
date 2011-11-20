// <summary>
//   loads all assemblies
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Kreissl.Showcase.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public static class AssemblyLocator
    {
        /// <summary>
        /// loads all assemblies into membory
        /// </summary>
        /// <returns>List of assemblies</returns>
        public static IEnumerable<Assembly> LoadAll()
        {
            string binFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
            if (!Directory.Exists(binFolder))
                binFolder = AppDomain.CurrentDomain.BaseDirectory;

            if (Directory.Exists(binFolder))
            {
                IList<string> dllFiles = Directory.GetFiles(binFolder, "Kreissl.*.dll", SearchOption.TopDirectoryOnly).ToList();

                foreach (string dllFile in dllFiles)
                {
                    AssemblyName assemblyName = AssemblyName.GetAssemblyName(dllFile);
                    yield return Assembly.Load(assemblyName);
                }
            }
        }
    }
}
