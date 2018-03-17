using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PageAttributesDemo.App_Code
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class AppSettingsAttribute : Attribute
    {
        public string AppSettingKey { get; internal set;}
    
        public AppSettingsAttribute()
        {
        }

        public AppSettingsAttribute(string appSettingKey)
        {
            AppSettingKey = appSettingKey;
        }
  }
}