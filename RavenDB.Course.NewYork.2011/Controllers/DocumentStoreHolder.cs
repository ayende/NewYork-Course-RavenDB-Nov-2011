using System;
using System.Configuration;
using Raven.Client;
using Raven.Client.Document;

namespace RavenDB.Course.NewYork._2011.Controllers
{
	public static class DocumentStoreHolder
	{
		private static IDocumentStore documentStore;

		public static IDocumentStore DocumentStore
		{
			get
			{
				if(documentStore != null)
					return documentStore;

				lock (typeof(DocumentStoreHolder))
				{
					if (documentStore != null)
						return documentStore;

					var customConnection = ConfigurationManager.ConnectionStrings[Environment.MachineName] != null;
					documentStore = new DocumentStore
					{
						ConnectionStringName = customConnection ? Environment.MachineName : "RavenDB"
					};


					documentStore.Initialize();

				}

				return documentStore;
			}
		}
	}
}