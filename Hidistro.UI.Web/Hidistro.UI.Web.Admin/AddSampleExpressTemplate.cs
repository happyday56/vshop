using System;
using System.Data;
using System.Text;
using Hidistro.ControlPanel.Store;
using Hidistro.Entities;
using Hidistro.UI.ControlPanel.Utility;

namespace Hidistro.UI.Web.Admin
{
    [PrivilegeCheck(Entities.Store.Privilege.ExpressTemplates)]
    public class AddSampleExpressTemplate : AdminPage
    {
        protected string ems = "";

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //string str = this.Page.Request.QueryString["ExpressName"];
            //string str2 = this.Page.Request.QueryString["XmlFile"];
            //if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(str2) || !str2.EndsWith(".xml"))
            //{
            //    base.GotoResourceNotFound();
            //}
            if (!base.IsPostBack)
            {
                DataTable expressTable = ExpressHelper.GetExpressTable();
                StringBuilder stringBuilder = new StringBuilder();
                foreach (DataRow row in expressTable.Rows)
                {
                    stringBuilder.AppendFormat("<option value='{0}'>{0}</option>", row["Name"]);
                }
                this.ems = stringBuilder.ToString();
            }
            string item = this.Page.Request.QueryString["ExpressName"];
            string str = this.Page.Request.QueryString["XmlFile"];
            if (string.IsNullOrEmpty(item) || string.IsNullOrEmpty(str) || !str.EndsWith(".xml"))
            {
                base.GotoResourceNotFound();
            }
        }
    }
}
