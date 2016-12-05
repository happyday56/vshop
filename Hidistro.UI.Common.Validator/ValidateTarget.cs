namespace Hidistro.UI.Common.Validator
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Globalization;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class ValidateTarget : WebControl, INamingContainer
    {
        private string containerId;
        private string controlToValidate;
        private bool nullable;
        private string targetClientId;
        private string validateGroup = "default";
        private ClientValidatorCollection validatorCollection;
        private ArrayList validators;

        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            if (string.IsNullOrEmpty(this.ControlToValidate))
            {
                throw new ArgumentNullException("ControlToValidate");
            }
            WebControl control = (WebControl) this.NamingContainer.FindControl(this.ControlToValidate);
            if ((control != null) && (this.Validators.Count > 0))
            {
                this.targetClientId = control.ClientID;
                if (!(control is RadioButtonList))
                {
                    control.Attributes.Add("ValidateGroup", this.ValidateGroup);
                }
                if (!string.IsNullOrEmpty(this.ContainerId))
                {
                    ValidatorContainer container = (ValidatorContainer) this.Page.FindControl(this.ContainerId);
                    if (container == null)
                    {
                        container = this.FindFromMasterPage();
                    }
                    if (container == null)
                    {
                        throw new Exception(string.Format(CultureInfo.InvariantCulture, "The validator container: '{0}' was not found", new object[] { this.ContainerId }));
                    }
                    this.CreateToContainer(container);
                }
                else
                {
                    this.CreateToChilds();
                }
            }
        }

        private void CreateToChilds()
        {
            this.Validators[0].SetOwner(this);
            this.Controls.Add(this.Validators[0].GenerateInitScript());
            for (int i = 1; i < this.Validators.Count; i++)
            {
                this.Validators[i].SetOwner(this);
                this.Controls.Add(this.Validators[i].GenerateAppendScript());
            }
        }

        private void CreateToContainer(ValidatorContainer container)
        {
            if (container != null)
            {
                this.Validators[0].SetOwner(this);
                container.AddValidatorControl(this.Validators[0].GenerateInitScript());
                for (int i = 1; i < this.Validators.Count; i++)
                {
                    this.Validators[i].SetOwner(this);
                    container.AddValidatorControl(this.Validators[i].GenerateAppendScript());
                }
            }
        }

        private ValidatorContainer FindFromMasterPage()
        {
            Control namingContainer = this.NamingContainer;
            ValidatorContainer container = (ValidatorContainer) namingContainer.FindControl(this.ContainerId);
            while ((container == null) && (namingContainer.Parent != null))
            {
                namingContainer = namingContainer.Parent;
                container = (ValidatorContainer) namingContainer.FindControl(this.ContainerId);
            }
            return container;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.HasControls())
            {
                this.RenderBeginTag(writer);
                for (int i = 0; i < this.Controls.Count; i++)
                {
                    this.Controls[i].RenderControl(writer);
                    writer.WriteLine();
                }
                this.RenderEndTag(writer);
            }
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.WriteLine("<script type=\"text/javascript\" language=\"javascript\">");
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.WriteLine("</script>");
        }

        public string ContainerId
        {
            get
            {
                return this.containerId;
            }
            set
            {
                this.containerId = value;
            }
        }

        public string ControlToValidate
        {
            get
            {
                return this.controlToValidate;
            }
            set
            {
                this.controlToValidate = value;
            }
        }

        public bool Nullable
        {
            get
            {
                return this.nullable;
            }
            set
            {
                this.nullable = value;
            }
        }

        [Browsable(false)]
        public string TargetClientId
        {
            get
            {
                return this.targetClientId;
            }
        }

        public string ValidateGroup
        {
            get
            {
                return this.validateGroup;
            }
            set
            {
                this.validateGroup = value;
            }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ClientValidatorCollection Validators
        {
            get
            {
                if (this.validatorCollection == null)
                {
                    this.validators = new ArrayList();
                    this.validatorCollection = new ClientValidatorCollection(this, this.validators);
                }
                return this.validatorCollection;
            }
        }
    }
}

