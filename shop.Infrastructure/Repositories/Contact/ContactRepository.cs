﻿using LinqKit;
using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using shop.Infrastructure.Model;
using shop.Infrastructure.Model.Common.Pagination;
using shop.Infrastructure.Repositories.VirtualItem;
using shop.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Repositories.Contact
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _dbContext;
        public ContactRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public async Task<ContactEntity> DeleteAsync(Guid Id)
        {
            var res = await DeleteAsync(new List<Guid> { Id });
            return res.FirstOrDefault();
        }

        public async Task<List<ContactEntity>> DeleteAsync(List<Guid> deleteIds)
        {
            var lstExist = new List<ContactEntity>();
            foreach (var id in deleteIds)
            {
                var exist = await FindAsync(id);
                if (exist == null)
                    continue;
                _dbContext.Contact.Update(exist);
                lstExist.Add(exist);
            }

            await _dbContext.SaveChangesAsync();
            return lstExist;
        }

        public async Task<ContactEntity> FindAsync(Guid Id)
        {
            var result = await _dbContext.Contact.AsNoTracking().FirstOrDefaultAsync(x => x.ID == Id );
            if (result == null)
                throw new ArgumentException(IVirtualItemRepository.Message_VirtualItemNotFound);
            return result;
        }

        public async Task<ContactEntity> SaveAsync(ContactEntity contactEntity)
        {
            var res = await SaveAsync(new List<ContactEntity> { contactEntity });
            return res.FirstOrDefault();
        }

        public async Task<List<ContactEntity>> SaveAsync(List<ContactEntity> contactEntity)
        {
            var updated = new List<ContactEntity>();
            foreach (var e in contactEntity)
            {
                ContactEntity exist = null;
                if (e.ID == Guid.Empty)
                {
                    e.ID = Guid.NewGuid();
                }
                else
                {
                    exist = await FindAsync(e.ID);
                }

                if (exist == null)
                {
                    _dbContext.Contact.Add(e);
                    updated.Add(e);
                }
                else
                {
                    exist.CODE = e.CODE;
                    exist.FullName = e.FullName;
                    exist.Email = e.Email;
                    exist.PhoneNumber = e.PhoneNumber;
                    exist.Content = e.Content;
                    exist.Status = e.Status;
                    exist.Type = e.Type;
                    exist.Address = e.Address;
                    exist.Topic = e.Topic;
                    exist.CreateDate = e.CreateDate;
                    exist.ModifyDate = e.ModifyDate;
                    _dbContext.Contact.Update(exist);
                    updated.Add(exist);
                }

                await _dbContext.SaveChangesAsync();

            }
            return updated;
        }

        async Task<Pagination<ContactEntity>> IContactRepository.GetAllAsync(ContactQueryModel contactQueryModel)
        {
            IQueryable<ContactEntity> query = BuildQuery(contactQueryModel);

            var sortExpression = string.Empty;
            if (string.IsNullOrWhiteSpace(contactQueryModel.Sort) || contactQueryModel.Sort.Equals("-LastModifiedOnDate"))
            {
                query = query.OrderByDescending(x => x.LastModifiedOnDate);
            }
            else
            {
                sortExpression = contactQueryModel.Sort;
            }

            var result = await query.GetPagedAsync(contactQueryModel.CurrentPage.Value, contactQueryModel.PageSize.Value, sortExpression);
            return result;
        }

        async Task<List<ContactEntity>> IContactRepository.ListAllAsync(ContactQueryModel contactQueryModel)
        {
            IQueryable<ContactEntity> query = BuildQuery(contactQueryModel);

            var sortExpression = string.Empty;
            if (string.IsNullOrWhiteSpace(contactQueryModel.Sort) || contactQueryModel.Sort.Equals("-LastModifiedOnDate"))
            {
                query = query.OrderByDescending(x => x.LastModifiedOnDate);
            }
            else
            {
                query = query.ApplySorting(contactQueryModel.Sort);
            }
            return await query.AsNoTracking().ToListAsync();
        }
        protected virtual IQueryable<ContactEntity> BuildQuery(ContactQueryModel queryModel)
        {
            IQueryable<ContactEntity> query;
            query = _dbContext.Contact.AsNoTracking().Where(x => x. == false);
            return query;
        }
    }
}
