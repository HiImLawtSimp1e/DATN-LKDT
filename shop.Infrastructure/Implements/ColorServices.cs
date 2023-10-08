using Microsoft.EntityFrameworkCore;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels;
using shop.Application.ViewModels.Colors;
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

        return new ApiResponse<List<ColorDto>>
        {
            Success = true,
            Message = "Get all colors successfully",
            Data = result
        };
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

        return new ApiResponse<bool>
        {
            Success = true,
            Message = "Create new color successfully",
            Data = true,
        };
    }
}