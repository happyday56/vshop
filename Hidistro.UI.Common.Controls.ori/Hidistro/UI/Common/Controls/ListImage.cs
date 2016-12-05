namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using ASPNET.WebControls;
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class ListImage : Image
    {
        [CompilerGenerated]
        private string AAZ0JeEma(2x58C7QQ)d9L4MH;

        protected override void OnDataBinding(EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.DataField))
            {
                object obj2 = DataBinder.Eval(this.Page.GetDataItem(), this.DataField);
                if (((obj2 != null) && (obj2 != DBNull.Value)) && !string.IsNullOrEmpty(obj2.ToString()))
                {
                    base.ImageUrl = (string) obj2;
                }
                else
                {
                    SiteSettings masterSettings = SettingsManager.GetMasterSettings(true);
                    if (this.DataField.Equals(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VABoAHUAbQBiAG4AYQBpAGwAVQByAGwANAAwAA==")))
                    {
                        base.ImageUrl = masterSettings.DefaultProductThumbnail1;
                    }
                    else if (this.DataField.Equals(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VABoAHUAbQBiAG4AYQBpAGwAVQByAGwANgAwAA==")))
                    {
                        base.ImageUrl = masterSettings.DefaultProductThumbnail2;
                    }
                    else if (this.DataField.Equals(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VABoAHUAbQBiAG4AYQBpAGwAVQByAGwAMQAwADAA")))
                    {
                        base.ImageUrl = masterSettings.DefaultProductThumbnail3;
                    }
                    else if (this.DataField.Equals(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VABoAHUAbQBiAG4AYQBpAGwAVQByAGwAMQA2ADAA")))
                    {
                        base.ImageUrl = masterSettings.DefaultProductThumbnail4;
                    }
                    else if (this.DataField.Equals(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VABoAHUAbQBiAG4AYQBpAGwAVQByAGwAMQA4ADAA")))
                    {
                        base.ImageUrl = masterSettings.DefaultProductThumbnail5;
                    }
                    else if (this.DataField.Equals(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VABoAHUAbQBiAG4AYQBpAGwAVQByAGwAMgAyADAA")))
                    {
                        base.ImageUrl = masterSettings.DefaultProductThumbnail6;
                    }
                    else if (this.DataField.Equals(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VABoAHUAbQBiAG4AYQBpAGwAVQByAGwAMwAxADAA")))
                    {
                        base.ImageUrl = masterSettings.DefaultProductThumbnail7;
                    }
                    else
                    {
                        base.ImageUrl = masterSettings.DefaultProductThumbnail8;
                    }
                }
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(base.ImageUrl))
            {
                if ((!string.IsNullOrEmpty(base.ImageUrl) && !Utils.IsUrlAbsolute(base.ImageUrl.ToLower())) && ((Utils.ApplicationPath.Length > 0) && !base.ImageUrl.StartsWith(Utils.ApplicationPath)))
                {
                    base.ImageUrl = Utils.ApplicationPath + base.ImageUrl;
                }
                base.Render(writer);
            }
        }

        public string DataField
        {
            [CompilerGenerated]
            get
            {
                return this.AAZ0JeEma(2x58C7QQ)d9L4MH;
            }
            [CompilerGenerated]
            set
            {
                this.AAZ0JeEma(2x58C7QQ)d9L4MH = value;
            }
        }
    }
}

