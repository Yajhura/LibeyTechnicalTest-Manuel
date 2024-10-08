using LibeyTechnicalTestDomain.Validations;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO
{
    public record UserUpdateorCreateCommand
    {
        [TextValidator(validSQL: true, validXSS: true, maxLength: 20, messageEntity: "DocumentNumber", isRequeridFiled: true)]
        public string? DocumentNumber { get; init; }
        public int DocumentTypeId { get; init; }

        [TextValidator(validSQL: true, validXSS: true, maxLength: 100, messageEntity: "Name", isRequeridFiled: true)]
        public string? Name { get; init; }

        [TextValidator(validSQL: true, validXSS: true, maxLength: 100, messageEntity: "FathersLastName", isRequeridFiled: true)]
        public string? FathersLastName { get; init; }

        [TextValidator(validSQL: true, validXSS: true, maxLength: 100, messageEntity: "MothersLastName", isRequeridFiled: true)]
        public string? MothersLastName { get; init; }

        [TextValidator(validSQL: true, validXSS: true, maxLength: 100, messageEntity: "Address")]
        public string? Address { get; init; }

        [TextValidator(validSQL: true, validXSS: true, maxLength: 6, messageEntity: "UbigeoCode")]
        public string? UbigeoCode { get; init; }

        [TextValidator(validSQL: true, validXSS: true, maxLength: 9, messageEntity: "Phone")]
        public string? Phone { get; init; }

        [TextValidator(validSQL: true, validXSS: true, maxLength: 100, messageEntity: "Email")]
        public string? Email { get; init; }

        [TextValidator(validSQL: true, validXSS: true, maxLength: 100, messageEntity: "Password")]
        public string? Password { get; init; }
        public bool Active { get; init; }
    }
}