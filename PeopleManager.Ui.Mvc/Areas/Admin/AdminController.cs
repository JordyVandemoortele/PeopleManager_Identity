using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PeopleManager.Ui.Mvc.Areas.Admin
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class AdminController: Controller
    {
    }
}
