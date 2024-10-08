using LibeyTechnicalTestDomain.Abstractions;
using LibeyTechnicalTestDomain.EFCore;
using LibeyTechnicalTestDomain.LibeyUbigeoAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUbigeoAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUbigeoAggregate.Domain;
using Microsoft.EntityFrameworkCore;

namespace LibeyTechnicalTestDomain.LibeyUbigeoAggregate.Infrastructure
{
    public sealed class LibeyUbigeoRepository : ILibeyUbigeoRepository
    {
        private readonly Context _context;

        public LibeyUbigeoRepository(Context context)
        {
            _context = context;
        }

        public async Task<ResultQuery<LibeyUbigeoResponse>> FindAll(LibeyUbigeoQueryInput queryInput)
        {
            List<LibeyUbigeoResponse> results = new List<LibeyUbigeoResponse>();
            if (queryInput.Code.Length == 4)
            {
                var query = _context.Ubigeos.AsQueryable();
                if (!string.IsNullOrEmpty(queryInput.Code))
                {
                    query = query.Where(x => x.ProvinceCode == queryInput.Code);
                }
                if (!string.IsNullOrEmpty(queryInput.TextSearch))
                {
                    query = query.Where(x =>
                                            x.UbigeoDescription.Contains(queryInput.TextSearch) ||
                                            x.UbigeoCode.Contains(queryInput.TextSearch));
                }
                int totalCount = await query.CountAsync();
                var pagedQuery = query.Skip((queryInput.Page - 1) * queryInput.PageSize)
                                      .Take(queryInput.PageSize);
                var items = await pagedQuery.Select(ubigeo => new LibeyUbigeoResponse
                {
                    Code = ubigeo.UbigeoCode,
                    Name = ubigeo.UbigeoDescription
                }).ToListAsync();
                results = items;
            }
            else if (queryInput.Code.Length == 2)
            {
                var query = _context.Provinces.AsQueryable();
                if (!string.IsNullOrEmpty(queryInput.Code))
                {
                    query = query.Where(x => x.RegionCode == queryInput.Code);
                }
                if (!string.IsNullOrEmpty(queryInput.TextSearch))
                {
                    query = query.Where(x =>
                                            x.ProvinceDescription.Contains(queryInput.TextSearch) ||
                                            x.ProvinceCode.Contains(queryInput.TextSearch));
                }
                int totalCount = await query.CountAsync();
                var pagedQuery = query.Skip((queryInput.Page - 1) * queryInput.PageSize)
                                      .Take(queryInput.PageSize);
                var items = await pagedQuery.Select(province => new LibeyUbigeoResponse
                {
                    Code = province.ProvinceCode,
                    Name = province.ProvinceDescription
                }).ToListAsync();
                results = items;
            }
            else
            {
                var query = _context.Regions.AsQueryable();
                if (!string.IsNullOrEmpty(queryInput.Code))
                {
                    query = query.Where(x => x.RegionCode == queryInput.Code);
                }
                if (!string.IsNullOrEmpty(queryInput.TextSearch))
                {
                    query = query.Where(x =>
                                            x.RegionDescription.Contains(queryInput.TextSearch) ||
                                            x.RegionCode.Contains(queryInput.TextSearch));
                }
                int totalCount = await query.CountAsync();
                var pagedQuery = query.Skip((queryInput.Page - 1) * queryInput.PageSize)
                                      .Take(queryInput.PageSize);
                var items = await pagedQuery.Select(region => new LibeyUbigeoResponse
                {
                    Code = region.RegionCode,
                    Name = region.RegionDescription
                }).ToListAsync();
                results = items;
            }
            return Result.SuccessQuery<LibeyUbigeoResponse>(results, string.Empty, results.Count);
        }


    }
}
