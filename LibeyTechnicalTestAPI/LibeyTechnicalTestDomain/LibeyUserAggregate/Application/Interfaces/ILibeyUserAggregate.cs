using LibeyTechnicalTestDomain.Abstractions;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface ILibeyUserAggregate
    {
        Task<Result<LibeyUserResponse>> FindResponse(string documentNumber);
        Task<Result> Create(UserUpdateorCreateCommand command);
        Task<Result> Update(UserUpdateorCreateCommand command);
        Task<Result> Delete(string documentNumber);
        Task<ResultQuery<LibeyUserResponse>> FindAll(LyberUserQueryInput query);
    }
}