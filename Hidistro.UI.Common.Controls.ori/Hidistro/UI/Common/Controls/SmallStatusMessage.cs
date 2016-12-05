namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using Hidistro.Core;
    using System;
    using System.Web.UI;

    public class SmallStatusMessage : LiteralControl
    {
        private bool AAZ0JeEma(2x58C7QQ)d9L4MH = true;
        private string ABOZyE4bfGcbd5Z8N5SO7b2;

        public SmallStatusMessage()
        {
            this.Visible = false;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.Visible)
            {
                if (this.AAZ0JeEma(2x58C7QQ)d9L4MH)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TQBlAHMAcwBhAGcAZQBTAHUAYwBjAGUAcwBzAA=="));
                }
                else
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TQBlAHMAcwBhAGcAZQBFAHIAcgBvAHIA"));
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("MAA="));
                writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("MAA="));
                writer.AddAttribute(HtmlTextWriterAttribute.Border, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("MAA="));
                writer.AddAttribute(HtmlTextWriterAttribute.Width, this.Width);
                writer.RenderBeginTag(HtmlTextWriterTag.Table);
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.AddAttribute(HtmlTextWriterAttribute.Style, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cABhAGQAZABpAG4AZwAtAHIAaQBnAGgAdAA6ACAAMwBwAHgAOwA="));
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.AddAttribute(HtmlTextWriterAttribute.Style, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cABhAGQAZABpAG4AZwAtAHIAaQBnAGgAdAA6ACAAMwBwAHgAOwA="));
                if (this.AAZ0JeEma(2x58C7QQ)d9L4MH)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Src, Globals.GetVshopSkinPath(null) + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwBpAG0AYQBnAGUAcwAvAHMAdQBjAGMAZQBzAHMALgBnAGkAZgA="));
                }
                else
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Src, Globals.GetVshopSkinPath(null) + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwBpAG0AYQBnAGUAcwAvAHcAYQByAG4AaQBuAGcALgBnAGkAZgA="));
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Align, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("YQBiAHMAbQBpAGQAZABsAGUA"));
                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                writer.RenderEndTag();
                writer.RenderEndTag();
                writer.AddAttribute(HtmlTextWriterAttribute.Align, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("bABlAGYAdAA="));
                writer.AddAttribute(HtmlTextWriterAttribute.Width, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("MQAwADAAJQA="));
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABuAG8AYgByAD4A") + this.Text + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABuAG8AYgByAC8APgA="));
                writer.RenderEndTag();
                writer.RenderEndTag();
                writer.RenderEndTag();
            }
        }

        public bool Success
        {
            get
            {
                return this.AAZ0JeEma(2x58C7QQ)d9L4MH;
            }
            set
            {
                this.AAZ0JeEma(2x58C7QQ)d9L4MH = value;
            }
        }

        public string Width
        {
            get
            {
                if (string.IsNullOrEmpty(this.ABOZyE4bfGcbd5Z8N5SO7b2))
                {
                    return AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("MQAwADAAJQA=");
                }
                return this.ABOZyE4bfGcbd5Z8N5SO7b2;
            }
            set
            {
                this.ABOZyE4bfGcbd5Z8N5SO7b2 = value;
            }
        }
    }
}

