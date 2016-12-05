namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using ASPNET.WebControls;
    using Hidistro.Core;
    using System;
    using System.Web.UI.WebControls;

    public class VshopTemplatedRepeater : Repeater
    {
        private string AAZ0JeEma(2x58C7QQ)d9L4MH = string.Empty;

        protected override void CreateChildControls()
        {
            if ((this.ItemTemplate == null) && !string.IsNullOrEmpty(this.TemplateFile))
            {
                this.ItemTemplate = this.Page.LoadTemplate(this.TemplateFile);
            }
        }

        public string TemplateFile
        {
            get
            {
                if (!string.IsNullOrEmpty(this.AAZ0JeEma(2x58C7QQ)d9L4MH) && !Utils.IsUrlAbsolute(this.AAZ0JeEma(2x58C7QQ)d9L4MH.ToLower()))
                {
                    return (Utils.ApplicationPath + this.AAZ0JeEma(2x58C7QQ)d9L4MH);
                }
                return this.AAZ0JeEma(2x58C7QQ)d9L4MH;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (value.StartsWith(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwA=")))
                    {
                        this.AAZ0JeEma(2x58C7QQ)d9L4MH = Globals.GetVshopSkinPath(null) + value;
                    }
                    else
                    {
                        this.AAZ0JeEma(2x58C7QQ)d9L4MH = Globals.GetVshopSkinPath(null) + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwA=") + value;
                    }
                }
                if (!this.AAZ0JeEma(2x58C7QQ)d9L4MH.StartsWith(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB0AGUAbQBwAGwAYQB0AGUAcwA=")))
                {
                    this.AAZ0JeEma(2x58C7QQ)d9L4MH = this.AAZ0JeEma(2x58C7QQ)d9L4MH.Substring(this.AAZ0JeEma(2x58C7QQ)d9L4MH.IndexOf(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB0AGUAbQBwAGwAYQB0AGUAcwA=")));
                }
            }
        }
    }
}

