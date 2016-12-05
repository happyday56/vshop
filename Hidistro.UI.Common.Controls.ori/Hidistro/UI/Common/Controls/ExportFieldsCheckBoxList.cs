namespace Hidistro.UI.Common.Controls
{
    using System;
    using System.Web.UI.WebControls;

    public class ExportFieldsCheckBoxList : CheckBoxList
    {
        private System.Web.UI.WebControls.RepeatDirection AAZ0JeEma(2x58C7QQ)d9L4MH;
        private int ABOZyE4bfGcbd5Z8N5SO7b2 = 9;

        public ExportFieldsCheckBoxList()
        {
            this.Items.Clear();
            this.Items.Add(new ListItem("昵称", "UserName"));
            this.Items.Add(new ListItem("姓名", "RealName"));
            this.Items.Add(new ListItem("手机号", "CellPhone"));
            this.Items.Add(new ListItem("QQ", "QQ"));
            this.Items.Add(new ListItem("邮箱", "Email"));
            this.Items.Add(new ListItem("OpenId", "Openid"));
            this.Items.Add(new ListItem("积分", "Points"));
            this.Items.Add(new ListItem("消费金额", "Expenditure"));
            this.Items.Add(new ListItem("详细地址", "Address"));
        }

        public override int RepeatColumns
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

        public override System.Web.UI.WebControls.RepeatDirection RepeatDirection
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

