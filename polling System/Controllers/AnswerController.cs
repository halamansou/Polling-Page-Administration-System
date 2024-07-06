using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using polling_System.Areas.Identity.Data;
using polling_System.Models;
using Polling_System.Models;
using System.Linq;


namespace polling_System.Controllers
{
    public class AnswerController : Controller
    {
        private readonly polling_SystemContext _context;

        public AnswerController(polling_SystemContext context)
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
