using System;
using System.Linq;
using JetBrains.Annotations;

namespace Utilities
{
    public static class TypeExtensions
    {
        public static bool IsTypeInheritsGenericInterface(this Type type, Type genericInterface)
        {
            return GetParentGenericInterfaceType(type, genericInterface) != default;
        }

        [CanBeNull]
        public static Type GetGenericInterfaceArgumentType(this Type type, Type genericInterface)
        {
            var interfaceType = GetParentGenericInterfaceType(type, genericInterface);
            return interfaceType?.GetGenericArguments()[0];
        }

        [CanBeNull]
        public static Type GetParentGenericInterfaceType(this Type type, Type genericInterface)
        {
            return type
                .GetInterfaces()
                .FirstOrDefault(AreGenericEquals);

            bool AreGenericEquals(Type t)
            {
                return t.IsGenericType &&
                       t.GetGenericTypeDefinition() == genericInterface;
            }
        }
    }
}
