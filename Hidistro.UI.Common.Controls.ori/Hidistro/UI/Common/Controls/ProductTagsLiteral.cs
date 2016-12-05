namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using Hidistro.ControlPanel.Commodities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class ProductTagsLiteral : Literal
    {
        protected IList<int> _selectvalue;

        protected override void Render(HtmlTextWriter writer)
        {
            base.Text = "";
            DataTable tags = CatalogHelper.GetTags();
            if (tags.Rows.Count < 0)
            {
                base.Text = AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("4GU=");
            }
            else
            {
                foreach (DataRow row in tags.Rows)
                {
                    string str = "";
                    if (this._selectvalue != null)
                    {
                        foreach (int num in this._selectvalue)
                        {
                            if (num == Convert.ToInt32(row[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VABhAGcASQBEAA==")].ToString()))
                            {
                                str = AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("YwBoAGUAYwBrAGUAZAA9ACIAYwBoAGUAYwBrAGUAZAAiAA==");
                            }
                        }
                    }
                    string text = base.Text;
                    base.Text = text + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABpAG4AcAB1AHQAIAB0AHkAcABlAD0AIgBjAGgAZQBjAGsAYgBvAHgAIgAgAG8AbgBjAGwAaQBjAGsAPQAiAGoAYQB2AGEAcwBjAHIAaQBwAHQAOgBDAGgAZQBjAGsAVABhAGcASQBkACgAdABoAGkAcwApACIAIAB2AGEAbAB1AGUAPQAiAA==") + row[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VABhAGcASQBEAA==")].ToString() + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IgAgAA==") + str + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwA+AA==") + row[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VABhAGcATgBhAG0AZQA=")].ToString() + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("ADA=");
                }
                base.Render(writer);
            }
        }

        public IList<int> SelectedValue
        {
            set
            {
                this._selectvalue = value;
            }
        }
    }
}

