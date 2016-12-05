using Hidistro.ControlPanel.Commodities;
using Hidistro.Entities.Commodities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Hidistro.UI.ControlPanel.Utility
{
    public class ProductCategoriesCheckBoxList : CheckBoxList
    {
        private int repeatColumns = 8;
        private RepeatDirection repeatDirection;

        public override void DataBind()
        {
            this.Items.Clear();
            foreach (CategoryInfo info in CatalogHelper.GetMainCategories())
            {
                base.Items.Add(new ListItem(info.Name, info.CategoryId.ToString()));
            }

        }

        public override int RepeatColumns
        {
            get
            {
                return this.repeatColumns;
            }
            set
            {
                this.repeatColumns = value;
            }
        }

        public override RepeatDirection RepeatDirection
        {
            get
            {
                return this.repeatDirection;
            }
            set
            {
                this.repeatDirection = value;
            }
        }
    }
}
