using Microsoft.AspNetCore.Mvc;

namespace PeopleManager.Ui.Mvc.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
