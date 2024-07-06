using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using polling_System.Areas.Identity.Data;
using Polling_System.Models;
using System.Linq;
using System.Threading.Tasks;

namespace polling_System.Controllers
{
    [Authorize("Admin")]
    public class ClientAnswersController : Controller
    {
        private readonly polling_SystemContext _context;

        public ClientAnswersController(polling_SystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userAnswers = await _context.ClientAnswers
                .Include(ca => ca.User) 
                .Include(ca => ca.Answer) 
                .ThenInclude(a => a.Question) 
                .ToListAsync();

            var viewModel = userAnswers.GroupBy(ca => ca.User)
                                       .Select(g => new UserAnswersViewModel
                                       {
                                           UserName = g.Key.UserName,
                                           Answers = g.Select(ca => new AnswerViewModel
                                           {
                                               QuestionText = ca.Answer.Question.Text,
                                               AnswerText = ca.Answer.Text
                                           }).ToList()
                                       }).ToList();

            return View(viewModel);
        }
    }
}
