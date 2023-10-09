using Microsoft.EntityFrameworkCore;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels;
using shop.Application.ViewModels.Colors;
using shop.Application.ViewModels.Colors.Requests;
using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;

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
        var newColor = new Color
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
        };

        await _dbContext.Colors.AddAsync(newColor);
        await _dbContext.SaveChangesAsync();

        return new ApiSuccessResponse<bool>("create new color success", true);
    }
}