namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using System;
    using System.Web.UI.WebControls;

    public class HourDropDownList : DropDownList
    {
        public override void DataBind()
        {
            this.Items.Clear();
            for (int i = 0; i <= 0x17; i++)
            {
                string text = i + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("9mU=");
                this.Items.Add(new ListItem(text, i.ToString()));
            }
        }

        public int? SelectedValue
        {
            get
            {
                int result = 0;
                int.TryParse(base.SelectedValue, out result);
                return new int?(result);
            }
            set
            {
                if (value.HasValue)
                {
                    base.SelectedValue = value.Value.ToString();
                }
            }
        }
    }
}

