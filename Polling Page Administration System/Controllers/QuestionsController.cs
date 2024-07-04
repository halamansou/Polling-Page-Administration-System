using Microsoft.AspNetCore.Mvc;
using Polling_Page_Administration_System.Models;
using System.Linq;

namespace Polling_Page_Administration_System.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly PollsContext _context;

        public QuestionsController(PollsContext context)
        {
            _context = context;
        }

        public IActionResult AddAnswer(int questionId)
        {
            ViewBag.QuestionId = questionId;
            return View();
        }

        [HttpPost]
        public IActionResult AddAnswer(Answer answer)
        {
            if (ModelState.IsValid)
            {
                _context.Answers.Add(answer);
                _context.SaveChanges();
                return RedirectToAction("Details", "Polls", new { id = answer.Question.PollId });
            }
            ViewBag.QuestionId = answer.QuestionId;
            return View(answer);
        }


    }
}
