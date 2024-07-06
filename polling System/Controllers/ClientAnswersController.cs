using Microsoft.AspNetCore.Mvc;

namespace polling_System.Controllers
{

    //Controller to display  Client's answer  
    public class ClientAnswersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
