using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PageAttributesDemo.App_Code
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class QueryStringAttribute : Attribute
    {
        public string QueryStringKey { get; internal set;}
    
        public QueryStringAttribute()
        {
        }

        public QueryStringAttribute(string queryStringKey)
        {
          QueryStringKey = queryStringKey;
        }
  }
}