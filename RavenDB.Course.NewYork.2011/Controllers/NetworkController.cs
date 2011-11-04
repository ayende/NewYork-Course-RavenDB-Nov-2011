using System.Web.Mvc;
using RavenDB.Course.NewYork._2011.Models;

namespace RavenDB.Course.NewYork._2011.Controllers
{
	public class NetworkController : Controller
	{
		 public ActionResult New(string name)
		 {
		 	using(var session = DocumentStoreHolder.DocumentStore.OpenSession())
		 	{
				session.Store(new CableNetwork
				{
					Name = name,
					Description = "biased",
					LogoUrl = "http://logo/"+name
				});

		 		session.SaveChanges();
		 	}

		 	return Content("Created");
		 }
	}
}