using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PageAttributesDemo.App_Code
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class KeepInViewStateAttribute : Attribute
    {
        public object DefaultValue { get; internal set;}

        public KeepInViewStateAttribute()
        {
        }

        public KeepInViewStateAttribute(object defaultValue)
        {
            DefaultValue = defaultValue;
        }

        public static bool TypeAllowed(Type type) => type.IsSerializable;
    }
}