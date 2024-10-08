using LibeyTechnicalTestDomain.Validations;

namespace LibeyTechnicalTestDomain.Abstractions
{
    public abstract class QueryInput
    {
        [TextValidator(validSQL: true, validXSS: true, maxLength: 100)]
        public string TextSearch { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        [TextValidator(validSQL: true, validXSS: true, maxLength: 20)]
        public string OrderBy { get; set; }

        [TextValidator(validSQL: true, validXSS: true, maxLength: 10)]
        public string OrderDirection { get; set; }

        protected QueryInput()
        {
            TextSearch = string.Empty;
            Page = 1;
            PageSize = 10;
            OrderBy = string.Empty;
            OrderDirection = string.Empty;
        }
    }
}
