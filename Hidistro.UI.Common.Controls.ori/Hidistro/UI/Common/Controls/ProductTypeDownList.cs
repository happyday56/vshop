namespace Hidistro.UI.Common.Controls
{
    using Hidistro.ControlPanel.Commodities;
    using Hidistro.Entities.Commodities;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public class ProductTypeDownList : DropDownList
    {
        private bool AAZ0JeEma(2x58C7QQ)d9L4MH = true;
        private string ABOZyE4bfGcbd5Z8N5SO7b2 = "全部";

        public override void DataBind()
        {
            this.Items.Clear();
            IList<ProductTypeInfo> productTypes = ProductTypeHelper.GetProductTypes();
            if (this.AllowNull)
            {
                base.Items.Add(new ListItem(this.NullToDisplay, string.Empty));
            }
            foreach (ProductTypeInfo info in productTypes)
            {
                base.Items.Add(new ListItem(info.TypeName, info.TypeId.ToString()));
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
                if (value.HasValue && (value > 0))
                {
                    base.SelectedValue = value.Value.ToString();
                }
                else
                {
                    base.SelectedValue = string.Empty;
                }
            }
        }
    }
}

