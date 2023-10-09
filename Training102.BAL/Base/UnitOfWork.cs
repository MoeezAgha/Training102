using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training102.BAL.Base.Repository;
using Training102.BAL.Interfaces;
using Training102.DAL;

namespace Training102.BAL.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UserRepository UserRepository { get; set; }

        public TrainingRepository TrainingRepository { get; set; }

        public QuizRepository QuizRepository { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            UserRepository = new UserRepository(_context);
            TrainingRepository = new TrainingRepository(_context);
            QuizRepository = new QuizRepository(_context);

        }




        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }


    //public interface ICustomRepository<TEntity> where TEntity : class
    //{
    //    Task<IEnumerable<TEntity>> GetEntitiesWithCustomLogicAsync();
    //    // Add other custom methods as needed
    //}
    //public class CustomRepository<TEntity> : ICustomRepository<TEntity> where TEntity : class
    //{
    //    private readonly DbSet<TEntity> _entities;

    //    public CustomRepository(DbContext context)
    //    {
    //        _entities = context.Set<TEntity>();
    //    }

    //    public async Task<IEnumerable<TEntity>> GetEntitiesWithCustomLogicAsync()
    //    {
    //        // Implement your custom data access logic here
    //        // For example, filter or transform the data in a specific way
    //        return await _entities.Where(/* Your custom criteria */).ToListAsync();
    //    }

    //    // Implement other custom methods as needed
    //}
}
