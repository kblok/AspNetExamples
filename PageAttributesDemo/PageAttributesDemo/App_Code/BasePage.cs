using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;

namespace PageAttributesDemo.App_Code
{
    public class BasePage : Page
    {
        public BasePage()
        {
            PreLoad += BasePage_PreLoad;
            PreRenderComplete += BasePage_PreRenderComplete;
        }

        private void BasePage_PreLoad(object sender, EventArgs e)
        {
            EvalFieldsOnPreLoad(this);

            var master = Master;
            while (master != null)
            {
                EvalFieldsOnPreLoad(master);
                master = master.Master;
            }
        }

        private void BasePage_PreRenderComplete(object sender, EventArgs e)
        {
            EvalFieldsOnPreRenderComplete(this);

            var master = Master;
            while (master != null)
            {
                EvalFieldsOnPreRenderComplete(master);
                master = master.Master;
            }
        }

        private void EvalFieldsOnPreLoad(TemplateControl instance)
        {
            var type = instance.GetType();

            while (!(type == typeof(Page) || type == typeof(UserControl)))
            {
                foreach (var field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    EvalQueryStringAttributeSet(instance, field);
                    EvalAppSettingsAttributeSet(instance, field);
                    EvalKeepInSessionAttributeSet(instance, field);
                    EvalKeepInViewStateAttributeSet(instance, field);
                }

                type = type.BaseType;
            }
        }

        private void EvalFieldsOnPreRenderComplete(TemplateControl instance)
        {
            var type = instance.GetType();

            while (!(type == typeof(Page) || type == typeof(UserControl)))
            {
                foreach (var field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    EvalKeepInSessionAttributeStore(instance, field);
                    EvalKeepInViewStateAttributeStore(instance, field);
                }

                type = type.BaseType;
            }
        }

        private void EvalKeepInViewStateAttributeStore(TemplateControl instance, FieldInfo field)
        {
            var keepInViewStateAttribute = GetAttributeFromField<KeepInViewStateAttribute>(field);

            if (keepInViewStateAttribute != null)
            {
                ViewState[field.Name + "AutoSave"] = field.GetValue(instance).ToString();
            }
        }

        private void EvalKeepInSessionAttributeStore(TemplateControl instance, FieldInfo field)
        {
            var keepInSessionAttribute = GetAttributeFromField<KeepInSessionAttribute>(field);

            if (keepInSessionAttribute != null)
            {
                if (string.IsNullOrEmpty(keepInSessionAttribute.SessionKey))
                {
                    Session[field.Name + "AutoSave"] = field.GetValue(instance);
                }
                else
                {
                    Session[Request.QueryString[keepInSessionAttribute.SessionKey]] = field.GetValue(instance);
                }
            }
        }

        private void EvalKeepInViewStateAttributeSet(TemplateControl instance, FieldInfo field)
        {
            var keepInViewStateAttribute = GetAttributeFromField<KeepInViewStateAttribute>(field);

            if (keepInViewStateAttribute != null)
            {
                if (!KeepInSessionAttribute.TypeAllowed(field.FieldType))
                {
                    throw new InvalidTypeException("KeepInsession", field);
                }

                if (ViewState[field.Name + "AutoSave"] != null)
                {
                    field.SetValue(instance, Convert.ChangeType(ViewState[field.Name + "AutoSave"].ToString(), field.FieldType));
                }
                else
                {
                    field.SetValue(instance, keepInViewStateAttribute.DefaultValue);
                }

            }
        }

        private void EvalKeepInSessionAttributeSet(TemplateControl instance, FieldInfo field)
        {
            var keepInSessionAttribute = GetAttributeFromField<KeepInSessionAttribute>(field);

            if (keepInSessionAttribute != null)
            {

                if (!KeepInSessionAttribute.TypeAllowed(field.FieldType))
                {
                    throw new InvalidTypeException("KeepInsession", field);
                }

                if (string.IsNullOrEmpty(keepInSessionAttribute.SessionKey))
                {
                    if (!(field.GetValue(instance) != null && Session[field.Name + "AutoSave"] == null))
                    {
                        field.SetValue(instance, Session[field.Name + "AutoSave"]);
                    }
                }
                else
                {
                    if (!(field.GetValue(instance) != null && Session[Request.QueryString[keepInSessionAttribute.SessionKey]] == null))
                    {
                        field.SetValue(instance, Session[Request.QueryString[keepInSessionAttribute.SessionKey]]);
                    }
                }
            }
        }

        private static void EvalAppSettingsAttributeSet(TemplateControl instance, FieldInfo field)
        {
            var appSettingsAttribute = GetAttributeFromField<AppSettingsAttribute>(field);

            if (appSettingsAttribute != null)
            {
                if (string.IsNullOrEmpty(appSettingsAttribute.AppSettingKey))
                {
                    field.SetValue(instance, Convert.ChangeType(ConfigurationManager.AppSettings[field.Name], field.FieldType));
                }
                else
                {
                    field.SetValue(instance, Convert.ChangeType(ConfigurationManager.AppSettings[appSettingsAttribute.AppSettingKey], field.FieldType));
                }
            }
        }

        private void EvalQueryStringAttributeSet(TemplateControl instance, FieldInfo field)
        {
            var queryStringAttribute = GetAttributeFromField<QueryStringAttribute>(field);

            if (queryStringAttribute != null)
            {

                if (string.IsNullOrEmpty(queryStringAttribute.QueryStringKey))
                {
                    field.SetValue(instance, Convert.ChangeType(Request.QueryString[field.Name], field.FieldType));
                }
                else if(!string.IsNullOrEmpty(Request.QueryString[queryStringAttribute.QueryStringKey]))
                {
                    field.SetValue(instance, Convert.ChangeType(Request.QueryString[queryStringAttribute.QueryStringKey], field.FieldType));
                }
            }
        }

        private static T GetAttributeFromField<T>(ICustomAttributeProvider field) where T : Attribute =>
            Array.Find(field.GetCustomAttributes(true), a => a is T) as T;
        
    }
}