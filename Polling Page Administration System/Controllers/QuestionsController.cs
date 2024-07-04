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

        public IActionResult Create(int pollId)
        {
            var model = new Question { PollId = pollId };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Question question)
        {
            if (question.Text !=null)
            {
                _context.Questions.Add(question);
                _context.SaveChanges();
                return RedirectToAction("Details", "Polls", new { id = question.PollId });
            }

            return View(question);
        }





    }
}
