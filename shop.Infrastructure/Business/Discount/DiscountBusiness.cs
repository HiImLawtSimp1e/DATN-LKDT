using shop.Domain.Entities;
using shop.Infrastructure.Intercepter;
using shop.Infrastructure.Model;
using shop.Infrastructure.Model.Common.Pagination;
using shop.Infrastructure.Repositories.Discount;
using shop.Infrastructure.Repositories.Rank;
using shop.Infrastructure.Repositories.VirtualItem;
using shop.Infrastructure.Utilities;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace shop.Infrastructure.Business.Discount
{
    public class DiscountBusiness : IDiscountBusiness
    {
        private readonly IDiscountRepository _discountRepository;
        public DiscountBusiness(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<DiscountEntity> DeleteAsync(Guid Id)
        {
            var res = await DeleteAsync(new List<Guid> { Id });
            return res.FirstOrDefault();
        }

        public async Task<List<DiscountEntity>> DeleteAsync(List<Guid> ids)
        {
            var res = await _discountRepository.DeleteAsync(ids);
            return res;
        }

        public async Task<DiscountEntity> FindAsync(Guid Id)
        {
            var res = await _discountRepository.FindAsync(Id);
            return res;
        }

        public async Task<DiscountEntity> PatchAsync(DiscountEntity discountEntity)
        {
            throw new NotImplementedException();
        }

        public async Task<DiscountEntity> SaveAsync(DiscountEntity discountEntity)
        {
            var res = await SaveAsync(new List<DiscountEntity> { discountEntity });
            return res.FirstOrDefault();
        }

        public async Task<List<DiscountEntity>> SaveAsync(List<DiscountEntity> discountEntity)
        {
            var result = await _discountRepository.SaveAsync(discountEntity);
            return result;
        }
    }
}
