namespace Hidistro.UI.Common.Validator
{
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class ClientValidator
    {
        private string errorMessage;
        private ValidateTarget owner;

        protected ClientValidator()
        {
        }

        internal abstract ValidateRenderControl GenerateAppendScript();
        internal abstract ValidateRenderControl GenerateInitScript();
        internal void SetOwner(ValidateTarget owner)
        {
            this.owner = owner;
        }

        public virtual string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
            set
            {
                this.errorMessage = value;
            }
        }

        protected ValidateTarget Owner
        {
            get
            {
                return this.owner;
            }
        }
    }
}

