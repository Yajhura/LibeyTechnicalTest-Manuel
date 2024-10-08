using LibeyTechnicalTestDomain.Abstractions;
using LibeyTechnicalTestDomain.LibeyUbigeoAggregate.Application.DTO;

namespace LibeyTechnicalTestDomain.LibeyUbigeoAggregate.Application.Interfaces
{
    public interface ILibeyUbigeoAggregate
    {
        public Task<ResultQuery<LibeyUbigeoResponse>> FindAll(LibeyUbigeoQueryInput queryInput);
    }
}
