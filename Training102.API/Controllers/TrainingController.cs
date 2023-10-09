//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Training102.DAL; // Import your data access context
//using Training102.BAL.Services.Contract; // Import your services and view models
//using Training102.BAL.Interfaces;
//using Training102.BAL.Base;
//using Training102.SharedModel;

//namespace Training102.API.Controllers
//{
//    [Route("api/trainings")]
//    [ApiController]
//    public class TrainingController : ControllerBase
//    {
//        private readonly IUnitOfWork _unitOfWork;

//        public TrainingController(IUnitOfWork trainingService)
//        {
//            _unitOfWork = trainingService;
//        }

//        // GET: api/trainings
//        [HttpGet]
//        public async Task<IActionResult> GetTrainings()
//        {
//            var trainings = await _unitOfWork.TrainingRepository.GetAllAsync();
//            return Ok(trainings);
//        }

//        // GET: api/trainings/{id}
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetTraining(int id)
//        {
//            var training = await _unitOfWork.TrainingRepository.GetByIdAsync(id);

//            if (training == null)
//            {
//                return NotFound();
//            }

//            return Ok(training);
//        }

//        // POST: api/trainings
//        [HttpPost]
//        public async Task<IActionResult> CreateTraining([FromBody] TrainingViewModel trainingViewModel)
//        {
//            if (trainingViewModel == null)
//            {
//                return BadRequest("Invalid training data.");
//            }

//            var result = await _unitOfWork.TrainingRepository.AddAsync(trainingViewModel);
//            await _unitOfWork.CommitAsync();

//            if (result.IsSuccessful)
//            {
//                return CreatedAtAction("GetTraining", new { id = result.TrainingId }, result);
//            }
//            else
//            {
//                return BadRequest(result);
//            }
//        }

//        // PUT: api/trainings/{id}
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateTraining(int id, [FromBody] TrainingViewModel trainingViewModel)
//        {
//            if (trainingViewModel == null || id != trainingViewModel.TrainingId)
//            {
//                return BadRequest("Invalid training data.");
//            }

//            var result = await _trainingService.UpdateTrainingAsync(id, trainingViewModel);

//            if (result.IsSuccessful)
//            {
//                return Ok(result);
//            }
//            else
//            {
//                return BadRequest(result);
//            }
//        }

//        // DELETE: api/trainings/{id}
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteTraining(int id)
//        {
//            var result = await _trainingService.DeleteTrainingAsync(id);

//            if (result.IsSuccessful)
//            {
//                return NoContent();
//            }
//            else
//            {
//                return BadRequest(result);
//            }
//        }
//    }
//}
