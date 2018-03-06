using System;
using System.Web.Services;
using System.Web.UI;

namespace PageSessionDemo
{
    public class BasePage : Page
    {
        public string PageKey
        {
            get => ViewState["PageInstanceUID"]?.ToString();
            internal set => ViewState["PageInstanceUID"] = value;
        }

        public PageSession PageSession { get; internal set; }

        public BasePage()
        {
            PreLoad += BasePage_PreLoad;
        }

        private void BasePage_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack && string.IsNullOrEmpty(PageKey))
            {
                PageKey = Guid.NewGuid().ToString();
            }
            
            PageSession = new PageSession(this);
        }
        
        [WebMethod(true)]
        public static void CleanUpPageSession(string pageKey) => new PageSession(null).Clear(pageKey);


    }
}