using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PageAttributesDemo.App_Code
{
    public class InvalidTypeException : Exception
    {
        public InvalidTypeException(string attribute, FieldInfo field)
            : base($"You cannot use the attribute {attribute} with a variable of type {field.FieldType.Name} (field : {field.Name})")
        {
        }
    }
}