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
	}
}