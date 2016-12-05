namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using System;
    using System.Globalization;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class MoneyColumnForAdmin : BoundField
    {
        private string AAZ0JeEma(2x58C7QQ)d9L4MH = string.Empty;

        private void AAZ0JeEma(2x58C7QQ)d9L4MH(object obj1, EventArgs)
        {
            TableCell cell = (TableCell) obj1;
            GridViewRow namingContainer = (GridViewRow) cell.NamingContainer;
            try
            {
                cell.Controls.Clear();
                object obj2 = DataBinder.Eval(namingContainer.DataItem, this.DataField);
                cell.Text = ((obj2 == null) || (obj2 == DBNull.Value)) ? this.NullDisplayText : Convert.ToDecimal(obj2).ToString(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("RgA="), CultureInfo.InvariantCulture);
                if (cell.Text == "")
                {
                    cell.Text = AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LQA=");
                }
                if (!string.IsNullOrEmpty(this.RemarkText))
                {
                    cell.Text = this.RemarkText + cell.Text;
                }
            }
            catch
            {
                throw new Exception(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("UwBwAGUAYwBpAGYAaQBlAGQAIABEAGEAdABhAEYAaQBlAGwAZAAgAHcAYQBzACAAbgBvAHQAIABmAG8AdQBuAGQALgA="));
            }
        }

        private void ABOZyE4bfGcbd5Z8N5SO7b2(object obj1, EventArgs)
        {
            TableCell cell = (TableCell) obj1;
            GridViewRow namingContainer = (GridViewRow) cell.NamingContainer;
            try
            {
                cell.Controls.Clear();
                object obj2 = DataBinder.Eval(namingContainer.DataItem, this.DataField);
                TextBox child = new TextBox {
                    ID = this.EditTextBoxId,
                    Width = Unit.Percentage(100.0),
                    Text = ((obj2 == null) || (obj2 == DBNull.Value)) ? "" : string.Format(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("ewAwADoARgB9AA=="), obj2)
                };
                cell.Controls.Add(child);
            }
            catch
            {
                throw new Exception(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("UwBwAGUAYwBpAGYAaQBlAGQAIABEAGEAdABhAEYAaQBlAGwAZAAgAHcAYQBzACAAbgBvAHQAIABmAG8AdQBuAGQALgA="));
            }
        }

        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            base.InitializeCell(cell, cellType, rowState, rowIndex);
            if (cell == null)
            {
                throw new ArgumentNullException(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("YwBlAGwAbAA="));
            }
            if (cellType == DataControlCellType.DataCell)
            {
                if ((rowState == DataControlRowState.Edit) && this.AllowEdit)
                {
                    cell.DataBinding += new EventHandler(this.ABOZyE4bfGcbd5Z8N5SO7b2);
                }
                else
                {
                    cell.DataBinding += new EventHandler(this.AAZ0JeEma(2x58C7QQ)d9L4MH);
                }
            }
        }

        public bool AllowEdit
        {
            get
            {
                return ((base.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("QQBsAGwAbwB3AEUAZABpAHQA")] == null) || ((bool) base.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("QQBsAGwAbwB3AEUAZABpAHQA")]));
            }
            set
            {
                base.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("QQBsAGwAbwB3AEUAZABpAHQA")] = value;
            }
        }

        public string EditTextBoxId
        {
            get
            {
                if (base.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("RQBkAGkAdABUAGUAeAB0AEIAbwB4AEkAZAA=")] == null)
                {
                    return null;
                }
                return (string) base.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("RQBkAGkAdABUAGUAeAB0AEIAbwB4AEkAZAA=")];
            }
            set
            {
                base.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("RQBkAGkAdABUAGUAeAB0AEIAbwB4AEkAZAA=")] = value;
            }
        }

        public string NullToDisplay
        {
            get
            {
                if (base.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TgB1AGwAbABUAG8ARABpAHMAcABsAGEAeQA=")] == null)
                {
                    return AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LQA=");
                }
                return (string) base.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TgB1AGwAbABUAG8ARABpAHMAcABsAGEAeQA=")];
            }
            set
            {
                base.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TgB1AGwAbABUAG8ARABpAHMAcABsAGEAeQA=")] = value;
            }
        }

        public string RemarkText
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

