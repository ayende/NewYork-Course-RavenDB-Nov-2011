using System.Linq;
using Raven.Client.Indexes;
using RavenDB.Course.NewYork._2011.Models;

namespace RavenDB.Course.NewYork._2011.Indexes
{
	public class NetworkAndProgramNames
		: AbstractMultiMapIndexCreationTask<NetworkAndProgramNames.Result>
	{
		public class Result
		{
			public string NetworkId { get; set; }
			public string NetworkName { get; set; }
			public string ProgramName { get; set; }
		}

		public NetworkAndProgramNames()
		{
			AddMap<CableNetwork>(
				networks => from network in networks 
							select new
							{
								NetworkId = network.Id, 
								NetworkName = network.Name,
								ProgramName = (string)null
							}
				);

			AddMap<Program>(programs =>
			                from program in programs 
							select new
							{
								program.NetworkId,
								NetworkName = (string)null,
								ProgramName = program.Name
							}
			);

			Reduce = results =>
			         from result in results
			         group result by result.NetworkId
			         into g
			         let networkName = g.Select(x => x.NetworkName).FirstOrDefault(x => x != null)
			         from gr in g
			         select new
			         {
			         	gr.NetworkId,
			         	NetworkName = networkName,
			         	gr.ProgramName
			         };

		}
	}
}