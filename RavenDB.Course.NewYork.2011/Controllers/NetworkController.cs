using System;
using System.Linq;
using System.Web.Mvc;
using Raven.Abstractions.Data;
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


		public ActionResult Set()
		{
			var databaseCommands = Session.Advanced.DatabaseCommands;
			
			databaseCommands.UpdateByIndex("Temp/Programs/ByNetworkId",
				new IndexQuery
				{
					Query = "NetworkId:[[NULL_VALUE]]"
				}, new PatchRequest[]
				{
					new PatchRequest
					{
						Name = "NetworkId",
						Type = PatchCommandType.Set,
						Value = "cablenetworks/1"
					}, 
				});

			return Content("?");
		}
		public ActionResult Populate(string name, int hour)
		{
			Session.Store(new Program
			{
				Name = name,
				TimeSlot = new TimeSpan(0,hour,30)
			});
			return Content("OK");
		}

		public ActionResult Search3(string text)
		{
			var results = Session
				.Query<ProgramsAndAnchors_ByName.Result, ProgramsAndAnchors_ByName>()
				.Where(x=>x.Name == text)
				.As<object>()
				.ToList();

			return Json(results, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Search4()
		{
			var programs = Session.Advanced.LuceneQuery<Program, Programs_ByRatingAndLocation>()
				.WithinRadiusOf(3, 40.7635581, -73.9735082)
				.WhereEquals("Rating", "PG")
				.ToList();

			return Json(programs,JsonRequestBehavior.AllowGet);
		}

		public ActionResult AddAnchor(string name)
		{
			Session.Store(new Anchor
			{
				Name = name,
			});
			return Content("Hired");
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