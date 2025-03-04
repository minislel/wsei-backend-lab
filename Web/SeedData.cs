using ApplicationCore.Interfaces.Repository;
using BackendLab01;
using System.Drawing;

namespace Infrastructure.Memory;
public static class SeedData
{
    public static void Seed(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            var quizRepo = provider.GetService<IGenericRepository<Quiz, int>>();
            var quizItemRepo = provider.GetService<IGenericRepository<QuizItem, int>>();
			var quizItemUserAnswerRepo = provider.GetService<IGenericRepository<QuizItemUserAnswer, string>>();
            var AdminService = provider.GetService<IQuizAdminService>();
            var listQuiz1 = new List<QuizItem>()
            {
                new QuizItem(0, "Pierwsza litera alfabetu?", new List<string>() { "B", "C", "D" }, "A"),
                new QuizItem(0, "Druga litera alfabetu?", new List<string>() { "A", "C", "E" }, "B"),
                new QuizItem(0, "Trzecia litera alfabetu?", new List<string>() { "A", "B", "F" }, "C"),
                new QuizItem(0, "Czwarta litera alfabetu?", new List<string>() { "A", "B", "F" }, "D"),
                new QuizItem(0, "Piąta litera alfabetu?", new List<string>() { "A", "B", "F" }, "E"),
                new QuizItem(0, "Szósta litera alfabetu?", new List<string>() { "A", "B", "F" }, "F")
            };
            var listQuizCyryllic = new List<QuizItem>()
            {
                new QuizItem(0, "Pierwsza litera alfabetu cyrylickiego?", new List<string>() { "Б", "В", "Г" }, "А"),
                new QuizItem(0, "Druga litera alfabetu cyrylickiego?", new List<string>() { "А", "В", "Г" }, "Б"),
                new QuizItem(0, "Trzecia litera alfabetu cyrylickiego?", new List<string>() { "А", "Б", "Г" }, "В"),
                new QuizItem(0, "Czwarta litera alfabetu cyrylickiego?", new List<string>() { "А", "Б", "В" }, "Г"),
                new QuizItem(0, "Piąta litera alfabetu cyrylickiego?", new List<string>() { "А", "Б", "В" }, "Д"),
                new QuizItem(0, "Szósta litera alfabetu cyrylickiego?", new List<string>() { "А", "Б", "В" }, "Е")
            };

            if (quizItemRepo is not null)
            {
                foreach (var item in listQuiz1)
                {
                    quizItemRepo.Add(item);
                }
                foreach (var item in listQuizCyryllic)
                {
                    quizItemRepo.Add(item);
                }
            }
            var quizItems1 = quizItemRepo.FindAll().Where(q => listQuiz1.Select(i => i.Id).Contains(q.Id)).ToList();
            var quizItemsCyryllic = quizItemRepo.FindAll().Where(q => listQuizCyryllic.Select(i => i.Id).Contains(q.Id)).ToList();
            if (quizRepo is not null)
            {
                quizRepo.Add(new Quiz(0, quizItems1, "Alfabet łaciński"));
                quizRepo.Add(new Quiz(0, quizItemsCyryllic, "Alfabet cyrylicki"));
            }

        }
    }
}