using LibeyTechnicalTestDomain.Abstractions;
using LibeyTechnicalTestDomain.Validations;

namespace LibeyTechnicalTestDomain.LibeyUbigeoAggregate.Application.DTO
{
    public class LibeyUbigeoQueryInput : QueryInput
    {
        [TextValidator(validSQL: true, validXSS: true, maxLength: 6,messageEntity: "Codigo")]
        public string Code { get; set; }

        public LibeyUbigeoQueryInput() : base()
        {
            Code = string.Empty;
        }
    }
}
