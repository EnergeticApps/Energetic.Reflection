using System;
using System.Collections.Generic;
using System.Text;

namespace Energetic.Reflection
{
    public class MissingAttributeException : InvalidOperationException
    {
        public static MissingAttributeException MissingFromAssembly<TAttribute>(string assemblyName)
            where TAttribute : Attribute
        {
            return new MissingAttributeException($"An attribute of type {typeof(TAttribute).FullName} was expected on assembly {assemblyName} but was not found.");
        }

        public static MissingAttributeException MissingFromClass<TAttribute>(Type @class)
        {
            return new MissingAttributeException($"An attribute of type {typeof(TAttribute).FullName} was expected on class {@class.FullName} but was not found.");
        }

        public static MissingAttributeException MissingFromMethod<TAttribute>(string methodName, Type @class)
        {
            return new MissingAttributeException($"An attribute of type {typeof(TAttribute).FullName} was expected on the {methodName} method of the {@class.FullName} class but was not found.");
        }

        public static MissingAttributeException MissingFromField<TAttribute>(string fieldName, Type @class)
        {
            return new MissingAttributeException($"An attribute of type {typeof(TAttribute).FullName} was expected on the {fieldName} field of the {@class.FullName} class but was not found.");
        }

        private MissingAttributeException(string message) : base(message)
        {

        }
    }
}
