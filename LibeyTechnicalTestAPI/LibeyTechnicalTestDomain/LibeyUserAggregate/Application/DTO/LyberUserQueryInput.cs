using LibeyTechnicalTestDomain.Abstractions;
using LibeyTechnicalTestDomain.Validations;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO
{
    public sealed class LyberUserQueryInput : QueryInput
    {
        [TextValidator(validSQL: true, validXSS: true, maxLength: 20)]
        public string DocumentNumber { get; set; }

        [TextValidator(validSQL: true, validXSS: true, maxLength: 100)]
        public string Email { get; set; }

        public LyberUserQueryInput() : base()
        {
            DocumentNumber = string.Empty;
            Email = string.Empty;
        }
    }
}
