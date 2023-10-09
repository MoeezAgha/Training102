using Training102.BAL.Base.Repository;

namespace Training102.BAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        public UserRepository UserRepository { get;  }
        public TrainingRepository TrainingRepository { get; }

        public QuizRepository QuizRepository { get; }

        Task<int> CommitAsync();
    }


}


