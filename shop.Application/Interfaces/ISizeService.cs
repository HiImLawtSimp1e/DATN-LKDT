using shop.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shop.Application.ViewModels.Sizes.Queries;
using shop.Application.ViewModels.Sizes.Requests;
using shop.Application.ViewModels.Sizes;

namespace shop.Application.Interfaces
{
    public interface ISizeService
    {
        Task<ApiResponse<List<SizeDto>>> GetAllSizes();
        Task<ApiResponse<SizeDto>> GetSizeById(SizeGetByIdRequest request);
        Task<ApiResponse<bool>> CreateSize(SizeCreateRequest request);
        Task<ApiResponse<bool>> UpdateSize(SizeUpdateRequest request);
        Task<ApiResponse<bool>> DeleteSize(SizeDeleteRequest request);
    }
}

