using PageAttributesDemo.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PageAttributesDemo
{
    public partial class Master : System.Web.UI.MasterPage
    {
        [QueryString("userId")]
        int _userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            fromQueryString.Text = _userId.ToString();
        }
    }
}