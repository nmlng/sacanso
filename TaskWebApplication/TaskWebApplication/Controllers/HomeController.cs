using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskWebApplication.Models
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			string serverPath = Server.MapPath("~/Content/Files");
			DirectoryInfo serverDirectory = new DirectoryInfo(serverPath);
			FileInfo[] files = serverDirectory.GetFiles();
			ViewBag.files = files;
			return View();
		}

		[HttpPost]
		public ActionResult Index(HttpPostedFileBase file)
		{
			if (file != null && file.ContentLength > 0)
				try
				{
					string path = Path.Combine(Server.MapPath("~/Content/Files"),
																		 Path.GetFileName(file.FileName));
					file.SaveAs(path);
					ViewBag.Message = "File uploaded successfully";
				}
				catch (Exception ex)
				{
					ViewBag.Message = "ERROR:" + ex.Message.ToString();
				}
			else
			{
				ViewBag.Message = "You have not specified a file.";
				return View();
			}
			return RedirectToAction("Index");
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}