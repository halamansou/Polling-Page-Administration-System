using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Polling_Page_Administration_System.Models;
using System.Linq;

namespace Polling_Page_Administration_System.Controllers
{
    public class AnswerController : Controller
    {
        private readonly PollsContext _context;

        public AnswerController(PollsContext context)
        {
            _context = context;
        }

        public IActionResult Create(int questionId)
        {
            var model = new Answer { QuestionId = questionId };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Answer answer)
        {
            if (answer.Text != null)
            {
                _context.Answers.Add(answer);
                _context.SaveChanges();

                var question = _context.Questions
                                       .Include(q => q.Poll)
                                       .FirstOrDefault(q => q.Id == answer.QuestionId);

                if (question != null)
                {
                    return RedirectToAction("Details", "Polls", new { id = question.PollId });
                }
            }

            ViewBag.QuestionId = answer.QuestionId;
            return View(answer);
        }
    }
}
