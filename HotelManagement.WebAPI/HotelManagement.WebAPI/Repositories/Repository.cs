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
            try
            {
                // Atașează entitatea în context și marchează ca Modified
                var entry = _context.Entry(entity);
                if (entry.State == EntityState.Detached)
                {
                    // Detașează orice entitate existentă cu același ID
                    var attachedEntity = _dbSet.Local.FirstOrDefault(e =>
                        _context.Entry(e).Property("Id").CurrentValue.Equals(
                            entry.Property("Id").CurrentValue));

                    if (attachedEntity != null)
                    {
                        _context.Entry(attachedEntity).State = EntityState.Detached;
                    }

                    // Marchează entitatea ca modificată
                    entry.State = EntityState.Modified;
                }
                else
                {
                    // Entitatea este deja atașată, deci marchează ca modificată
                    entry.State = EntityState.Modified;
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare în Update: {ex.Message}");
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                var entity = _dbSet.Find(id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare în Delete: {ex.Message}");
                throw;
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

        // Implementări asincrone
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare în AddAsync: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                // Obține valoarea ID-ului entității
                var idProperty = typeof(T).GetProperty("Id");
                var id = idProperty.GetValue(entity);

                // Verifică dacă există entități atașate cu același ID
                var attachedEntity = _dbSet.Local.FirstOrDefault(e =>
                    idProperty.GetValue(e).Equals(id) && e != entity);

                // Dacă există, detașează-le
                if (attachedEntity != null)
                {
                    _context.Entry(attachedEntity).State = EntityState.Detached;
                }

                // Atașează entitatea nouă și marcheaz-o ca modificată
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare în UpdateAsync: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare în DeleteAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity != null;
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }
    }
}