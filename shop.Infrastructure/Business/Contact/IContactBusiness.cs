using shop.Domain.Entities;
using shop.Infrastructure.Model.Common.Pagination;
using shop.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Business.Contact
{
    public interface IContactBusiness
    {
        /*public Task<Pagination<VirtualItemEntity>> GetAllAsync(VirtualItemQueryModel virtualItemQueryModel);
        public Task<List<VirtualItemEntity>> ListAllAsync(VirtualItemQueryModel virtualItemQueryModel);*/
        public Task<ContactEntity> SaveAsync(ContactEntity contactEntity);
        public Task<ContactEntity> PatchAsync(ContactEntity contactEntity);
        public Task<List<ContactEntity>> SaveAsync(List<ContactEntity> contactEntity);
        public Task<ContactEntity> FindAsync(Guid Id);
        public Task<ContactEntity> DeleteAsync(Guid Id);
        public Task<List<ContactEntity>> DeleteAsync(List<Guid> Id);
    }
}
