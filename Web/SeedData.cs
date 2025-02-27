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
            quizItemRepo.Add(new QuizItem(0, "Pierwsza litera alfabetu?", new List<string>() { "B", "C", "D" }, "A"));
            quizItemRepo.Add(new QuizItem(0, "Druga litera alfabetu?", new List<string>() { "A", "C", "E" }, "B"));
			quizItemRepo.Add(new QuizItem(0, "Trzecia litera alfabetu?", new List<string>() { "A", "B", "F" }, "C"));
			quizItemRepo.Add(new QuizItem(0, "Czwarta litera alfabetu?", new List<string>() { "A", "B", "F" }, "D"));
			quizItemRepo.Add(new QuizItem(0, "Piąta litera alfabetu?", new List<string>() { "A", "B", "F" }, "E"));
			quizItemRepo.Add(new QuizItem(0, "Szósta litera alfabetu?", new List<string>() { "A", "B", "F" }, "F"));



			AdminService.AddQuiz("tesotwy 1", AdminService.FindAllQuizItems());
		}
    }
}