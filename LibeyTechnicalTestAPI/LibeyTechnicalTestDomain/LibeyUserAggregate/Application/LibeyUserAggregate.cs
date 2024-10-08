using LibeyTechnicalTestDomain.Abstractions;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using LibeyTechnicalTestDomain.Shared;
using LibeyTechnicalTestDomain.Validations;
using Microsoft.Extensions.Logging;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application
{
    public class LibeyUserAggregate : ILibeyUserAggregate
    {
        private readonly ILibeyUserRepository _repository;
        private readonly ILogger<LibeyUserAggregate> _logger;
        private readonly ValidatorService _validator;
        public LibeyUserAggregate(ILibeyUserRepository repository, ValidatorService validatorService, ILogger<LibeyUserAggregate> logger)
        {
            _repository = repository;
            _logger = logger;
            _validator = validatorService;
        }

        public async Task<Result> Create(UserUpdateorCreateCommand command)
        {
            try
            {
                _validator.ValidateEntity<UserUpdateorCreateCommand>(command, "Crear");

                if (_validator.HasErrors())
                    return Result.Failure(_validator.GetFinalStatus().Message);


                LibeyUser newLibeyUser = LibeyUser.Create(command);

                int resultCreate = await _repository.Create(newLibeyUser);

                if (resultCreate == 0) return Result.Failure(string.Format(Messages.ErrorCreate, Messages.LibeyUser));

                return Result.Success(string.Format(Messages.SavedSuccessfullyMale, Messages.LibeyUser));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.ErrorInternalServer);
                return Result.Failure(Messages.ErrorInternalServer);
            }

        }

        public async Task<Result> Delete(string documentNumber)
        {

            try
            {
                _validator.ValidateId(documentNumber);

                if (_validator.HasErrors())
                    return Result.Failure(_validator.GetFinalStatus().Message);


                LibeyUserResponse? libeyUser = await _repository.FindResponse(documentNumber);

                if (libeyUser == null) return Result.Failure(string.Format(Messages.NotFoundMale, Messages.LibeyUser));

                LibeyUser newLibeyUser = LibeyUser.Create(libeyUser);

                newLibeyUser.Delete();

                int resultDelete = await _repository.Delete(newLibeyUser);

                if (resultDelete == 0) return Result.Failure(string.Format(Messages.ErrorDelete, Messages.LibeyUser));

                return Result.Success(string.Format(Messages.DeletedSuccessfullyMale, Messages.LibeyUser));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.ErrorInternalServer);
                return Result.Failure(Messages.ErrorInternalServer);
            }

        }

        public async Task<ResultQuery<LibeyUserResponse>> FindAll(LyberUserQueryInput query)
        {
            try
            {

                _validator.ValidateEntity<LyberUserQueryInput>(query, "");

                if (_validator.HasErrors())
                    return Result.FailureQuery<LibeyUserResponse>(_validator.GetFinalStatus().Message);


                ResultQuery<LibeyUserResponse> result = await _repository.FindAll(query);

                if (result.Value is null) return Result.FailureQuery<LibeyUserResponse>(string.Format(Messages.NotFoundAllMale, Messages.LibeyUser));


                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.ErrorInternalServer);
                return Result.FailureQuery<LibeyUserResponse>(Messages.ErrorInternalServer);
            }

        }

        public async Task<Result<LibeyUserResponse>> FindResponse(string documentNumber)
        {
            try
            {
                _validator.ValidateId(documentNumber);

                if (_validator.HasErrors())
                    return Result.Failure<LibeyUserResponse>(_validator.GetFinalStatus().Message);

                LibeyUserResponse? result = await _repository.FindResponse(documentNumber);

                if (result is null) return Result.Failure<LibeyUserResponse>(string.Format(Messages.NotFoundMale, Messages.LibeyUser));

                return Result.Success(result, string.Empty);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.ErrorInternalServer);
                return Result.Failure<LibeyUserResponse>(Messages.ErrorInternalServer);
            }
        }

        public async Task<Result> Update(UserUpdateorCreateCommand command)
        {
            try
            {
                _validator.ValidateEntity<UserUpdateorCreateCommand>(command, Messages.Update);

                if (_validator.HasErrors())
                    return Result.Failure(_validator.GetFinalStatus().Message);


                LibeyUserResponse? libeyUser = await _repository.FindResponse(command.DocumentNumber!);

                if (libeyUser == null) return Result.Failure(string.Format(Messages.NotFoundMale, Messages.LibeyUser));

                LibeyUser newLibeyUser = LibeyUser.Create(libeyUser);

                newLibeyUser.Update(command);

                int resultUpdate = await _repository.Update(newLibeyUser);

                if (resultUpdate == 0) return Result.Failure(string.Format(Messages.ErrorUpdate, Messages.LibeyUser));

                return Result.Success(string.Format(Messages.UpdatedSuccessfullyMale, Messages.LibeyUser));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.ErrorInternalServer);
                return Result.Failure(Messages.ErrorInternalServer);

            }

        }
    }
}