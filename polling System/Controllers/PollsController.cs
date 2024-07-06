using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using polling_System.Areas.Identity.Data;
using polling_System.Models;
using Polling_System.Models;
using System.Linq;

namespace polling_System.Controllers
{
    //[Authorize]
    [Authorize("Admin")]
    public class PollsController : Controller
    {
        private readonly polling_SystemContext _context;

        public PollsController(polling_SystemContext context)
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


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Poll poll)
        {
            _context.Polls.Add(poll);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));


        }

        public IActionResult Details(int id)
        {
            var poll = _context.Polls
                .Include(p => p.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefault(p => p.Id == id);

            if (poll == null)
            {
                return NotFound();
            }

            return View(poll);
        }




    }

}

