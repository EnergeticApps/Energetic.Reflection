using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetConcreteTypes(this Assembly assembly)
        {
            return assembly.GetExportedTypes()
                .Where(type => type.IsConcrete());
        }

        public static IEnumerable<Type> GetConcreteTypes(this Assembly assembly, Type typeToImplementOrInherit)
        {
            return assembly.GetExportedTypes(typeToImplementOrInherit)
            .Where(type => type.IsConcrete());
        }

        public static IEnumerable<Type> GetConcreteTypes(this IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(assembly => assembly.GetConcreteTypes()).Distinct();
        }

        public static IEnumerable<Type> GetConcreteTypes(this IEnumerable<Assembly> assemblies, Type typeToImplementOrInherit)
        {
            return assemblies.SelectMany(assembly => assembly.GetConcreteTypes(typeToImplementOrInherit));
        }

        public static IEnumerable<Type> GetExportedTypes(this IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(assembly => assembly.GetExportedTypes());
        }

        public static IEnumerable<Type> GetExportedTypes(this IEnumerable<Assembly> assemblies, Type typeToImplementOrInherit)
        {
            return assemblies.SelectMany(assembly => assembly.GetExportedTypes(typeToImplementOrInherit));
        }

        public static IEnumerable<Type> GetExportedTypes(this Assembly assembly, Type typeToImplementOrInherit)
        {
            return assembly.GetExportedTypes()
                .Where(type => typeToImplementOrInherit.IsAssignableFrom(type));
        }
    }
}
