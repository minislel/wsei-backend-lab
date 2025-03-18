namespace WebAPI.Dto
{
    public class QuizDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public List<QuizItem> Items { get; set; }

        public static QuizDto of(Quiz quiz)
        {
            return new QuizDto
            {
                Id = quiz.Id,
                Question = quiz.Title,
                Items = quiz.Items
            };
        }

    }
}
