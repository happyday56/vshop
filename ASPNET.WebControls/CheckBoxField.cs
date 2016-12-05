namespace ASPNET.WebControls
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class CheckBoxField : BoundField
    {
        public const string DataCellCheckBoxID = "checkboxCol";
        public const string HeaderCheckBoxID = "checkboxHead";
        private int headWidth = 30;
        private string text = "选择";

        private static string GetCheckHeadReverseScript()
        {
            string str = " <script language=JavaScript>";
            return (((((((((((((str + " function CheckReverse()" + " {") + " var frm = document.[frmID];" + "  var boolAllChecked;") + "  boolAllChecked=true;" + "  for(i=0;i< frm.length;i++)") + "  {" + "         e=frm.elements[i];") + "     if(e.type=='checkbox' && e.name.indexOf('checkboxCol') != -1)" + "        {") + "         if( e.checked== false)" + "           {") + "              e.checked = true;" + "              b = e.parentNode.parentNode;") + "              b.style.background = '#FBFBF4';" + "           }") + "          else " + "           {") + "             e.checked = false;" + "             b = e.parentNode.parentNode;") + "             b.style.background = '#ffffff';" + "            }") + "          }" + "    }") + " }" + "  </script>");
        }

        private static string GetClickCheckScript()
        {
            string str = " <script language=JavaScript>";
            return ((((((((str + " function CheckClickAll()") + " {" + " var frm = document.[frmID];") + "  for(i=0;i< frm.length;i++)" + "  {") + "         e=frm.elements[i];" + "        if(e.type=='checkbox' && e.name.indexOf('checkboxCol') != -1)") + "           {" + "            e.checked= true ;") + "             b = e.parentNode.parentNode;" + "             b.style.background = '#FBFBF4';") + "            }" + "  }") + " }" + "  </script>");
        }

        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            base.InitializeCell(cell, cellType, rowState, rowIndex);
            if (cell == null)
            {
                throw new ArgumentNullException("cell");
            }
            CheckBox child = new CheckBox();
            switch (cellType)
            {
                case DataControlCellType.Header:
                {
                    Label label = new Label {
                        Text = this.text,
                        ID = "label"
                    };
                    cell.Controls.Add(label);
                    cell.Width = Unit.Pixel(this.HeadWidth);
                    break;
                }
                case DataControlCellType.Footer:
                    break;

                case DataControlCellType.DataCell:
                    child.ID = "checkboxCol";
                    cell.Controls.Add(child);
                    cell.Width = Unit.Pixel(5);
                    return;

                default:
                    return;
            }
        }

        public static void RegisterClientCheckEvents(Page pg, string formID)
        {
            if (pg == null)
            {
                throw new ArgumentNullException("pg");
            }
            ClientScriptManager clientScript = pg.ClientScript;
            string checkHeadReverseScript = GetCheckHeadReverseScript();
            string clickCheckScript = GetClickCheckScript();
            if (!clientScript.IsClientScriptBlockRegistered("clientCheckHeadReverse"))
            {
                clientScript.RegisterClientScriptBlock(pg.GetType(), "clientCheckHeadReverse", checkHeadReverseScript.Replace("[frmID]", formID));
            }
            if (!clientScript.IsClientScriptBlockRegistered("clickCheckScript"))
            {
                clientScript.RegisterClientScriptBlock(pg.GetType(), "clickCheckScript", clickCheckScript.Replace("[frmID]", formID));
            }
        }

        public int HeadWidth
        {
            get
            {
                return this.headWidth;
            }
            set
            {
                this.headWidth = value;
            }
        }
    }
}

