namespace Hidistro.UI.Common.Controls
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class ButtonWrapper : IButton, IText
    {
        private Button AAZ0JeEma(2x58C7QQ)d9L4MH;

        public event EventHandler Click
        {
            add
            {
                this.AAZ0JeEma(2x58C7QQ)d9L4MH.Click += value;
            }
            remove
            {
                this.AAZ0JeEma(2x58C7QQ)d9L4MH.Click -= value;
            }
        }

        public event CommandEventHandler Command
        {
            add
            {
                this.AAZ0JeEma(2x58C7QQ)d9L4MH.Command += value;
            }
            remove
            {
                this.AAZ0JeEma(2x58C7QQ)d9L4MH.Command -= value;
            }
        }

        internal ButtonWrapper(Button button)
        {
            this.AAZ0JeEma(2x58C7QQ)d9L4MH = button;
        }

        public AttributeCollection Attributes
        {
            get
            {
                return this.AAZ0JeEma(2x58C7QQ)d9L4MH.Attributes;
            }
        }

        public bool CausesValidation
        {
            get
            {
                return this.AAZ0JeEma(2x58C7QQ)d9L4MH.CausesValidation;
            }
            set
            {
                this.AAZ0JeEma(2x58C7QQ)d9L4MH.CausesValidation = value;
            }
        }

        public string CommandArgument
        {
            get
            {
                return this.AAZ0JeEma(2x58C7QQ)d9L4MH.CommandArgument;
            }
            set
            {
                this.AAZ0JeEma(2x58C7QQ)d9L4MH.CommandArgument = value;
            }
        }

        public string CommandName
        {
            get
            {
                return this.AAZ0JeEma(2x58C7QQ)d9L4MH.CommandName;
            }
            set
            {
                this.AAZ0JeEma(2x58C7QQ)d9L4MH.CommandName = value;
            }
        }

        public System.Web.UI.Control Control
        {
            get
            {
                return this.AAZ0JeEma(2x58C7QQ)d9L4MH;
            }
        }

        public string Text
        {
            get
            {
                return this.AAZ0JeEma(2x58C7QQ)d9L4MH.Text;
            }
            set
            {
                this.AAZ0JeEma(2x58C7QQ)d9L4MH.Text = value;
            }
        }

        public bool Visible
        {
            get
            {
                return this.AAZ0JeEma(2x58C7QQ)d9L4MH.Visible;
            }
            set
            {
                this.AAZ0JeEma(2x58C7QQ)d9L4MH.Visible = value;
            }
        }
    }
}

