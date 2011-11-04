using System.Web.Mvc;

namespace RavenDB.Course.NewYork._2011.Controllers
{
	public class HomeController : RavenController
	{
		 public ActionResult Index()
		 {
		 	return Content("Fly, Raven, Fly!");
		 }
	}
}