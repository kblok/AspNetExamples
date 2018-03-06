using System;

namespace PageSessionDemo
{
    public partial class Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageSession["increment"] = 0;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            fullSessionList.Text = string.Empty;

            foreach(string key in Session.Keys)
            {
                fullSessionList.Text += $"{key}: {Session[key]}<br/>";
            }

            sessionValue.Text = PageSession["increment"].ToString();
        }

        protected void incrementLink_Click(object sender, EventArgs e)
        {
            PageSession["increment"] = (int)PageSession["increment"] + 1;
        }
    }
}