namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using Hidistro.Core;
    using System;
    using System.Web.UI;

    public class StatusMessage : LiteralControl
    {
        private bool AAZ0JeEma(2x58C7QQ)d9L4MH = true;
        private bool ABOZyE4bfGcbd5Z8N5SO7b2;

        public StatusMessage()
        {
            this.Visible = false;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.Visible)
            {
                if (!this.ABOZyE4bfGcbd5Z8N5SO7b2)
                {
                    if (this.AAZ0JeEma(2x58C7QQ)d9L4MH)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("QwBvAG0AbQBvAG4ATQBlAHMAcwBhAGcAZQBTAHUAYwBjAGUAcwBzAA=="));
                    }
                    else
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("QwBvAG0AbQBvAG4ATQBlAHMAcwBhAGcAZQBFAHIAcgBvAHIA"));
                    }
                }
                else if (this.AAZ0JeEma(2x58C7QQ)d9L4MH)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("QwBvAG0AbQBvAG4ATQBlAHMAcwBhAGcAZQBTAHUAYwBjAGUAcwBzAA=="));
                }
                else
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("QwBvAG0AbQBvAG4AVwBhAHIAbgBpAG4AZwBNAGUAcwBzAGEAZwBlAA=="));
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("MAA="));
                writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("MAA="));
                writer.AddAttribute(HtmlTextWriterAttribute.Class, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("QwBvAG0AbQBvAG4ATQBlAHMAcwBhAGcAZQBTAHUAYwBjAGUAcwBzAA=="));
                writer.AddAttribute(HtmlTextWriterAttribute.Border, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("MAA="));
                writer.RenderBeginTag(HtmlTextWriterTag.Table);
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.AddAttribute(HtmlTextWriterAttribute.Style, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cABhAGQAZABpAG4AZwAtAHIAaQBnAGgAdAA6ACAAOABwAHgAOwA="));
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.AddAttribute(HtmlTextWriterAttribute.Style, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cABhAGQAZABpAG4AZwAtAHIAaQBnAGgAdAA6ACAAOABwAHgAOwA="));
                if (this.AAZ0JeEma(2x58C7QQ)d9L4MH)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Src, Globals.ApplicationPath + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB1AHQAaQBsAGkAdAB5AC8AcABpAGMAcwAvAHMAdABhAHQAdQBzAC0AZwByAGUAZQBuAC4AZwBpAGYA"));
                }
                else if (this.ABOZyE4bfGcbd5Z8N5SO7b2)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Src, Globals.ApplicationPath + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB1AHQAaQBsAGkAdAB5AC8AcABpAGMAcwAvAHMAdABhAHQAdQBzAC0AeQBlAGwAbABvAHcALgBnAGkAZgA="));
                }
                else
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Src, Globals.ApplicationPath + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB1AHQAaQBsAGkAdAB5AC8AcABpAGMAcwAvAHMAdABhAHQAdQBzAC0AcgBlAGQALgBnAGkAZgA="));
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Align, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("YQBiAHMAbQBpAGQAZABsAGUA"));
                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                writer.RenderEndTag();
                writer.RenderEndTag();
                writer.AddAttribute(HtmlTextWriterAttribute.Width, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("MQAwADAAJQA="));
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write(this.Text);
                writer.RenderEndTag();
                writer.RenderEndTag();
                writer.RenderEndTag();
            }
        }

        public bool IsWarning
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
    }
}

