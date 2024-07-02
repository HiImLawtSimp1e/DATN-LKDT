using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepositories
{
    public interface IBlogRepository
    {
        Task<BlogEntity> Add(BlogEntity obj);
        Task<BlogEntity> Update(BlogEntity obj);
        Task<List<BlogEntity>> GetAll();
        Task Delete(Guid id);

        Task<BlogEntity> GetById(Guid id);
    }
}
