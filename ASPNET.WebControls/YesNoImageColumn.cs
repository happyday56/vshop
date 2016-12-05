namespace ASPNET.WebControls
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class YesNoImageColumn : DataControlField
    {
        private string dataField;

        public YesNoImageColumn()
        {
            this.CommandName = "SetYesOrNo";
        }

        private void cell_DataBinding(object sender, EventArgs e)
        {
            TableCell cell = (TableCell) sender;
            if (cell.Controls.Count != 0)
            {
                string webResourceUrl = base.Control.Page.ClientScript.GetWebResourceUrl(base.GetType(), "ASPNET.WebControls.Grid.Images.false.gif");
                string str2 = base.Control.Page.ClientScript.GetWebResourceUrl(base.GetType(), "ASPNET.WebControls.Grid.Images.true.gif");
                GridViewRow namingContainer = (GridViewRow) cell.NamingContainer;
                ImageButton button = (ImageButton) cell.Controls[0];
                try
                {
                    button.ImageUrl = Convert.ToBoolean(DataBinder.Eval(namingContainer.DataItem, this.DataField)) ? str2 : webResourceUrl;
                }
                catch
                {
                    throw new Exception("Specified DataField was not found.");
                }
            }
        }

        protected override DataControlField CreateField()
        {
            return new YesNoImageColumn();
        }

        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            base.InitializeCell(cell, cellType, rowState, rowIndex);
            if (cell == null)
            {
                throw new ArgumentNullException("cell");
            }
            if (cellType == DataControlCellType.DataCell)
            {
                cell.DataBinding += new EventHandler(this.cell_DataBinding);
                ImageButton child = new ImageButton {
                    BorderWidth = Unit.Pixel(0),
                    CommandName = this.CommandName
                };
                cell.Controls.Add(child);
            }
        }

        public string CommandName { get; set; }

        public string DataField
        {
            get
            {
                if (string.IsNullOrEmpty(this.dataField))
                {
                    object obj2 = base.ViewState["DataField"];
                    if (obj2 != null)
                    {
                        this.dataField = (string) obj2;
                    }
                    else
                    {
                        this.dataField = string.Empty;
                    }
                }
                return this.dataField;
            }
            set
            {
                if (!object.Equals(value, base.ViewState["DataField"]))
                {
                    base.ViewState["DataField"] = value;
                    this.dataField = value;
                    this.OnFieldChanged();
                }
            }
        }
    }
}

