namespace Hidistro.UI.Common.Validator
{
    using System;
    using System.Globalization;

    public class NumberRangeClientValidator : ClientValidator
    {
        private int maxValue = 0x7fffffff;
        private int minValue = -2147483648;

        internal override ValidateRenderControl GenerateAppendScript()
        {
            return new ValidateRenderControl { Text = string.Format(CultureInfo.InvariantCulture, "appendValid(new NumberRangeValidator('{0}', {1}, {2}, '{3}'));", new object[] { base.Owner.TargetClientId, this.MinValue, this.MaxValue, this.ErrorMessage }) };
        }

        internal override ValidateRenderControl GenerateInitScript()
        {
            return new ValidateRenderControl();
        }

        public int MaxValue
        {
            get
            {
                return this.maxValue;
            }
            set
            {
                this.maxValue = value;
            }
        }

        public int MinValue
        {
            get
            {
                return this.minValue;
            }
            set
            {
                this.minValue = value;
            }
        }
    }
}

