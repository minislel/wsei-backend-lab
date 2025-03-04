using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace BackendLab01.Pages
{
    
    public class QuizModel : PageModel
    {
        private readonly IQuizUserService _userService;

        private readonly ILogger _logger;
        public QuizModel(IQuizUserService userService, ILogger<QuizModel> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [BindProperty]
        public string Question { get; set; }
        [BindProperty]
        public List<string> Answers { get; set; }
        
        [BindProperty]
        public String UserAnswer { get; set; }
        
        [BindProperty]
        public int QuizId { get; set; }
        
        [BindProperty]
        public int ItemId { get; set; }
        [BindProperty]

        public int? nextQuizItemId { get; set; }    
        public IActionResult OnGet(int quizId, int itemId)
        {
            QuizId = quizId;
            ItemId = itemId;
            
            var quiz = _userService.FindQuizById(quizId);
			nextQuizItemId = itemId + 1 <= quiz.Items.Count ? itemId + 1 : null;
			var quizItem = quiz?.Items.Find(i => i.Id == itemId);
            Question = quizItem?.Question;
            Answers = new List<string>();
            if (quizItem is not null)
            {
                Answers.AddRange(quizItem?.IncorrectAnswers);
                Answers.Add(quizItem?.CorrectAnswer);
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            _userService.SaveUserAnswerForQuiz(QuizId, 1, ItemId, UserAnswer);
            if (nextQuizItemId == null)
            { 
                return RedirectToPage("Summary", new { quizId = QuizId, userId = 1 });
            }
            return RedirectToPage("Item", new {quizId = QuizId, itemId = ItemId + 1});
        }
    }
}
