namespace Hidistro.UI.Common.Validator
{
    using System;
    using System.Globalization;

    public class InputNumberClientValidator : ClientValidator
    {
        internal override ValidateRenderControl GenerateAppendScript()
        {
            return new ValidateRenderControl();
        }

        internal override ValidateRenderControl GenerateInitScript()
        {
            return new ValidateRenderControl { Text = string.Format(CultureInfo.InvariantCulture, "initValid(new InputValidator('{0}', 1, 10, {1}, '{2}', '{3}', '{4}'))", new object[] { base.Owner.TargetClientId, base.Owner.Nullable ? "true" : "false", @"-?[0-9]\\d*", string.Empty, this.ErrorMessage }) };
        }
    }
}

