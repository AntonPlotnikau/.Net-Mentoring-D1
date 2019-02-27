using System;
using Microsoft.AspNetCore.Mvc;
using BLL;

namespace CoreWebUI.Controllers
{
	public class HelloController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult SayHello(string name)
		{
			string response = HelloService.SayHello(DateTime.Now, name);

			return Content(response);
		}
	}
}