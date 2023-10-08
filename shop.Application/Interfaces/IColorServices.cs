using shop.Application.Common;
using shop.Application.ViewModels;
using shop.Application.ViewModels.Colors;

namespace shop.Application.Interfaces;

public interface IColorServices
{
    Task<ApiResponse<List<ColorDto>>> GetAllColors();
    Task<ApiResponse<bool>> CreateColor(ColorCreateRequest request);
    
}