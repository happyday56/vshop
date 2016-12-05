namespace Hishop.Components.Validation
{
    using System.Collections.Generic;

    internal interface IValidatedType : IValidatedElement
    {
        IEnumerable<MethodInfo> GetSelfValidationMethods();
        IEnumerable<IValidatedElement> GetValidatedFields();
        IEnumerable<IValidatedElement> GetValidatedMethods();
        IEnumerable<IValidatedElement> GetValidatedProperties();
    }
}

