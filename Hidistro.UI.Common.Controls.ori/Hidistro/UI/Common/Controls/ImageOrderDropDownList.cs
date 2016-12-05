namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using System;
    using System.Web.UI.WebControls;

    public class ImageOrderDropDownList : DropDownList
    {
        public override void DataBind()
        {
            this.Items.Clear();
            this.Items.Add(new ListItem(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("CWMKTiBP9mX0lc5OWmYwUull"), AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("MAA=")));
            this.Items.Add(new ListItem(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("CWMKTiBP9mX0lc5O6WUwUlpm"), AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("MQA=")));
            this.Items.Add(new ListItem(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("CWP+VkdyDVRHU49e"), AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("MgA=")));
            this.Items.Add(new ListItem(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("CWP+VkdyDVRNlo9e"), AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("MwA=")));
            this.Items.Add(new ListItem(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("CWPuTzll9mX0lc5OWmYwUull"), AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("NAA=")));
            this.Items.Add(new ListItem(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("CWPuTzll9mX0lc5O6WUwUlpm"), AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("NQA=")));
            this.Items.Add(new ListItem(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("CWP+Vkdyzk4nWTBSD1w="), AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("NgA=")));
            this.Items.Add(new ListItem(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("CWP+Vkdyzk4PXDBSJ1k="), AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("NwA=")));
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

