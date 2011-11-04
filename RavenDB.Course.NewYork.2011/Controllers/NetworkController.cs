using System;
using System.Linq;
using System.Web.Mvc;
using Raven.Client.Linq;
using RavenDB.Course.NewYork._2011.Indexes;
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

		public ActionResult Search2(string text)
		{
			var results = Session.Query<ProgramsReviews.Result, ProgramsReviews>()
				.Customize(x=>x.Include<ProgramsReviews.Result>(y=>y.Id))
				.Search(x => x.Comment, text)
				.Where(x=>x.Comment == text)
				.AsProjection<ProgramsReviews.Result>()
				.ToList();

			return Json(new
			{
				Comment = results[0],
				Program = Session.Load<Program>(results[0].Id)
			}, JsonRequestBehavior.AllowGet);
		
		}

		public ActionResult Search(string text)
		{
			var results = Session.Query<Programs_Reviews2.Result, Programs_Reviews2>()
				.Search(x=>x.Comments, text)
				.As<Program>()
				.ToList();

			return Json(results, JsonRequestBehavior.AllowGet);
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
						   orderby program.Popularity
			               select program;

			return Json(programs.ToList(), JsonRequestBehavior.AllowGet);
		}
	}
}