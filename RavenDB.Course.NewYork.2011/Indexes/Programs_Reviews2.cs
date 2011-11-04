using System;
using System.Linq;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;
using RavenDB.Course.NewYork._2011.Models;

namespace RavenDB.Course.NewYork._2011.Indexes
{

	public class ProgramsReviews: AbstractIndexCreationTask<Program, ProgramsReviews.Result>
	{
		public class Result
		{
			public string Id { get; set; }
			public string Comment { get; set; }
			public string Author { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
		}

		public ProgramsReviews()
		{
			Map = programs =>
			      from program in programs
			      from review in program.Reviews
			      select new
			      {
			      	program.Name,
			      	program.Description,
			      	review.Author,
			      	review.Comment,
					program.Id
			      };

			Index(x=>x.Comment, FieldIndexing.Analyzed);
			Store(x=>x.Comment, FieldStorage.Yes);

			Index(x=>x.Description, FieldIndexing.Analyzed);
			Store(x=>x.Description, FieldStorage.Yes);

			Store(x=>x.Author, FieldStorage.Yes);
			Store(x=>x.Name, FieldStorage.Yes);
		}
	}
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