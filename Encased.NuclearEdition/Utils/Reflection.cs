using System;
using System.Reflection;

namespace Encased.NuclearEdition.Utils
{
    public static class Reflection
    {
        public static Type Void { get; } = typeof(void);
        public static Type Boolean { get; } = typeof(Boolean);

        public static MethodInfo GetInstanceMethod(this Type type, Type returnType, String methodName, params Type[] parametersTypes)
        {
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            return GetMethod(bindingFlags, type, returnType, methodName, parametersTypes);
        }

        public static MethodInfo GetStaticMethod(this Type type, Type returnType, String methodName, params Type[] parametersTypes)
        {
            const BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            return GetMethod(bindingFlags, type, returnType, methodName, parametersTypes);
        }

        private static MethodInfo GetMethod(BindingFlags bindingFlags, Type type, Type returnType, String methodName, Type[] parametersTypes)
        {
            foreach (MethodInfo method in type.GetMethods(bindingFlags))
            {
                if (method.Name != methodName)
                    continue;

                if (method.ReturnType != returnType)
                    continue;

                ParameterInfo[] parameters = method.GetParameters();
                if (parameters.Length != parametersTypes.Length)
                    continue;

                for (Int32 i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i].ParameterType != parametersTypes[i])
                        goto next;
                }

                return method;

                next:
                continue;
            }

            throw new ArgumentException($"Cannot find suitable method \"{methodName}\" in the type \"{type}\".", methodName);
        }
    }
}