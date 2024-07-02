using shop.Domain.Entities;
using shop.Infrastructure.Model;
using shop.Infrastructure.Model.Common.Pagination;
using shop.Infrastructure.Repositories.WarrantyCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Business.WarrantyCard
{
    public class WarrantyCardBusiness : IWarrantyCardBusiness
    {
        private readonly IWarrantyCardRepository _warrantyCardRepository;

        public WarrantyCardBusiness(IWarrantyCardRepository warrantyCardRepository)
        {
            _warrantyCardRepository=warrantyCardRepository;
        }

        public async Task<WarrantyCardEntity> DeleteAsync(Guid Id)
        {
            return await _warrantyCardRepository.DeleteAsync(Id);
        }

        public async Task<List<WarrantyCardEntity>> DeleteAsync(List<Guid> Ids)
        {
            return await _warrantyCardRepository.DeleteAsync(Ids);
        }

        public async Task<WarrantyCardEntity> FindAsync(Guid Id)
        {
           return await _warrantyCardRepository.FindAsync(Id);
        }

        public async Task<Pagination<WarrantyCardEntity>> GetAllAsync(WarrantyCardQueryModel warrantyCardQueryModel)
        {
           return await _warrantyCardRepository.GetAllAsync(warrantyCardQueryModel);
        }

        public async Task<List<WarrantyCardEntity>> ListAllAsync(WarrantyCardQueryModel warrantyCardQueryModel)
        {
         return await _warrantyCardRepository.ListAllAsync(warrantyCardQueryModel);
        }

        public async Task<WarrantyCardEntity> SaveAsync(WarrantyCardEntity warrantyCardEntity)
        {
            return await _warrantyCardRepository.SaveAsync(warrantyCardEntity);    
        }

        public async Task<List<WarrantyCardEntity>> SaveAsync(List<WarrantyCardEntity> warrantyCardEntities)
        {
            return await _warrantyCardRepository.SaveAsync(warrantyCardEntities);
        }
    }
}
