using shop.Domain.Entities;
using shop.Infrastructure.Model.Common.Pagination;
using shop.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace shop.Infrastructure.Business.Discount
{
    public interface IDiscountBusiness
    {
        /*public Task<Pagination<VirtualItemEntity>> GetAllAsync(VirtualItemQueryModel virtualItemQueryModel);
        public Task<List<VirtualItemEntity>> ListAllAsync(VirtualItemQueryModel virtualItemQueryModel);*/
        public Task<DiscountEntity> SaveAsync(DiscountEntity discountEntity);
        public Task<DiscountEntity> PatchAsync(DiscountEntity discountEntity);
        public Task<List<DiscountEntity>> SaveAsync(List<DiscountEntity> discountEntity);
        public Task<DiscountEntity> FindAsync(Guid Id);
        public Task<DiscountEntity> DeleteAsync(Guid Id);
        public Task<List<DiscountEntity>> DeleteAsync(List<Guid> Id);
    }
}
