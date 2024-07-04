using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Polling_Page_Administration_System.Models;
using System.Linq;
using System.Security.Claims;

namespace Polling_Page_Administration_System.Controllers
{
    public class ClientController : Controller
    {
        private readonly PollsContext _context;

        public ClientController(PollsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var polls = _context.Polls
                                .Include(p => p.Questions)
                                .ThenInclude(q => q.Answers)
                                .ToList();
            return View(polls);
        }

        public IActionResult Answer(int questionId)
        {
            var question = _context.Questions
                                   .Include(q => q.Answers)
                                   .FirstOrDefault(q => q.Id == questionId);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        [HttpPost]
        public IActionResult Answer(int questionId, int answerId)
        {
            var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var clientAnswer = new ClientAnswer
            {
                ClientId = int.Parse(clientId),
                AnswerId = answerId,
                AnsweredAt = DateTime.Now
            };

            _context.ClientAnswers.Add(clientAnswer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult MyAnswers()
        {
            var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var answers = _context.ClientAnswers
                                  .Where(ca => ca.ClientId == int.Parse(clientId))
                                  .Include(ca => ca.Answer)
                                  .ThenInclude(a => a.Question)
                                  .ToList();
            return View(answers);
        }
    }
}
