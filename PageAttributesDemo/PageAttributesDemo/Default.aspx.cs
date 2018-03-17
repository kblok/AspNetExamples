using PageAttributesDemo.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PageAttributesDemo
{
    public partial class Default : BasePage
    {
        [QueryString("userID")]
        int _userId;
        [AppSettings("someValue")]
        int _someConfig;
        [KeepInSession]
        int _someSessionInt;
        [KeepInViewState]
        int _someViewStateInt;


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            fromSession.Text = _someSessionInt.ToString();
            fromViewState.Text = _someViewStateInt.ToString();
            fromQueryString.Text = _userId.ToString();
            fromAppSettings.Text = _someConfig.ToString();
        }

        protected void incrementValueFromSession_Click(object sender, EventArgs e)
        {
            _someSessionInt++;
        }

        protected void incrementValueFromViewState_Click(object sender, EventArgs e)
        {
            _someViewStateInt++;
        }
    }
}