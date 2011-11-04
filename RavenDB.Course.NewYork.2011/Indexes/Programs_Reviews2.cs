using System;
using System.Linq;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;
using RavenDB.Course.NewYork._2011.Models;

namespace RavenDB.Course.NewYork._2011.Indexes
{
	public class Programs_Reviews2 : AbstractIndexCreationTask<Program, Programs_Reviews2.Result>
	{
		public class Result
		{
			public Result()
			{
				throw new InvalidOperationException("Can't happen");
			}

			public string Comments { get; set; }
			public string Authors { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
		}

		public Programs_Reviews2()
		{
			Map = programs =>
			      from program in programs
			      select new
			      {
			      	Comments = program.Reviews.Select(x => x.Comment),
			      	Authors = program.Reviews.Select(x => x.Author),
			      	program.Name,
			      	program.Description
			      };

			Index(x=>x.Description, FieldIndexing.Analyzed);
			Index(x=>x.Comments, FieldIndexing.Analyzed);
			Store(x=>x.Name, FieldStorage.Yes);
		}

	}
}