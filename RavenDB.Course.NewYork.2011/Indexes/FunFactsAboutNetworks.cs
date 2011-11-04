using System.Linq;
using Lucene.Net.Documents;
using Raven.Client.Indexes;
using RavenDB.Course.NewYork._2011.Models;

namespace RavenDB.Course.NewYork._2011.Indexes
{
	public class FunFactsAboutNetworks : AbstractIndexCreationTask<CableNetwork>
	{
		public FunFactsAboutNetworks()
		{
			Map = networks => from network in networks
			                  select new
			                  {
			                  	_ = from fun in network.Fun
			                  	    select new Field(fun.Key, fun.Value, Field.Store.YES, Field.Index.ANALYZED)
			                  };

		}
	}
}