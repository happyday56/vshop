namespace Hidistro.UI.Common.Controls
{
    using Hidistro.Entities;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web.UI.WebControls;

    public class ExpressRadioButtonList : RadioButtonList
    {
        [CompilerGenerated]
        private string AAZ0JeEma(2x58C7QQ)d9L4MH;
        [CompilerGenerated]
        private IList<string> ABOZyE4bfGcbd5Z8N5SO7b2;

        public override void DataBind()
        {
            IList<string> expressCompanies = this.ExpressCompanies;
            if ((expressCompanies == null) || (expressCompanies.Count == 0))
            {
                expressCompanies = ExpressHelper.GetAllExpressName();
            }
            base.Items.Clear();
            foreach (string str in expressCompanies)
            {
                ListItem item = new ListItem(str, str);
                if (string.Compare(item.Value, this.Name, false) == 0)
                {
                    item.Selected = true;
                }
                base.Items.Add(item);
            }
        }

        public IList<string> ExpressCompanies
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

        public string Name
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

