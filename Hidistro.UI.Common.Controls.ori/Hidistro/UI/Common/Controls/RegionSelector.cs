namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using Hidistro.Entities;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;

    public class RegionSelector : WebControl
    {
        private int? AAZ0JeEma(2x58C7QQ)d9L4MH;
        private bool ABOZyE4bfGcbd5Z8N5SO7b2;
        private WebControl AC2pFbvHCa1v4C1DIZ8SngolueQb;
        private WebControl AD)2Z(NKy;
        private WebControl AE)AZJK0MajbU;
        private int? AFCkJKk;
        private int? AGeIcrrVo5pC(snrJL;
        private int? AHxCaphXxC5uvu;
        [CompilerGenerated]
        private string AIYbPEZFPIB(c7C;
        [CompilerGenerated]
        private string AJK4pBZQRuLk5fMo8iYo;
        [CompilerGenerated]
        private string AKCpNDo;
        [CompilerGenerated]
        private string ALQRCRR5Is;
        [CompilerGenerated]
        private string AM)mPO1uc;

        public RegionSelector()
        {
            this.ProvinceTitle = "省：";
            this.CityTitle = "市：";
            this.CountyTitle = "区/县：";
            this.NullToDisplay = "-请选择-";
            this.Separator = "，";
        }

        private static void AAZ0JeEma(2x58C7QQ)d9L4MH(WebControl control1, Dictionary<int, string> dictionary1, int? nullable1)
        {
            foreach (int num in dictionary1.Keys)
            {
                WebControl child = AC2pFbvHCa1v4C1DIZ8SngolueQb(num.ToString(CultureInfo.InvariantCulture), dictionary1[num]);
                if (nullable1.HasValue && (num == nullable1.Value))
                {
                    child.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cwBlAGwAZQBjAHQAZQBkAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dAByAHUAZQA="));
                }
                control1.Controls.Add(child);
            }
        }

        private WebControl ABOZyE4bfGcbd5Z8N5SO7b2(string text1, string text2)
        {
            WebControl control = new WebControl(HtmlTextWriterTag.Select);
            control.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aQBkAA=="), text1);
            control.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("bgBhAG0AZQA="), text1);
            control.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cwBlAGwAZQBjAHQAcwBlAHQA"), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cgBlAGcAaQBvAG4AcwA="));
            WebControl child = new WebControl(HtmlTextWriterTag.Option);
            child.Controls.Add(new LiteralControl(text2));
            child.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dgBhAGwAdQBlAA=="), "");
            control.Controls.Add(child);
            return control;
        }

        private static WebControl AC2pFbvHCa1v4C1DIZ8SngolueQb(string text1, string text2)
        {
            WebControl control = new WebControl(HtmlTextWriterTag.Option);
            control.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dgBhAGwAdQBlAA=="), text1);
            control.Controls.Add(new LiteralControl(text2.Trim()));
            return control;
        }

        private static Literal AD)2Z(NKy(string text1)
        {
            return new Literal { Text = text1 };
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            if (!this.ABOZyE4bfGcbd5Z8N5SO7b2)
            {
                if (!string.IsNullOrEmpty(this.Context.Request.Form[A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cgBlAGcAaQBvAG4AUwBlAGwAZQBjAHQAbwByAFYAYQBsAHUAZQA=")]))
                {
                    this.AAZ0JeEma(2x58C7QQ)d9L4MH = new int?(int.Parse(this.Context.Request.Form[A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cgBlAGcAaQBvAG4AUwBlAGwAZQBjAHQAbwByAFYAYQBsAHUAZQA=")]));
                }
                this.ABOZyE4bfGcbd5Z8N5SO7b2 = true;
            }
            if (this.AAZ0JeEma(2x58C7QQ)d9L4MH.HasValue)
            {
                XmlNode region = RegionHelper.GetRegion(this.AAZ0JeEma(2x58C7QQ)d9L4MH.Value);
                if (region != null)
                {
                    if (region.Name == A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("YwBvAHUAbgB0AHkA"))
                    {
                        this.AHxCaphXxC5uvu = new int?(this.AAZ0JeEma(2x58C7QQ)d9L4MH.Value);
                        this.AGeIcrrVo5pC(snrJL = new int?(int.Parse(region.ParentNode.Attributes[A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aQBkAA==")].Value));
                        this.AFCkJKk = new int?(int.Parse(region.ParentNode.ParentNode.Attributes[A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aQBkAA==")].Value));
                    }
                    else if (region.Name == A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("YwBpAHQAeQA="))
                    {
                        this.AGeIcrrVo5pC(snrJL = new int?(this.AAZ0JeEma(2x58C7QQ)d9L4MH.Value);
                        this.AFCkJKk = new int?(int.Parse(region.ParentNode.Attributes[A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aQBkAA==")].Value));
                    }
                    else if (region.Name == A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cAByAG8AdgBpAG4AYwBlAA=="))
                    {
                        this.AFCkJKk = new int?(this.AAZ0JeEma(2x58C7QQ)d9L4MH.Value);
                    }
                }
            }
            this.AC2pFbvHCa1v4C1DIZ8SngolueQb = this.ABOZyE4bfGcbd5Z8N5SO7b2(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("ZABkAGwAUgBlAGcAaQBvAG4AcwAxAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LQD3iwmQ6WIBdy0A"));
            AAZ0JeEma(2x58C7QQ)d9L4MH(this.AC2pFbvHCa1v4C1DIZ8SngolueQb, RegionHelper.GetAllProvinces(), this.AFCkJKk);
            this.Controls.Add(AD)2Z(NKy(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABzAHAAYQBuAD4A")));
            this.Controls.Add(this.AC2pFbvHCa1v4C1DIZ8SngolueQb);
            this.Controls.Add(AD)2Z(NKy(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PAAvAHMAcABhAG4APgA=")));
            this.AD)2Z(NKy = this.ABOZyE4bfGcbd5Z8N5SO7b2(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("ZABkAGwAUgBlAGcAaQBvAG4AcwAyAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LQD3iwmQ6WICXi0A"));
            if (this.AFCkJKk.HasValue)
            {
                AAZ0JeEma(2x58C7QQ)d9L4MH(this.AD)2Z(NKy, RegionHelper.GetCitys(this.AFCkJKk.Value), this.AGeIcrrVo5pC(snrJL);
            }
            this.Controls.Add(AD)2Z(NKy(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABzAHAAYQBuAD4A")));
            this.Controls.Add(this.AD)2Z(NKy);
            this.Controls.Add(AD)2Z(NKy(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PAAvAHMAcABhAG4APgA=")));
            this.AE)AZJK0MajbU = this.ABOZyE4bfGcbd5Z8N5SO7b2(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("ZABkAGwAUgBlAGcAaQBvAG4AcwAzAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LQD3iwmQ6WI6Uy0A"));
            if (this.AGeIcrrVo5pC(snrJL.HasValue)
            {
                AAZ0JeEma(2x58C7QQ)d9L4MH(this.AE)AZJK0MajbU, RegionHelper.GetCountys(this.AGeIcrrVo5pC(snrJL.Value), this.AHxCaphXxC5uvu);
            }
            this.Controls.Add(AD)2Z(NKy(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABzAHAAYQBuAD4A")));
            this.Controls.Add(this.AE)AZJK0MajbU);
            this.Controls.Add(AD)2Z(NKy(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PAAvAHMAcABhAG4APgA=")));
        }

        public int? GetSelectedRegionId()
        {
            if (!string.IsNullOrEmpty(this.Context.Request.Form[A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cgBlAGcAaQBvAG4AUwBlAGwAZQBjAHQAbwByAFYAYQBsAHUAZQA=")]))
            {
                return new int?(int.Parse(this.Context.Request.Form[A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cgBlAGcAaQBvAG4AUwBlAGwAZQBjAHQAbwByAFYAYQBsAHUAZQA=")]));
            }
            return null;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            writer.AddAttribute(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aQBkAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cgBlAGcAaQBvAG4AUwBlAGwAZQBjAHQAbwByAFYAYQBsAHUAZQA="));
            writer.AddAttribute(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("bgBhAG0AZQA="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cgBlAGcAaQBvAG4AUwBlAGwAZQBjAHQAbwByAFYAYQBsAHUAZQA="));
            writer.AddAttribute(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dgBhAGwAdQBlAA=="), this.AAZ0JeEma(2x58C7QQ)d9L4MH.HasValue ? this.AAZ0JeEma(2x58C7QQ)d9L4MH.Value.ToString(CultureInfo.InvariantCulture) : "");
            writer.AddAttribute(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dAB5AHAAZQA="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aABpAGQAZABlAG4A"));
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
            if (!this.Page.ClientScript.IsStartupScriptRegistered(base.GetType(), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("UgBlAGcAaQBvAG4AUwBlAGwAZQBjAHQAUwBjAHIAaQBwAHQA")))
            {
                string script = string.Format(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABzAGMAcgBpAHAAdAAgAHMAcgBjAD0AIgB7ADAAfQAiACAAdAB5AHAAZQA9ACIAdABlAHgAdAAvAGoAYQB2AGEAcwBjAHIAaQBwAHQAIgA+ADwALwBzAGMAcgBpAHAAdAA+AA=="), this.Page.ClientScript.GetWebResourceUrl(base.GetType(), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("SABpAGQAaQBzAHQAcgBvAC4AVQBJAC4AQwBvAG0AbQBvAG4ALgBDAG8AbgB0AHIAbwBsAHMALgByAGUAZwBpAG8AbgAuAGgAZQBsAHAAZQByAC4AagBzAA==")));
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("UgBlAGcAaQBvAG4AUwBlAGwAZQBjAHQAUwBjAHIAaQBwAHQA"), script, false);
            }
        }

        public void SetSelectedRegionId(int? selectedRegionId)
        {
            this.AAZ0JeEma(2x58C7QQ)d9L4MH = selectedRegionId;
            this.ABOZyE4bfGcbd5Z8N5SO7b2 = true;
        }

        public string CityTitle
        {
            [CompilerGenerated]
            get
            {
                return this.AJK4pBZQRuLk5fMo8iYo;
            }
            [CompilerGenerated]
            set
            {
                this.AJK4pBZQRuLk5fMo8iYo = value;
            }
        }

        public override ControlCollection Controls
        {
            get
            {
                base.EnsureChildControls();
                return base.Controls;
            }
        }

        public string CountyTitle
        {
            [CompilerGenerated]
            get
            {
                return this.AKCpNDo;
            }
            [CompilerGenerated]
            set
            {
                this.AKCpNDo = value;
            }
        }

        public string NullToDisplay
        {
            [CompilerGenerated]
            get
            {
                return this.AM)mPO1uc;
            }
            [CompilerGenerated]
            set
            {
                this.AM)mPO1uc = value;
            }
        }

        public string ProvinceTitle
        {
            [CompilerGenerated]
            get
            {
                return this.AIYbPEZFPIB(c7C;
            }
            [CompilerGenerated]
            set
            {
                this.AIYbPEZFPIB(c7C = value;
            }
        }

        public string SelectedRegions
        {
            get
            {
                int? selectedRegionId = this.GetSelectedRegionId();
                if (!selectedRegionId.HasValue)
                {
                    return "";
                }
                return RegionHelper.GetFullRegion(selectedRegionId.Value, this.Separator);
            }
            set
            {
                string[] strArray = value.Split(new char[] { ',' });
                if (strArray.Length >= 3)
                {
                    int? selectedRegionId = new int?(RegionHelper.GetRegionId(strArray[2], strArray[1], strArray[0]));
                    this.SetSelectedRegionId(selectedRegionId);
                }
            }
        }

        public string Separator
        {
            [CompilerGenerated]
            get
            {
                return this.ALQRCRR5Is;
            }
            [CompilerGenerated]
            set
            {
                this.ALQRCRR5Is = value;
            }
        }
    }
}

