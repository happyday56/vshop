namespace ASPNET.WebControls
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class BindableLabel : Label
    {
        private string dataField;
        private string format;
        private string nullToDisplay = "-";

        protected override void OnDataBinding(EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.DataField))
            {
                object obj2 = DataBinder.Eval(this.Page.GetDataItem(), this.DataField);
                if ((obj2 != null) && (obj2 != DBNull.Value))
                {
                    base.Text = string.IsNullOrEmpty(this.Format) ? obj2.ToString() : string.Format(this.Format, obj2);
                }
            }
        }

        public string DataField
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

        public string Format
        {
            get
            {
                return this.format;
            }
            set
            {
                this.format = value;
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
    }
}

