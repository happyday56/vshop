namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.UI.WebControls;

    public class YesNoRadioButtonList : RadioButtonList
    {
        [CompilerGenerated]
        private string AAZ0JeEma(2x58C7QQ)d9L4MH;
        [CompilerGenerated]
        private string ABOZyE4bfGcbd5Z8N5SO7b2;

        public YesNoRadioButtonList()
        {
            this.NoText = "否";
            this.YesText = "是";
            this.Items.Clear();
            this.Items.Add(new ListItem(this.YesText, "True"));
            this.Items.Add(new ListItem(this.NoText, "False"));
            this.RepeatDirection = RepeatDirection.Horizontal;
            this.SelectedValue = true;
        }

        public string NoText
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

        public bool SelectedValue
        {
            get
            {
                return bool.Parse(base.SelectedValue);
            }
            set
            {
                base.SelectedIndex = base.Items.IndexOf(base.Items.FindByValue(value ? AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VAByAHUAZQA=") : AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("RgBhAGwAcwBlAA==")));
            }
        }

        public string YesText
        {
            [CompilerGenerated]
            get
            {
                return this.AAZ0JeEma(2x58C7QQ)d9L4MH;
            }
            [CompilerGenerated]
            set
            {
                this.AAZ0JeEma(2x58C7QQ)d9L4MH = value;
            }
        }
    }
}

