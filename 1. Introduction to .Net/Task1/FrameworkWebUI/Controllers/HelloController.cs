using System;
using System.Web.Mvc;
using BLL;

namespace FrameworkWebUI.Controllers
{
    public class HelloController : Controller
    {
		[HttpGet]
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult SayHello(string name)
		{
			string response = HelloService.SayHello(DateTime.Now, name);

			return Content(response);
		}
	}
}