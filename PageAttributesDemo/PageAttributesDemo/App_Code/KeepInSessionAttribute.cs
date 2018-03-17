using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PageAttributesDemo.App_Code
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class KeepInSessionAttribute : Attribute
    {
        public string SessionKey { get; internal set;}

        public KeepInSessionAttribute()
        {
        }

        public KeepInSessionAttribute(string sessionKey)
        {
            SessionKey = sessionKey;
        }

        public static bool TypeAllowed(Type type) => type.IsSerializable;
    }
}