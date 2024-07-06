using Microsoft.AspNetCore.Mvc;
using polling_System.Areas.Identity.Data;
using polling_System.Models;
using Polling_System.Models;
using System.Linq;

namespace polling_System.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly polling_SystemContext _context;

        public QuestionsController(polling_SystemContext context)
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
