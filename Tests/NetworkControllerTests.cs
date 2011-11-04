using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client.Embedded;
using RavenDB.Course.NewYork._2011.Controllers;
using RavenDB.Course.NewYork._2011.Models;
using Xunit;

namespace Tests
{
	public class NetworkControllerTests
	{
		[Fact]
		public void WillSaveNewNetwork()
		{
			using(var store = new EmbeddableDocumentStore{RunInMemory=true}.Initialize())
			{
				var controller = new NetworkController {Session = store.OpenSession()};

				controller.New("cnn");

				controller.Session.SaveChanges();


				using(var session = store.OpenSession())
				{
					var network = session.Load<CableNetwork>("cablenetworks/1");
					Assert.Equal("cnn", network.Name);
				}
			}
		}
	}
}
