using Microsoft.EntityFrameworkCore;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels;
using shop.Application.ViewModels.Colors;
using shop.Application.ViewModels.Colors.Queries;
using shop.Application.ViewModels.Colors.Requests;
using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using System.Linq;

namespace shop.Infrastructure.Implements;

public class ColorServices : IColorServices
{
    private readonly AppDbContext _dbContext;

    public ColorServices(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResponse<List<ColorDto>>> GetAllColors()
    {
        var query = from c in _dbContext.Colors
                    select new ColorDto
                    {
                        Id = c.Id,
                        Name = c.Name
                    };

        var result = await query.ToListAsync();

        return new ApiSuccessResponse<List<ColorDto>>("Get all colors successfully", result);
    }

    public async Task<ApiResponse<bool>> CreateColor(ColorCreateRequest request)
    {
        var colorNameExit = await _dbContext.Colors.FirstOrDefaultAsync(c=>c.Name.ToLower()==request.Name.ToLower());

        if (colorNameExit==null)
        {
            return new ApiSuccessResponse<bool>("Color name already exit", false);
        }

        var newColor = new Color
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            CreatedDate = DateTime.Now,
        };

        await _dbContext.Colors.AddAsync(newColor);
        await _dbContext.SaveChangesAsync();

        return new ApiSuccessResponse<bool>("create new color success", true);
    }

    public async Task<ApiResponse<ColorDto>> GetColorById(ColorGetByIdRequest request)
    {
        var colorData = await GetAllColors();
        var result = colorData.ResultObject.FirstOrDefault(c => c.Id == request.Id);

        if (result==null)
        {
            return new ApiResponse<ColorDto>()
            {
                IsSuccessed = false,
                Message = "Color do not exist",
            };
        }

        return new ApiResponse<ColorDto>()
        {
            IsSuccessed = true,
            Message = "Create color sucessfully",
            ResultObject = result
        };

    }

    public async Task<ApiResponse<bool>> UpdateColor(Guid ID, ColorUpdateRequest request)
    {
        var existingColor = await _dbContext.Colors.FirstOrDefaultAsync(c => c.Id == ID);

        if (existingColor == null)
        {
            return new ApiSuccessResponse<bool>("Color does not exist", false);
        }

        var duplicateColor = await _dbContext.Colors.FirstOrDefaultAsync(c => c.Name == request.Name && c.Id != ID);
        if (duplicateColor != null)
        {
            return new ApiSuccessResponse<bool>("Color with the same name already exists", false);
        }

        existingColor.Name = request.Name;
        await _dbContext.SaveChangesAsync();

        return new ApiSuccessResponse<bool>("Update color success", true);
    }

    public async Task<ApiResponse<bool>> DeleteColor(ColorDeleteRequest request)
    {
        var queryResult = await _dbContext.Colors.FirstOrDefaultAsync(c => c.Id == request.Id);

        if (queryResult == null)
        {
            return new ApiSuccessResponse<bool>("Color does not exist", false);
        }

        queryResult.DeletedDate = DateTime.Now;
        queryResult.Status = 0;
        await _dbContext.SaveChangesAsync();
        return new ApiSuccessResponse<bool>("Delete color success", true);
    }
}