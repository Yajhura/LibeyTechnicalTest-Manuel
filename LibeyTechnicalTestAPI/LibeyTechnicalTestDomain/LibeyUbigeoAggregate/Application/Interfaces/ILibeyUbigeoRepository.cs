using LibeyTechnicalTestDomain.Abstractions;
using LibeyTechnicalTestDomain.LibeyUbigeoAggregate.Application.DTO;

namespace LibeyTechnicalTestDomain.LibeyUbigeoAggregate.Application.Interfaces
{
    public interface ILibeyUbigeoRepository
    {
        public Task<ResultQuery<LibeyUbigeoResponse>> FindAll(LibeyUbigeoQueryInput queryInput);
    }
}
