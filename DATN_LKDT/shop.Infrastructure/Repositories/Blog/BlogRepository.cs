using shop.Domain.Entities;
using shop.Infrastructure.Model;
using shop.Infrastructure.Model.Common.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shop.Infrastructure.Database.Context;
using shop.Infrastructure.Repositories.Blog;

namespace AppData.Repositories
{
    public class BlogRepository: IBlogRepository
    {
        public readonly AppDbContext _dbContext;
        public BlogRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BlogEntity> Add(BlogEntity obj)
        {
            await _dbContext.Blogs.AddAsync(obj);
            await _dbContext.SaveChangesAsync();
            return obj;
        }

        public async Task Delete(Guid id)
        {
            var a = await _dbContext.Blogs.FindAsync(id);
            _dbContext.Blogs.Remove(a);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<BlogEntity>> GetAll()
        {
            var lst = new List<BlogEntity>();
            try
            {
                lst = await _dbContext.Blogs.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return lst;
        }

        public async Task<BlogEntity> GetById(Guid id)
        {
            var blogs = new BlogEntity();
            try
            {
                blogs = await _dbContext.Blogs.FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return blogs;
        }

        public async Task<BlogEntity> Update(BlogEntity obj)
        {
            var a = await _dbContext.Blogs.FindAsync(obj.Id);
            a.Title = obj.Title;
            a.Content = obj.Content;
            a.CreatedDate = obj.CreatedDate;
            a.Images = obj.Images;
            a.Status = obj.Status;
            _dbContext.Blogs.Update(a);
            await _dbContext.SaveChangesAsync();
            return obj;
        }
    }
}
