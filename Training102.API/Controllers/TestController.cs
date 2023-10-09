using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Training102.BAL.Base.Repository;
using Training102.BAL.Interfaces;
using Training102.DAL;

namespace Training102.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {

        public IUnitOfWork _unitOfWork;
        //public IUserRepository _user;

        public TestController(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
          //  _user = userRepository;
        }
        [HttpPost("index")]
        public async Task<IActionResult>  Index()
        {
       
            var user = _unitOfWork.UserRepository.Get(u => u.Id == 1, ""); // Replace with your filter and includes
            var use2r = _unitOfWork.UserRepository.Get(u => u.Id == 2, "QuizCompletions");
            var sortedUsers = _unitOfWork.UserRepository.GetList(orderBy: q => q.OrderBy(u => u.Id),includeTables: "QuizCompletions");


            

            return Ok(sortedUsers);
        }
    }
}
