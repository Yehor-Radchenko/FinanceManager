using Microsoft.AspNetCore.Mvc;

namespace FinanceManagerAPI.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
