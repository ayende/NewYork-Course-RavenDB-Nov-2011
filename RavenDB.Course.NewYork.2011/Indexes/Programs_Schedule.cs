using System;
using System.Linq;
using Raven.Client.Indexes;
using RavenDB.Course.NewYork._2011.Models;

namespace RavenDB.Course.NewYork._2011.Indexes
{
	public class Programs_Schedule : AbstractIndexCreationTask<Program, Programs_Schedule.Result>
	{
		 public class Result
		 {
			 public object[] Programs { get; set; }
			 public string At { get; set; }

		 }

		public Programs_Schedule( )
		{
			Map = programs =>
			      from program in programs
			      let at = program.TimeSlot.ToString().Split(':')[1]
			      select new
			      {
					  Programs = new object[]
			      	{
			      		new
			      		{
			      			At = program.TimeSlot.ToString(),
			      			program.Name
			      		}
			      	},
			      	At = at,
			      };

			Reduce = results =>
			         from result in results
			         group result by result.At
			         into g
			         select new
			         {
			         	Programs = g.SelectMany(x => x.Programs),
			         	At = g.Key
			         };
		}
	}
}