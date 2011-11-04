using System;

namespace RavenDB.Course.NewYork._2011.Models
{
	public class Program
	{
		public string Id { get; set; }
		public string NetworkId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Popularity { get; set; }
		public TimeSpan TimeSlot { get; set; }
		public ProgramRating Rating { get; set; }
		public Review[] Reviews { get; set; }
		public class Review
		{
			public string Comment { get; set; }
			public string Author { get; set; }
		}
	}
}