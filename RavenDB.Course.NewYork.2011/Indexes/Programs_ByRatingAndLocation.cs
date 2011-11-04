using System.Linq;
using Raven.Client.Indexes;
using RavenDB.Course.NewYork._2011.Models;

namespace RavenDB.Course.NewYork._2011.Indexes
{
	public class Programs_ByRatingAndLocation : AbstractIndexCreationTask<Program>
	{
		public Programs_ByRatingAndLocation()
		{
			Map = programs =>
			      from program in programs
			      select new
			      {
					  program.Rating,
					  _ = SpatialIndex.Generate(program.Based.Lat, program.Based.Lng)
			      };
		}
	}
}