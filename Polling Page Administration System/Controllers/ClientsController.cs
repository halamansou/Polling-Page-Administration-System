using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Polling_Page_Administration_System.Models;


namespace Polling_Page_Administration_System.Controllers
{
    public class ClientsController : Controller
    {
        private readonly PollsContext _context;

        public ClientsController(PollsContext context)
        {
            _context = context;
        }

        public IActionResult LastPoll()
        {
            var poll = _context.Polls
                .Include(p => p.Questions)
                .ThenInclude(q => q.Answers)
                .OrderByDescending(p => p.Id)
                .FirstOrDefault();

            if (poll == null)
            {
                return NotFound();
            }

            return View(poll);
        }

        [HttpPost]
        public IActionResult SubmitAnswer(int answerId)
        {
            var answer = _context.Answers.Find(answerId);

            if (answer == null)
            {
                return NotFound();
            }

            // Logic to handle answer submission

            return RedirectToAction("ThankYou");
        }

        public IActionResult ThankYou()
        {
            return View();
        }
    }
}
