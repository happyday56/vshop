namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using Hidistro.ControlPanel.Commodities;
    using Hidistro.Core;
    using System;
    using System.Data;
    using System.Web.UI.WebControls;

    public class ProductTagsDropDownList : DropDownList
    {
        private bool AAZ0JeEma(2x58C7QQ)d9L4MH = true;
        private string ABOZyE4bfGcbd5Z8N5SO7b2 = "全部";

        public override void DataBind()
        {
            base.Items.Clear();
            if (this.AllowNull)
            {
                base.Items.Add(new ListItem(this.NullToDisplay, string.Empty));
            }
            foreach (DataRow row in CatalogHelper.GetTags().Rows)
            {
                ListItem item = new ListItem(Globals.HtmlDecode(row[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VABhAGcATgBhAG0AZQA=")].ToString()), row[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VABhAGcASQBEAA==")].ToString());
                base.Items.Add(item);
            }
        }

        public bool AllowNull
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

        public string NullToDisplay
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

        public int? SelectedValue
        {
            get
            {
                if (string.IsNullOrEmpty(base.SelectedValue))
                {
                    return null;
                }
                return new int?(int.Parse(base.SelectedValue));
            }
            set
            {
                if (!value.HasValue)
                {
                    base.SelectedValue = string.Empty;
                }
                else
                {
                    base.SelectedValue = value.ToString();
                }
            }
        }
    }
}

