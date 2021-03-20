using Energetic.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System
{
    public static class TypeExtensions
    {
        public static IEnumerable<Assembly> GetContainingAssemblies(this IEnumerable<Type> markerTypes)
        {
            return markerTypes.Select(type => type.Assembly).Distinct();
        }

        public static bool HasParameterlessConstructor(this Type type)
        {
            return type.GetConstructor(Type.EmptyTypes) is { };
        }

        public static bool IsConcrete(this Type type)
        {
            return (!type.IsAbstract && !type.IsInterface);
        }

        public static TAttribute GetAttribute<TAttribute>(this Type type, bool throwIfNotFound = true)
            where TAttribute : Attribute
        {
            var attributes = type.GetAttributes<TAttribute>();

            if (attributes.Count() == 0 && throwIfNotFound)
                throw MissingAttributeException.MissingFromClass<TAttribute>(type);

            return attributes.FirstOrDefault();
        }

        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this Type type)
        where TAttribute : Attribute
        {
            return (IEnumerable<TAttribute>)type.GetCustomAttributes(typeof(TAttribute), false);
        }

        public static IEnumerable<Type> GetParentTypes(this Type type)
        {
            if (type == null)
            {
                yield break;
            }

            // return all implemented or inherited interfaces
            foreach (var @interface in type.GetInterfaces())
            {
                yield return @interface;
            }

            // return all inherited types
            var currentBaseType = type.BaseType;
            while (currentBaseType != null)
            {
                yield return currentBaseType;
                currentBaseType = currentBaseType.BaseType;
            }
        }

        public static bool IsAssignableFromOpenGenericType(this Type type, Type interfaceOrAbstractType)
        {
            var interfaceTypes = type.GetInterfaces();

            if (interfaceTypes.Any(it => it.IsGenericType && it.GetGenericTypeDefinition() == interfaceOrAbstractType))
            {
                return true;
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == interfaceOrAbstractType)
                return true;

            Type baseType = type.BaseType;
            if (baseType == null) return false;

            return baseType.IsAssignableFromOpenGenericType(interfaceOrAbstractType);
        }


        public static IEnumerable<Type> GetClosedGenericBaseTypesMatchingOpenGenericType(this Type type, Type openGenericType)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (openGenericType is null)
            {
                throw new ArgumentNullException(nameof(openGenericType));
            }

            if (!openGenericType.IsGenericTypeDefinition)
            {
                throw new ArgumentException($"Type {openGenericType.Name}, passed in {nameof(openGenericType)}, is not an open generic type definition. E.g. such as List<,>.");
            }

            var allBaseTypesAndInterfaces = type.GetParentTypes();

            return allBaseTypesAndInterfaces.Where(t => t.IsGenericType && t.GetGenericTypeDefinition().Equals(openGenericType));
        }
    }
}
