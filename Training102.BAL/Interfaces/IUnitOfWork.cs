using Training102.BAL.Base.Repository;

namespace Training102.BAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        public UserRepository UserRepository { get;  }
        Task<int> CommitAsync();
    }


}


