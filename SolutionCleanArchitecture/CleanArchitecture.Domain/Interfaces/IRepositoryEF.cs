using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces
{
    public interface IRepositoryEF<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        void Update(T entity);
        Task<int> Insert(T entity);
        Task Delete(Expression<Func<T, bool>> condicion);
        Task<T> Obtener<T>(Expression<Func<T, bool>> condicion) where T : class;

        Task<List<T>> ObtenerList<T>(Expression<Func<T, bool>> condicion) where T : class;

        void UpdateFieldsSave(T entity, params Expression<Func<T, object>>[] includeProperties);

        void DeleteList(List<T> entity);
    }
}
