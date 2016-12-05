namespace Hidistro.UI.Common.Controls
{
    using Hidistro.ControlPanel.Sales;
    using Hidistro.Entities.Sales;
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.UI.WebControls;

    public class ShippingModeDropDownList : DropDownList
    {
        private bool AAZ0JeEma(2x58C7QQ)d9L4MH = true;
        [CompilerGenerated]
        private string ABOZyE4bfGcbd5Z8N5SO7b2;

        public override void DataBind()
        {
            base.Items.Clear();
            if (this.AllowNull)
            {
                base.Items.Add(new ListItem(this.NullToDisplay, string.Empty));
            }
            foreach (ShippingModeInfo info in SalesHelper.GetShippingModes())
            {
                base.Items.Add(new ListItem(info.Name, info.ModeId.ToString()));
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
            [CompilerGenerated]
            get
            {
                return this.ABOZyE4bfGcbd5Z8N5SO7b2;
            }
            [CompilerGenerated]
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

