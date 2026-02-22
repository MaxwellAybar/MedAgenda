using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAgenda.Domain.Repository
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}
