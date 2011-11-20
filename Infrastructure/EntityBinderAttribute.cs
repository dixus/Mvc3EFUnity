// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityBinderAttribute.cs" company="T-Systems Multimedia Solutions GmbH">
//   Copyright (c) Riesaer Str. 5, 01129 Dresden. All rights reserved.
// </copyright>
// <summary>
//   The dump attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Kreissl.Showcase.Infrastructure
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// Entity Binder Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class EntityBinderAttribute : Attribute
    {
        #region Constructors and Destructors

        /// <summary>
        /// Gibt alle Klassentypen zurück, die das Attribut besitzen
        /// </summary>
        /// <returns>Liste mit Typen</returns>
        public static IEnumerable<Type> GetTypes()
        {
            var assemblies =
                AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("De.Mms.StromBoxx"));

            foreach (var assembly in assemblies)
            {
                var types = from type in assembly.GetTypes()
                            where type.IsClass && type.GetCustomAttributes(typeof(EntityBinderAttribute), false).Any()
                            select type;

                foreach (var type in types)
                {
                    yield return type;
                }
            }
        }

        #endregion
    }
}
