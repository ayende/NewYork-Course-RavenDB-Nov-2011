using System.Web.Mvc;
using Raven.Client;

namespace RavenDB.Course.NewYork._2011.Controllers
{
	public abstract class RavenController: Controller
	{
		public new IDocumentSession Session { get; set; }

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			Session = DocumentStoreHolder.DocumentStore.OpenSession();
		}

		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			using(Session)
			{
				if (filterContext.Exception != null)
					return;

				Session.SaveChanges();
			}
		}

	}
}