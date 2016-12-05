using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.UI.ControlPanel.Utility;

namespace Hidistro.UI.Web.Admin.distributor
{
    public partial class DistributorLogo : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.Click += new EventHandler(this.btnSave_Click);
            if (!base.IsPostBack)
            {
                SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
                this.hidpic.Value = masterSettings.DistributorLogoPic;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);

            masterSettings.DistributorLogoPic = this.hidpic.Value;

            SettingsManager.Save(masterSettings);

            if (!string.IsNullOrEmpty(this.hidpicdel.Value))
            {
                string[] strArrays = this.hidpicdel.Value.Split(new char[] { '|' });

                for (int i = 0; i < (int)strArrays.Length; i++)
                {
                    string str = strArrays[i];
                    str = base.Server.MapPath(str);
                    if (File.Exists(str))
                    {
                        File.Delete(str);
                    }
                }

            }

            this.hidpicdel.Value = "";
            this.ShowMsg("修改成功", true);
        }


    }
}