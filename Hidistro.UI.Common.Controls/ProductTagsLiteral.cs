namespace Hidistro.UI.Common.Controls
{
    using Hidistro.ControlPanel.Commodities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class ProductTagsLiteral : Literal
    {
        protected IList<int> m_selectvalue;

        protected override void Render(HtmlTextWriter writer)
        {
            base.Text = "";
            DataTable tags = CatalogHelper.GetTags();
            if (tags.Rows.Count < 0)
            {
                base.Text = "无";
            }
            else
            {
                foreach (DataRow row in tags.Rows)
                {
                    string str = "";
                    if (this.m_selectvalue != null)
                    {
                        foreach (int num in this.m_selectvalue)
                        {
                            if (num == Convert.ToInt32(row["TagID"].ToString()))
                            {
                                str = "checked=\"checked\"";
                            }
                        }
                    }
                    string text = base.Text;
                    base.Text = text + "<input type=\"checkbox\" onclick=\"javascript:CheckTagId(this)\" value=\"" + row["TagID"].ToString() + "\" " + str + "/>" + row["TagName"].ToString() + "　";
                }
                base.Render(writer);
            }
        }

        public IList<int> SelectedValue
        {
            set
            {
                this.m_selectvalue = value;
            }
        }
    }
}

