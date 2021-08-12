using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
  public interface IRepository<T> where T : class
  {
    Task Insert(T entity);

    Task Update(T entity);

    Task<T> GetById(string id);

    Task<IEnumerable<T>> GetAll();

    Task Delete(string id);
  }
}