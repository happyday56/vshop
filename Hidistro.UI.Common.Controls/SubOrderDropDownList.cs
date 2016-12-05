using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Hidistro.UI.Common.Controls
{
    public class SubOrderDropDownList : DropDownList
    {
        private bool allowNull = true;
        private string nullToDisplay = "";

        public override void DataBind()
        {
            this.Items.Clear();

            if (this.AllowNull)
            {
                base.Items.Add(new ListItem(this.NullToDisplay, string.Empty));
            }
            
            DataTable sublist = this.SubOrderList;
            if (null != sublist && sublist.Rows.Count > 0)
            {
                foreach (DataRow dr in sublist.Rows)
                {
                    if (null != dr["BrandName"])
                    {
                        this.Items.Add(new ListItem((string)dr["BrandName"], (string)dr["SubOrderId"]));
                    }                    
                }
            }
        }

        public bool AllowNull
        {
            get
            {
                return this.allowNull;
            }
            set
            {
                this.allowNull = value;
            }
        }

        public string NullToDisplay
        {
            get
            {
                return this.nullToDisplay;
            }
            set
            {
                this.nullToDisplay = value;
            }
        }


        public DataTable SubOrderList { get; set; }

    }
}
