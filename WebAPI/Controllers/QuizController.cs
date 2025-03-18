using BackendLab01;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [Route("api/v1/quizzes")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizUserService _service;
        public QuizController(IQuizUserService userService)
        {
            _service = userService;
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<QuizDto> FindById(int id)
        {
            var quiz = _service.FindQuizById(id);
            if (quiz == null)
            {
                return NotFound();
            }
            return Ok(quiz);
        }
        [HttpGet]
        public IEnumerable<QuizDto> FindAll()
        {
            return _service.GetAllQuizzes().Select(q => QuizDto.of(q));
        }
        [HttpPost]
        [Route("{quizId}/items/{itemId}")]
        public void SaveAnswer([FromBody] QuizItemAnswerDto dto, int quizId, int quizItemId)
        {
            _service.SaveUserAnswerForQuiz(quizId, dto.UserId, quizItemId, dto.Answer);
        }
        [HttpGet]
        [Route("{quizId}/results/{userId}")]
        public ActionResult<QuizResultDto> GetResults(int quizId, int userId)
        {
            var quiz = _service.FindQuizById(quizId);
            if (quiz == null)
            {
                return NotFound();
            }
            var correctAnswers = _service.CountCorrectAnswersForQuizFilledByUser(quizId, userId);
            var totalAnswers = quiz.Items.Count;
            return new QuizResultDto
            {
                CorrectAnswers = correctAnswers,
                TotalAnswers = totalAnswers
            };
        }
    }
}
