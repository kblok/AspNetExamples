using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PageSessionDemo
{
    public class PageSession
    {
        private readonly BasePage _page;

        public PageSession(BasePage page)
        {
            _page = page;

            if (_page != null)
            {
                SetupScripts();
            }
        }

        public object this[string name]
        {
            get => _page.Session[GetFullKey(name)];
            set
            {
                //We create the PageSession list
                if (_page.Session[_page.PageKey + "_SessionList"] == null)
                {
                    _page.Session[_page.PageKey + "_SessionList"] = new List<string>();
                }

                //We add the item to the list
                if (!((List<string>)_page.Session[_page.PageKey + "_SessionList"]).Contains(GetFullKey(name)))
                {
                    ((List<string>)_page.Session[_page.PageKey + "_SessionList"]).Add(GetFullKey(name));
                }

                //We add the item to the Session collection using the preffix
                _page.Session[GetFullKey(name)] = value;
            }
        }

        public void Clear()
        {
            Clear(_page.PageKey);
        }

        public void Clear(string pageKey)
        {
            if (HttpContext.Current.Session[pageKey + "_SessionList"] != null)
            {

                foreach (string item in HttpContext.Current.Session[pageKey + "_SessionList"] as List<string>)
                {
                    if (HttpContext.Current.Session[item] != null)
                    {
                        if (HttpContext.Current.Session[item] is IDisposable)
                        {
                            ((IDisposable)HttpContext.Current.Session[item]).Dispose();
                        }
                        HttpContext.Current.Session[item] = null;
                        HttpContext.Current.Session.Remove(item);
                    }
                }

                HttpContext.Current.Session[pageKey + "_SessionList"] = null;
                HttpContext.Current.Session.Remove(pageKey + "_SessionList");
            }
        }

        public string GetFullKey(string name) => _page.PageKey + name;

        private void SetupScripts()
        {
            _page.ClientScript.RegisterHiddenField("IsBasePagePostBack", "");
            _page.ClientScript.RegisterHiddenField("BasePagePageKey", _page.PageKey);

            _page.ClientScript.RegisterStartupScript(
                GetType(),
                "OnUnloadMethods",
                "window.addEventListener('unload', pageSessionCleanUp); " +
                "function setBasePagePostBack(){{document.getElementById('IsBasePagePostBack').value = '1'; return true;}};", true);

            _page.ClientScript.RegisterOnSubmitStatement(
                GetType(),
                "SafeUnload",
                "setBasePagePostBack();");

            _page.ClientScript.RegisterClientScriptInclude("page-session.js", "page-session.js");
        }
    }
}