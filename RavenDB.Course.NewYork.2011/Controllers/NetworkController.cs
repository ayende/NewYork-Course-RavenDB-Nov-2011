using System;
using System.Linq;
using System.Web.Mvc;
using RavenDB.Course.NewYork._2011.Models;

namespace RavenDB.Course.NewYork._2011.Controllers
{
	public class NetworkController : RavenController
	{
		public ActionResult New(string name)
		{
			Session.Store(new CableNetwork
			{
				Name = name,
				Description = "biased",
				LogoUrl = "http://logo/" + name
			});

			return Content("Created");
		}

		public ActionResult AddProgram(string networkId, string name)
		{
			Session.Store(new Program
			{
				Name = name,
				Rating = ProgramRating.PG,
				Description = "Boring",
				TimeSlot = new TimeSpan(0, 08, 25),
				NetworkId = networkId
			});

			return Content("Created");
		}

		public ActionResult Programs(string networkId)
		{
			var programs = from program in Session.Query<Program>()
			               where program.NetworkId == networkId
			               select program;

			return Json(programs.ToList(), JsonRequestBehavior.AllowGet);
		}
	}
}