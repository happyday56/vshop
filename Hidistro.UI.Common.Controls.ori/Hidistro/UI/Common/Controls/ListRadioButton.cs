namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:ListRadioButton runat=server></{0}:ListRadioButton>")]
    public class ListRadioButton : RadioButton, IPostBackDataHandler
    {
        private void AAZ0JeEma(2x58C7QQ)d9L4MH(HtmlTextWriter writer1)
        {
            writer1.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer1.AddAttribute(HtmlTextWriterAttribute.Type, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cgBhAGQAaQBvAA=="));
            writer1.AddAttribute(HtmlTextWriterAttribute.Name, this.GroupName);
            writer1.AddAttribute(HtmlTextWriterAttribute.Value, this.get_Value());
            if (this.Checked)
            {
                writer1.AddAttribute(HtmlTextWriterAttribute.Checked, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("YwBoAGUAYwBrAGUAZAA="));
            }
            if (!this.Enabled)
            {
                writer1.AddAttribute(HtmlTextWriterAttribute.Disabled, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("ZABpAHMAYQBiAGwAZQBkAA=="));
            }
            string str = base.Attributes[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("bwBuAGMAbABpAGMAawA=")];
            if (this.AutoPostBack)
            {
                if (str != null)
                {
                    str = string.Empty;
                }
                str = str + this.Page.ClientScript.GetPostBackEventReference(this, string.Empty);
                writer1.AddAttribute(HtmlTextWriterAttribute.Onclick, str);
                writer1.AddAttribute(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("bABhAG4AZwB1AGEAZwBlAA=="), AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("agBhAHYAYQBzAGMAcgBpAHAAdAA="));
            }
            else if (str != null)
            {
                writer1.AddAttribute(HtmlTextWriterAttribute.Onclick, str);
            }
            if (this.AccessKey.Length > 0)
            {
                writer1.AddAttribute(HtmlTextWriterAttribute.Accesskey, this.AccessKey);
            }
            if (this.TabIndex != 0)
            {
                writer1.AddAttribute(HtmlTextWriterAttribute.Tabindex, this.TabIndex.ToString(NumberFormatInfo.InvariantInfo));
            }
            writer1.RenderBeginTag(HtmlTextWriterTag.Input);
            writer1.RenderEndTag();
        }

        protected override void Render(HtmlTextWriter output)
        {
            this.AAZ0JeEma(2x58C7QQ)d9L4MH(output);
        }

        bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            bool flag = false;
            string str = postCollection[this.GroupName];
            if ((str != null) && (str == this.get_Value()))
            {
                if (!this.Checked)
                {
                    this.Checked = true;
                    flag = true;
                }
                return flag;
            }
            if (this.Checked)
            {
                this.Checked = false;
            }
            return flag;
        }

        void IPostBackDataHandler.RaisePostDataChangedEvent()
        {
            this.OnCheckedChanged(EventArgs.Empty);
        }

        private string AAZ0JeEma(2x58C7QQ)d9L4MH
        {
            get
            {
                string uniqueID = base.Attributes[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dgBhAGwAdQBlAA==")];
                if (uniqueID == null)
                {
                    uniqueID = this.UniqueID;
                }
                return uniqueID;
            }
        }
    }
}

