using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Polling_Page_Administration_System.Models;
using Polling_Page_Administration_System.Models;
using System.Linq;

namespace Polling_Page_Administration_System.Controllers
{
    //[Authorize]
    public class PollsController : Controller
    {
        private readonly PollsContext _context;

        public PollsController(PollsContext context)
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

