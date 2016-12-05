namespace ASPNET.WebControls
{
    using System;
    using System.Collections;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class DropdownColumn : BoundField
    {
        private bool allowNull = true;
        private string clientOnChangeEventScript;
        private string dataField;
        private string dataKey;
        public object DataSource;
        private string dataTextField;
        private string dataValueField;
        private bool enabledSelect = true;
        private bool forEditItem;
        private string id = "DropdownlistCol";
        private string nullToDisplay = "";

        private void EditItemDataBinding(object sender, EventArgs e)
        {
            DataControlFieldCell cell = (DataControlFieldCell) sender;
            DropDownList list = (DropDownList) cell.Controls[0];
            ListItem item = null;
            try
            {
                IEnumerator enumerator = ((IEnumerable) this.DataSource).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    string str = Convert.ToString(DataBinder.Eval(enumerator.Current, this.DataValueField));
                    string text = Convert.ToString(DataBinder.Eval(enumerator.Current, this.DataTextField));
                    list.Items.Add(new ListItem(text, str));
                }
            }
            catch
            {
                throw new Exception("Specified Field was not found.");
            }
            if (this.AllowNull)
            {
                list.Items.Insert(0, new ListItem(this.NullToDisplay, string.Empty));
            }
            try
            {
                GridViewRow namingContainer = (GridViewRow) cell.NamingContainer;
                item = list.Items.FindByValue(Convert.ToString(DataBinder.Eval(namingContainer.DataItem, this.DataKey)));
            }
            catch
            {
                throw new Exception("Specified DataField was not found.");
            }
            if (item != null)
            {
                item.Selected = true;
            }
            if (this.ClientOnChangeEventScript != null)
            {
                list.Attributes.Add("onchange", this.ClientOnChangeEventScript);
            }
        }

        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            base.InitializeCell(cell, cellType, rowState, rowIndex);
            if (cellType == DataControlCellType.DataCell)
            {
                if (this.JustForEditItem)
                {
                    switch (rowState)
                    {
                        case DataControlRowState.Normal:
                        case DataControlRowState.Alternate:
                        case DataControlRowState.Selected:
                            cell.DataBinding += new EventHandler(this.ItemDataBinding);
                            return;

                        case (DataControlRowState.Selected | DataControlRowState.Alternate):
                            return;

                        case DataControlRowState.Edit:
                            cell.DataBinding += new EventHandler(this.EditItemDataBinding);
                            return;
                    }
                }
                else
                {
                    cell.DataBinding += new EventHandler(this.EditItemDataBinding);
                    DropDownList child = new DropDownList {
                        ID = this.ID
                    };
                    if (!this.EnabledSelect)
                    {
                        child.Enabled = false;
                    }
                    cell.Controls.Add(child);
                }
            }
        }

        private void ItemDataBinding(object sender, EventArgs e)
        {
            DataControlFieldCell cell = (DataControlFieldCell) sender;
            GridViewRow namingContainer = (GridViewRow) cell.NamingContainer;
            try
            {
                cell.Text = Convert.ToString(DataBinder.Eval(namingContainer.DataItem, this.DataField));
            }
            catch
            {
                throw new Exception("Specified DataField was not found.");
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

        public string ClientOnChangeEventScript
        {
            get
            {
                return this.clientOnChangeEventScript;
            }
            set
            {
                this.clientOnChangeEventScript = value;
            }
        }

       new public string DataField
        {
            get
            {
                return this.dataField;
            }
            set
            {
                this.dataField = value;
            }
        }

        public string DataKey
        {
            get
            {
                return this.dataKey;
            }
            set
            {
                this.dataKey = value;
            }
        }

        public string DataTextField
        {
            get
            {
                return this.dataTextField;
            }
            set
            {
                this.dataTextField = value;
            }
        }

        public string DataValueField
        {
            get
            {
                return this.dataValueField;
            }
            set
            {
                this.dataValueField = value;
            }
        }

        public bool EnabledSelect
        {
            get
            {
                return this.enabledSelect;
            }
            set
            {
                this.enabledSelect = value;
            }
        }

        public string ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public bool JustForEditItem
        {
            get
            {
                return this.forEditItem;
            }
            set
            {
                this.forEditItem = value;
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

        public GridView Owner
        {
            get
            {
                return (GridView) base.Control;
            }
        }

        public ListItemCollection SelectedItems
        {
            get
            {
                ListItemCollection items = new ListItemCollection();
                foreach (GridViewRow row in this.Owner.Rows)
                {
                    DropDownList list = (DropDownList) row.FindControl(this.ID);
                    if ((list != null) && (list.Items.Count > 0))
                    {
                        items.Add(list.SelectedItem);
                    }
                }
                return items;
            }
        }

        public string[] SelectedValues
        {
            get
            {
                ArrayList list = new ArrayList();
                if (this.Owner != null)
                {
                    foreach (GridViewRow row in this.Owner.Rows)
                    {
                        DropDownList list2 = (DropDownList) row.FindControl(this.ID);
                        if ((list2 != null) && (list2.Items.Count > 0))
                        {
                            list.Add(list2.SelectedValue);
                        }
                    }
                }
                return (string[]) list.ToArray(typeof(string));
            }
        }
    }
}

