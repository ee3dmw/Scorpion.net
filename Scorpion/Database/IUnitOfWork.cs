using System;

namespace Scorpion.Database
{
    public interface IUnitOfWork : IDisposable
    {
        int Save();
    }
}