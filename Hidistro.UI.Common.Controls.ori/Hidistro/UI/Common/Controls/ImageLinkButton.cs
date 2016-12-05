namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using System;
    using System.Globalization;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class ImageLinkButton : LinkButton
    {
        private string AAZ0JeEma(2x58C7QQ)d9L4MH = "<img border=\"0\" src=\"{0}\" alt=\"{1}\" />";
        private Hidistro.UI.Common.Controls.ImagePosition ABOZyE4bfGcbd5Z8N5SO7b2;
        private bool AC2pFbvHCa1v4C1DIZ8SngolueQb;
        private string AD)2Z(NKy = "确定要执行该删除操作吗？删除后将不可以恢复！";
        private string AE)AZJK0MajbU;
        private bool AFCkJKk = true;

        private string AAZ0JeEma(2x58C7QQ)d9L4MH()
        {
            if (string.IsNullOrEmpty(this.ImageUrl))
            {
                return string.Empty;
            }
            return string.Format(CultureInfo.InvariantCulture, this.AAZ0JeEma(2x58C7QQ)d9L4MH, new object[] { this.ImageUrl, this.Alt });
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.IsShow)
            {
                string str = string.Format(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cgBlAHQAdQByAG4AIAAgACAAYwBvAG4AZgBpAHIAbQAoACcAewAwAH0AJwApADsA"), this.DeleteMsg);
                base.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TwBuAEMAbABpAGMAawA="), str);
            }
            base.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("bgBhAG0AZQA="), this.NamingContainer.UniqueID + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("JAA=") + this.ID);
            string str2 = this.AAZ0JeEma(2x58C7QQ)d9L4MH();
            if (!this.ShowText)
            {
                base.Text = "";
            }
            if (this.ImagePosition == Hidistro.UI.Common.Controls.ImagePosition.Right)
            {
                base.Text = base.Text + str2;
            }
            else
            {
                base.Text = str2 + base.Text;
            }
            base.Render(writer);
        }

        public string Alt
        {
            get
            {
                return this.AE)AZJK0MajbU;
            }
            set
            {
                this.AE)AZJK0MajbU = value;
            }
        }

        public string DeleteMsg
        {
            get
            {
                return this.AD)2Z(NKy;
            }
            set
            {
                this.AD)2Z(NKy = value;
            }
        }

        public Hidistro.UI.Common.Controls.ImagePosition ImagePosition
        {
            get
            {
                return this.ABOZyE4bfGcbd5Z8N5SO7b2;
            }
            set
            {
                this.ABOZyE4bfGcbd5Z8N5SO7b2 = value;
            }
        }

        public string ImageUrl
        {
            get
            {
                if (this.ViewState[A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("UwByAGMA")] != null)
                {
                    return (string) this.ViewState[A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("UwByAGMA")];
                }
                return null;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    string relativeUrl = value;
                    if (relativeUrl.StartsWith(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("fgA=")))
                    {
                        relativeUrl = base.ResolveUrl(relativeUrl);
                    }
                    this.ViewState[A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("UwByAGMA")] = relativeUrl;
                }
                else
                {
                    this.ViewState[A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("UwByAGMA")] = null;
                }
            }
        }

        public bool IsShow
        {
            get
            {
                return this.AC2pFbvHCa1v4C1DIZ8SngolueQb;
            }
            set
            {
                this.AC2pFbvHCa1v4C1DIZ8SngolueQb = value;
            }
        }

        public bool ShowText
        {
            get
            {
                return this.AFCkJKk;
            }
            set
            {
                this.AFCkJKk = value;
            }
        }
    }
}

