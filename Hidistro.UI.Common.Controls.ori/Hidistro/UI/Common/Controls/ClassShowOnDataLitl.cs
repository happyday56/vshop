namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using Hidistro.Core;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class ClassShowOnDataLitl : Literal
    {
        protected override void Render(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(base.Text))
            {
                base.Text = string.Format(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABzAHAAYQBuAD4AewAwAH0APAAvAHMAcABhAG4APgA="), this.DefaultText);
            }
            else
            {
                base.Text = string.Format(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABzAHAAYQBuACAAcwB0AHkAbABlAD0AIgB7ADAAfQAiACAAYwBsAGEAcwBzAD0AIgB7ADEAfQAiAD4AewAyAH0APAAvAHMAcABhAG4APgA="), this.Style, this.Class, base.Text);
                if (this.IsShowLink)
                {
                    base.Text = string.Format(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABhACAAaAByAGUAZgA9ACIAewAwAH0AIgA+AHsAMQB9ADwALwBhAD4A"), this.Link, base.Text);
                }
            }
            base.Render(writer);
        }

        public string Class
        {
            get
            {
                if (this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("QwBsAGEAcwBzAA==")] == null)
                {
                    return string.Empty;
                }
                return (string) this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("QwBsAGEAcwBzAA==")];
            }
            set
            {
                this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("QwBsAGEAcwBzAA==")] = value;
            }
        }

        public string DefaultText
        {
            get
            {
                if (this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("RABlAGYAYQB1AGwAdABUAGUAeAB0AA==")] == null)
                {
                    return string.Empty;
                }
                return (string) this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("RABlAGYAYQB1AGwAdABUAGUAeAB0AA==")];
            }
            set
            {
                this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("RABlAGYAYQB1AGwAdABUAGUAeAB0AA==")] = value;
            }
        }

        public bool IsShowLink
        {
            get
            {
                if (this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("SQBzAFMAaABvAHcATABpAG4AawA=")] == null)
                {
                    return false;
                }
                return (bool) this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("SQBzAFMAaABvAHcATABpAG4AawA=")];
            }
            set
            {
                this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("SQBzAFMAaABvAHcATABpAG4AawA=")] = value;
            }
        }

        public string Link
        {
            get
            {
                if (this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TABpAG4AawA=")] == null)
                {
                    return string.Empty;
                }
                return (Globals.GetSiteUrls().UrlData.FormatUrl((string) this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TABpAG4AawA=")]) + this.LinkQuery);
            }
            set
            {
                this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TABpAG4AawA=")] = value;
            }
        }

        public string LinkQuery
        {
            get
            {
                if (this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TABpAG4AawBRAHUAZQByAHkA")] == null)
                {
                    return string.Empty;
                }
                return (string) this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TABpAG4AawBRAHUAZQByAHkA")];
            }
            set
            {
                this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TABpAG4AawBRAHUAZQByAHkA")] = value;
            }
        }

        public string Style
        {
            get
            {
                if (this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("UwB0AHkAbABlAA==")] == null)
                {
                    return string.Empty;
                }
                return (string) this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("UwB0AHkAbABlAA==")];
            }
            set
            {
                this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("UwB0AHkAbABlAA==")] = value;
            }
        }
    }
}

