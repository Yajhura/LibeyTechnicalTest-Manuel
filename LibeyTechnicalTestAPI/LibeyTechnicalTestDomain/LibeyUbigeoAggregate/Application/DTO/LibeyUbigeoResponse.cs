
namespace LibeyTechnicalTestDomain.LibeyUbigeoAggregate.Application.DTO
{
    public sealed class LibeyUbigeoResponse
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public LibeyUbigeoResponse()
        {
            Code = string.Empty;
            Name = string.Empty;
        }

        public LibeyUbigeoResponse(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
