using LibeyTechnicalTestDomain.Abstractions;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface ILibeyUserRepository
    {
        Task<LibeyUserResponse?> FindResponse(string documentNumber);
        Task<int> Create(LibeyUser command);
        Task<int> Update(LibeyUser command);
        Task<int> Delete(LibeyUser command);
        Task<ResultQuery<LibeyUserResponse>> FindAll(LyberUserQueryInput query);
    }
}
