using MongoDB.Bson;
using System.Collections.Generic;
namespace BookStorage.Interface.Models
{
	public class Book
	{
		public ObjectId id { get; set; }

		public string Name { get; set; }

		public string Author { get; set; }

		public int Count { get; set; }

		public IEnumerable<string> Genres { get; set; }

		public int Year { get; set; }
	}
}
