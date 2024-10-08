using LibeyTechnicalTestDomain.Abstractions;
using LibeyTechnicalTestDomain.EFCore;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using Microsoft.EntityFrameworkCore;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Infrastructure
{
    public class LibeyUserRepository : ILibeyUserRepository
    {
        private readonly Context _context;
        public LibeyUserRepository(Context context)
        {
            _context = context;
        }

        public async Task<int> Create(LibeyUser command)
        {
            _context.Add<LibeyUser>(command);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(LibeyUser command)
        {
            _context.Remove<LibeyUser>(command);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(LibeyUser command)
        {
            _context.Update<LibeyUser>(command);
            return await _context.SaveChangesAsync();
        }


        public async Task<ResultQuery<LibeyUserResponse>> FindAll(LyberUserQueryInput queryInput)
        {

            var query = _context.LibeyUsers.AsQueryable();

            if (!string.IsNullOrEmpty(queryInput.DocumentNumber))
                query = query.Where(x => x.DocumentNumber.Contains(queryInput.DocumentNumber));

            if (!string.IsNullOrEmpty(queryInput.Email))
                query = query.Where(x => x.Email.Contains(queryInput.Email));

            if (!string.IsNullOrEmpty(queryInput.TextSearch))
                query = query.Where(x => x.Name.Contains(queryInput.TextSearch) ||
                                         x.FathersLastName.Contains(queryInput.TextSearch) ||
                                         x.MothersLastName.Contains(queryInput.TextSearch));

            int totalCount = await query.CountAsync();

            var pagedQuery = query.Skip((queryInput.Page - 1) * queryInput.PageSize)
                                  .Take(queryInput.PageSize);

            var items = await pagedQuery.Select(libeyUser => new LibeyUserResponse
            {
                DocumentNumber = libeyUser.DocumentNumber,
                Active = libeyUser.Active,
                Address = libeyUser.Address,
                DocumentTypeId = libeyUser.DocumentTypeId,
                Email = libeyUser.Email,
                FathersLastName = libeyUser.FathersLastName,
                MothersLastName = libeyUser.MothersLastName,
                Name = libeyUser.Name,
                Password = libeyUser.Password,
                Phone = libeyUser.Phone
            }).ToListAsync();

            return Result.SuccessQuery<LibeyUserResponse>(items, string.Empty, totalCount);

        }

        public async Task<LibeyUserResponse?> FindResponse(string documentNumber)
        {
            var q = from libeyUser in _context.LibeyUsers.Where(x => x.DocumentNumber.Equals(documentNumber))
                    select new LibeyUserResponse()
                    {
                        DocumentNumber = libeyUser.DocumentNumber,
                        Active = libeyUser.Active,
                        Address = libeyUser.Address,
                        DocumentTypeId = libeyUser.DocumentTypeId,
                        Email = libeyUser.Email,
                        FathersLastName = libeyUser.FathersLastName,
                        MothersLastName = libeyUser.MothersLastName,
                        Name = libeyUser.Name,
                        Password = libeyUser.Password,
                        Phone = libeyUser.Phone,
                        UbigeoCode = libeyUser.UbigeoCode
                    };







            var list = await q.ToListAsync();

            var result = list.FirstOrDefault();

            if (result != null)
            {
                var province = await _context.Provinces
                    .Where(x => x.ProvinceCode == result!.UbigeoCode.Substring(2))
                    .Select(x => x.ProvinceCode)
                    .FirstOrDefaultAsync();

                var region = await _context.Regions
                    .Where(x => x.RegionCode == result!.UbigeoCode.Substring(0, 2))
                    .Select(x => x.RegionCode)
                    .FirstOrDefaultAsync();

                result.ProvinceCode = province;
                result.RegionCode = region;

                return result;
            }
            else
            {
                return result;
            }
        }
    }
}