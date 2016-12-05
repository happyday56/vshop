namespace ASPNET.WebControls
{
    using System;
    using System.Web.UI.WebControls;

    public class SortImageColumn : ImageField
    {
        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            base.InitializeCell(cell, cellType, rowState, rowIndex);
            if (cell == null)
            {
                throw new ArgumentNullException("cell");
            }
            string webResourceUrl = base.Control.Page.ClientScript.GetWebResourceUrl(base.GetType(), "ASPNET.WebControls.Grid.Images.up.gif");
            string str2 = base.Control.Page.ClientScript.GetWebResourceUrl(base.GetType(), "ASPNET.WebControls.Grid.Images.down.gif");
            ImageButton child = new ImageButton {
                ID = "rise",
                ImageUrl = webResourceUrl,
                CommandName = "Rise"
            };
            ImageButton button2 = new ImageButton {
                ID = "fall",
                ImageUrl = str2,
                CommandName = "Fall"
            };
            if (cellType == DataControlCellType.DataCell)
            {
                cell.Controls.Add(button2);
                cell.Controls.Add(child);
            }
        }
    }
}

