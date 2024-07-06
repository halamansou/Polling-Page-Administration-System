using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using polling_System.Areas.Identity.Data;


namespace polling_System.Controllers
{
    //[Authorize]
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
        //[HttpPost]
        //public async Task<IActionResult> SubmitAnswers(int pollId, List<int> selectedAnswers)
        //{
        //    var userId = User.Identity.GetHashCode(); 
        //    foreach (var answerId in selectedAnswers)
        //    {
        //        var response = new Response
        //        {
        //            AnswerId = answerId,
        //            UserId = userId, 
        //            SubmittedAt = DateTime.Now
        //        };
        //        _context.Responses.Add(response);
        //    }
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("ThankYou");
        //}

        public IActionResult ThankYou()
        {
            return View();
        }
    }

}