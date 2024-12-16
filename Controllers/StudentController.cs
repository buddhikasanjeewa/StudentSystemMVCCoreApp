using Microsoft.AspNetCore.Mvc;

namespace MyFirstMvcCore.Controllers
{
	public class StudentController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
