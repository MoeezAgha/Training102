using Training102.BAL.Interfaces;
using Training102.DAL;

namespace Training102.BAL.Base.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
    public class TrainingRepository : BaseRepository<Training>, ITrainingRepository 
    { 
        public TrainingRepository(ApplicationDbContext context) : base(context)
        { 
        }
    }


    public class QuizRepository : BaseRepository<Quiz>, IQuizRepository
    {
        public QuizRepository(ApplicationDbContext context) : base(context)
        {
        }
    }


    internal interface IQuizRepository : IBaseRepository<Quiz>
    {
    }




    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }


    internal interface IQuestionRepository : IBaseRepository<Question>
    {
    }


    public class AnswerRepository : BaseRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(ApplicationDbContext context) : base(context)
        {
        }
    }


    internal interface IAnswerRepository : IBaseRepository<Answer>
    {
    }



    public class TrainingAssignToUserRepository : BaseRepository<TrainingAssignToUser>, ITrainingAssignToUserRepository
    {
        public TrainingAssignToUserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }


    internal interface ITrainingAssignToUserRepository : IBaseRepository<TrainingAssignToUser>
    {
    }

    public class QuizCompletionRepository : BaseRepository<QuizCompletion>, IQuizCompletionRepository
    {
        public QuizCompletionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }


    internal interface IQuizCompletionRepository : IBaseRepository<QuizCompletion>
    {
    }
}

