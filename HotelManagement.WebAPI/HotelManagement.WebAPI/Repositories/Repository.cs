// Repositories/Repository.cs
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HotelManagement.WebAPI.Data;
using HotelManagement.WebAPI.Repositories;

namespace HotelManagement.WebAPI.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly HotelDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(HotelDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // Implementări sincrone
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

        public bool Exists(int id)
        {
            var entity = _dbSet.Find(id);
            return entity != null;
        }

        public int Count()
        {
            return _dbSet.Count();
        }

        // Implementări asincrone utilizând Task.Run
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.Run(() => _dbSet.ToList());
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.Run(() => _dbSet.Where(predicate).ToList());
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await Task.Run(() => _dbSet.Find(id));
        }

        public async Task<T> AddAsync(T entity)
        {
            return await Task.Run(() => {
                _dbSet.Add(entity);
                _context.SaveChanges();
                return entity;
            });
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.Run(() => {
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            });
        }

        public async Task DeleteAsync(int id)
        {
            await Task.Run(() => {
                var entity = _dbSet.Find(id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    _context.SaveChanges();
                }
            });
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await Task.Run(() => {
                var entity = _dbSet.Find(id);
                return entity != null;
            });
        }

        public async Task<int> CountAsync()
        {
            return await Task.Run(() => _dbSet.Count());
        }
    }
}