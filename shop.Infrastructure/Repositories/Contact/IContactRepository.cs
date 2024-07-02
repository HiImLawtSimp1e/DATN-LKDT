using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shop.Domain.Entities;
using shop.Infrastructure.Model;
using shop.Infrastructure.Model.Common.Pagination;

namespace shop.Infrastructure.Repositories.Contact
{
    public interface IContactRepository
    {
        /*public Task<Pagination<RankEntity>> GetAllAsync(VirtualItemQueryModel virtualItemQueryModel);
        public Task<List<RankEntity>> ListAllAsync(VirtualItemQueryModel virtualItemQueryModel);*/
        public Task<ContactEntity> SaveAsync(ContactEntity contactEntity);
        public Task<List<ContactEntity>> SaveAsync(List<ContactEntity> contactEntity);
        public Task<ContactEntity> FindAsync(Guid Id);
        public Task<ContactEntity> DeleteAsync(Guid Id);
        public Task<List<ContactEntity>> DeleteAsync(List<Guid> Id);
    }
}
