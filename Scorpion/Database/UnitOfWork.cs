using System.Data.Entity;

namespace Scorpion.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            //_context.Dispose(); Do we want to do this? Will lose change tracking for entities
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}