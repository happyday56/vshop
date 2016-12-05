namespace ASPNET.WebControls
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class CheckBoxColumn : BoundField
    {
        private int cellWidth = 5;
        public const string DataCellCheckBoxID = "checkboxCol";
        public const string HeaderCheckBoxID = "checkboxHead";
        private int headWidth = 30;
        private string text = "選擇";
        private bool visible;

        private static string GetCheckColScript()
        {
            string str = " <script language=JavaScript>";
            return ((((((((((((str + " function CheckAll( checkAllBox )" + " {") + "   var frm = document.[frmID];" + "   var ChkState=checkAllBox.checked;") + "   for(i=0;i< frm.length;i++)" + "    {") + "         e=frm.elements[i];" + "        if(e.type=='checkbox' && e.name.indexOf('checkboxCol') != -1)") + "         {" + "            e.checked= ChkState ;") + "          if( ChkState == true)" + "           {") + "             b = e.parentNode.parentNode;" + "             b.style.background = '#FBFBF4';") + "           }" + "          else ") + "           {" + "             b = e.parentNode.parentNode;") + "             b.style.background = '#ffffff';" + "           }") + "        }" + "    }") + " }" + "  </script>");
        }

        private static string GetCheckHeadReverseScript()
        {
            string str = " <script language=JavaScript>";
            return (((((((((((((str + " function CheckReverse()" + " {") + " var frm = document.[frmID];" + "  var boolAllChecked;") + "  boolAllChecked=true;" + "  for(i=0;i< frm.length;i++)") + "  {" + "         e=frm.elements[i];") + "     if(e.type=='checkbox' && e.name.indexOf('checkboxCol') != -1)" + "        {") + "         if( e.checked== false)" + "           {") + "              e.checked = true;" + "              b = e.parentNode.parentNode;") + "              b.style.background = '#FBFBF4';" + "           }") + "          else " + "           {") + "             e.checked = false;" + "             b = e.parentNode.parentNode;") + "             b.style.background = '#ffffff';" + "            }") + "          }" + "    }") + " }" + "  </script>");
        }

        private static string GetCheckHeadScript()
        {
            string str = "";
            str = "<script language=JavaScript>";
            return (((((((((((str + "function CheckChanged()" + "{") + "  var frm = document.[frmID];" + "  var boolAllChecked;") + "  boolAllChecked=true;" + "  for(i=0;i< frm.length;i++)") + "  {" + "    e=frm.elements[i];") + "  if ( e.type=='checkbox' && e.name.indexOf('checkboxCol') != -1 )" + "         if( e.checked == true)") + "           {" + "             b = e.parentNode.parentNode;") + "             b.style.background = '#FBFBF4';" + "           }") + "          else " + "           {") + "             b = e.parentNode.parentNode;" + "             b.style.background = '#ffffff';") + "           }" + "  }") + " }" + " </script>");
        }

        private static string GetClickCheckScript()
        {
            string str = " <script language=JavaScript>";
            return (((((((((str + " function CheckClickAll()") + " {" + " var frm = document.[frmID];") + "  for(i=0;i< frm.length;i++)" + "  {") + "         e=frm.elements[i];" + "        if(e.type=='checkbox' && e.name.indexOf('checkboxCol') != -1)") + "           {" + "            e.checked= true ;") + "             b = e.parentNode.parentNode;" + "             b.style.background = '#FBFBF4';") + "            }" + "        if(e.type=='checkbox' && e.name.indexOf('checkboxHead') != -1)") + "            e.checked= true ;" + "  }") + " }" + "  </script>");
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
                    if (!this.ShowHead)
                    {
                        Label label = new Label
                        {
                            Text = this.Text,
                            ID = "label"
                        };
                        cell.Controls.Add(label);
                        break;
                    }
                    child.Text = this.Text;
                    child.ID = "checkboxHead";
                    cell.Controls.Add(child);
                    break;

                case DataControlCellType.Footer:
                    return;

                case DataControlCellType.DataCell:
                    child.ID = "checkboxCol";
                    cell.Controls.Add(child);
                    cell.Width = Unit.Pixel(this.CellWidth);
                    return;

                default:
                    return;
            }
            cell.Width = Unit.Pixel(this.HeadWidth);
        }

        private static void RegisterAttributes(Control ctrl)
        {
            foreach (Control control in ctrl.Controls)
            {
                try
                {
                    if (control.HasControls())
                    {
                        RegisterAttributes(control);
                    }
                    CheckBox box = (CheckBox)control;
                    if ((box != null) && (box.ID == "checkboxCol"))
                    {
                        box.Attributes.Add("onclick", "CheckChanged()");
                    }
                    else if ((box != null) && (box.ID == "checkboxHead"))
                    {
                        box.Attributes.Add("onclick", "CheckAll(this)");
                    }
                }
                catch (InvalidCastException)
                {
                }
            }
        }

        public static void RegisterClientCheckEvents(Page pg, string formID)
        {
            if (pg == null)
            {
                throw new ArgumentNullException("pg");
            }
            ClientScriptManager clientScript = pg.ClientScript;
            string checkHeadScript = GetCheckHeadScript();
            string checkHeadReverseScript = GetCheckHeadReverseScript();
            string clickCheckScript = GetClickCheckScript();
            if (!clientScript.IsClientScriptBlockRegistered("clientScriptCheckAll"))
            {
                clientScript.RegisterClientScriptBlock(pg.GetType(), "clientScriptCheckAll", checkHeadScript.Replace("[frmID]", formID));
            }
            if (!clientScript.IsClientScriptBlockRegistered("clientReverseScript"))
            {
                clientScript.RegisterClientScriptBlock(pg.GetType(), "clientScriptCheckReverse", checkHeadReverseScript.Replace("[frmID]", formID));
            }
            if (!clientScript.IsClientScriptBlockRegistered("clientCheckClickScript"))
            {
                clientScript.RegisterClientScriptBlock(pg.GetType(), "clientCheckClickScript", clickCheckScript.Replace("[frmID]", formID));
            }
            RegisterAttributes(pg);
        }

        public int CellWidth
        {
            get
            {
                return this.cellWidth;
            }
            set
            {
                this.cellWidth = value;
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

        public bool ShowHead
        {
            get
            {
                return this.visible;
            }
            set
            {
                this.visible = value;
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }
    }
}

