using LibeyTechnicalTestDomain.Abstractions;
using LibeyTechnicalTestDomain.LibeyUbigeoAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUbigeoAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.Shared;
using LibeyTechnicalTestDomain.Validations;
using Microsoft.Extensions.Logging;

namespace LibeyTechnicalTestDomain.LibeyUbigeoAggregate.Application
{
    public sealed class LibeyUbigeoAggregate : ILibeyUbigeoAggregate
    {
        private readonly ILibeyUbigeoRepository _repository;
        private readonly ILogger<LibeyUbigeoAggregate> _logger;
        private readonly ValidatorService _validator;

        public LibeyUbigeoAggregate(ILibeyUbigeoRepository repository, ValidatorService validatorService, ILogger<LibeyUbigeoAggregate> logger)
        {
            _repository = repository;
            _logger = logger;
            _validator = validatorService;
        }


        public async Task<ResultQuery<LibeyUbigeoResponse>> FindAll(LibeyUbigeoQueryInput queryInput)
        {
            try
            {
                _validator.ValidateEntity<LibeyUbigeoQueryInput>(queryInput, "");

                if (_validator.HasErrors())
                    return Result.FailureQuery<LibeyUbigeoResponse>(_validator.GetFinalStatus().Message);

                var result = await _repository.FindAll(queryInput);

                if (result.Value is null)
                    return Result.FailureQuery<LibeyUbigeoResponse>(string.Format(Messages.NotFoundAllMale, Messages.Ubigeo));


                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.ErrorInternalServer);
                return Result.FailureQuery<LibeyUbigeoResponse>(Messages.ErrorInternalServer);
            }

        }
    }
}
