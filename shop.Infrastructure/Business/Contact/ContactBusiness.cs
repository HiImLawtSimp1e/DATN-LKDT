using shop.Domain.Entities;
using shop.Infrastructure.Intercepter;
using shop.Infrastructure.Model;
using shop.Infrastructure.Model.Common.Pagination;
using shop.Infrastructure.Repositories.Contact;
using shop.Infrastructure.Repositories.Rank;
using shop.Infrastructure.Repositories.VirtualItem;
using shop.Infrastructure.Utilities;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace shop.Infrastructure.Business.Contact
{
    public class ContactBusiness : IContactBusiness
    {
        private readonly IContactRepository _contactRepository;
        public ContactBusiness(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<ContactEntity> DeleteAsync(Guid Id)
        {
            var res = await DeleteAsync(new List<Guid> { Id });
            return res.FirstOrDefault();
        }

        public async Task<List<ContactEntity>> DeleteAsync(List<Guid> ids)
        {
            var res = await _contactRepository.DeleteAsync(ids);
            return res;
        }

        public async Task<ContactEntity> FindAsync(Guid Id)
        {
            var res = await _contactRepository.FindAsync(Id);
            return res;
        }

        public async Task<ContactEntity> PatchAsync(ContactEntity contactEntity)
        {
            throw new NotImplementedException();
        }

        public async Task<ContactEntity> SaveAsync(ContactEntity contactEntity)
        {
            var res = await SaveAsync(new List<ContactEntity> { contactEntity });
            return res.FirstOrDefault();
        }

        public async Task<List<ContactEntity>> SaveAsync(List<ContactEntity> contactEntity)
        {            
            var result = await _contactRepository.SaveAsync(contactEntity);
            return result;
        }
    }
}
