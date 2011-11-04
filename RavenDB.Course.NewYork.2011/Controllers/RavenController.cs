using System.Web.Mvc;
using Raven.Client;

namespace RavenDB.Course.NewYork._2011.Controllers
{
	public abstract class RavenController: Controller
	{
		protected IDocumentSession session;

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			session = DocumentStoreHolder.DocumentStore.OpenSession();
		}

		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			using(session)
			{
				if (filterContext.Exception != null)
					return;

				session.SaveChanges();
			}
		}

	}
}