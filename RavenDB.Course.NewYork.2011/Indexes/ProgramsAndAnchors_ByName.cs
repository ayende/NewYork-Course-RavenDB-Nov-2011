using System.Linq;
using Raven.Client.Indexes;
using RavenDB.Course.NewYork._2011.Models;

namespace RavenDB.Course.NewYork._2011.Indexes
{
	public class ProgramsAndAnchors_ByName : AbstractMultiMapIndexCreationTask<ProgramsAndAnchors_ByName.Result>
	{
		public class Result
		{
			public string Name { get; set; }
		}

		public ProgramsAndAnchors_ByName()
		{
			AddMap<Program>(programs => 
				from program in programs 
				select new { program.Name}
			);

			AddMap<Anchor>(anchors =>
				from anchor in anchors
				select new { anchor.Name }
			);
		}
	}
}