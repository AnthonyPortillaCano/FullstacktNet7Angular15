using CleanArchitecture.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infraestructure.Data
{
    public class RepositoryEF<T> :IRepositoryEF<T> where T : class
    {
        protected readonly DbContext _context;
        public RepositoryEF(DbContext context)
        {
            _context = context;
        }
        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public void Update(T entity)
        {
            _context.Update(entity);
        }
        public async Task<int> Insert(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
           // var entityFinal = entity
            var IdProperty = entity.GetType().GetProperty("Id").GetValue(entity, null);
            return (int)IdProperty;
        }
        public async Task Delete(Expression<Func<T,bool>> condicion)
        {
            await _context.Set<T>().Where(condicion).ExecuteDeleteAsync();
        }
        public async Task<T> Obtener<T>(Expression<Func<T, bool>> condicion) where T: class
        {
            return await _context.Set<T>().FirstOrDefaultAsync(condicion);
        }
        public async Task<List<T>> ObtenerList<T>(Expression<Func<T, bool>> condicion) where T : class
        {
            return await _context.Set<T>().Where(condicion).ToListAsync();
        }
        public void UpdateFieldsSave(T entity,params Expression<Func<T, object>>[] includeProperties)
        {
            var dbEntry = _context.Entry(entity);
            foreach(var includeProperty in includeProperties)
            {
                dbEntry.Property(includeProperty).IsModified= true;
            }
        }
          public void DeleteList(List<T> entity)
        {
            _context.RemoveRange(entity);
        }
    }
}
