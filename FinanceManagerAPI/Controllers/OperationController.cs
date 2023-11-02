using Microsoft.AspNetCore.Mvc;

namespace FinanceManagerAPI.Controllers
{
    public class OperationController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
