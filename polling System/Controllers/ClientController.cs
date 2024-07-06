using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using polling_System.Areas.Identity.Data;
using Polling_System.Models;
using System.Security.Claims;

namespace polling_System.Controllers
{
    [Authorize("User")]
    public class ClientController : Controller
    {
        private readonly polling_SystemContext _context;

        public ClientController(polling_SystemContext context)
        {
            _context = context;
        }

        // Action to view the last poll
        public async Task<IActionResult> LastPoll()
        {
            var poll = await _context.Polls
                .Include(p => p.Questions)
                .ThenInclude(q => q.Answers)
                .OrderByDescending(p => p.Id)
                .FirstOrDefaultAsync();

            return View(poll);
        }

        // Action to submit answers
        [HttpPost]
        public async Task<IActionResult> SubmitAnswers(int pollId, Dictionary<int, int> selectedAnswers)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            foreach (var questionId in selectedAnswers.Keys)
            {
                var answerId = selectedAnswers[questionId];
                var response = new ClientAnswer
                {
                    AnswerId = answerId,
                    UserId = userId,
                    AnsweredAt = DateTime.Now
                };
                _context.ClientAnswers.Add(response);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("ThankYou");
        }

        public IActionResult ThankYou()
        {
            return View();
        }



		public async Task<IActionResult> UserResponses()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var userResponses = await _context.ClientAnswers
				.Include(ca => ca.Answer)
				.ThenInclude(a => a.Question)
				.Where(ca => ca.UserId == userId)
				.ToListAsync();

			return View(userResponses);
		}

	}
}
