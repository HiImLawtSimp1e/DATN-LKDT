using Microsoft.EntityFrameworkCore;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.Sizes;
using shop.Application.ViewModels.Sizes;
using shop.Application.ViewModels.Sizes;
using shop.Application.ViewModels.Sizes.Queries;
using shop.Application.ViewModels.Sizes.Requests;
using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Implements
{
    public class SizeServices : ISizeService
    {
        private readonly AppDbContext _dbContext;

        public SizeServices(AppDbContext dbContext)
        {
                _dbContext = dbContext;
        }
        public async Task<ApiResponse<bool>> CreateSize(SizeCreateRequest request)
        {
            var newSize= new Size
            {
                Id = Guid.NewGuid(),
                SizeNumber = request.SizeNumber,
            };

            await _dbContext.Sizes.AddAsync(newSize);
            await _dbContext.SaveChangesAsync();

            return new ApiSuccessResponse<bool>("create new Sizesuccess", true);
        }

        public async Task<ApiResponse<bool>> DeleteSize(SizeDeleteRequest request)
        {
            var query = await _dbContext.Sizes.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (query == null)
            {
                return new ApiSuccessResponse<bool>("Size does not exist", false);
            }

            query.DeletedDate = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResponse<bool>("Delete size success", true);
        }

        public async Task<ApiResponse<List<SizeDto>>> GetAllSizes()
        {
            var query = from c in _dbContext.Sizes
                        select new SizeDto
                        {
                            Id = c.Id,
                            SizeNumber = c.SizeNumber
                        };

            var result = await query.ToListAsync();

            return new ApiSuccessResponse<List<SizeDto>>("Get all Sizes successfully", result);
        }

        public async Task<ApiResponse<SizeDto>> GetSizeById(SizeGetByIdRequest request)
        {
            var SizeData = await GetAllSizes();
            var result = SizeData.ResultObject.FirstOrDefault(c => c.Id == request.Id);

            if (result == null)
            {
                return new ApiResponse<SizeDto>()
                {
                    IsSuccessed = false,
                    Message = "Size do not exist",
                };
            }

            return new ApiResponse<SizeDto>()
            {
                IsSuccessed = true,
                Message = "Create size sucessfully",
                ResultObject = result
            };
        }

        public async Task<ApiResponse<bool>> UpdateSize(SizeUpdateRequest request)
        {
            var query = await _dbContext.Sizes.FirstOrDefaultAsync(c => c.SizeNumber == request.SizeNumber);

            if (query == null)
            {
                return new ApiSuccessResponse<bool>("Size does not exist", false);
            }

            query.SizeNumber = request.SizeNumber;
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResponse<bool>("Update size success", true);
        }
    }
}
