using BookStorage.Interface.Interfaces;
using BookStorage.Interface.Models;
using BookStorage.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			IBooksRepository repository = new BooksRepository("mongodb://localhost", "BookDb", "Books");

			#region Task1
			//1.Добавьте следующие книги(название, автор, количество экземпляров, жанр, год издания):
			//	· Hobbit, Tolkien, 5, fantasy, 2014
			//	· Lord of the rings, Tolkien, 3, fantasy, 2015
			//	· Kolobok, 10, kids, 2000
			//	· Repka, 11, kids, 2000
			//	· Dyadya Stiopa, Mihalkov, 1, kids, 2001
			Console.WriteLine("Task 1");
			repository.AddBooks(
				new List<Book>
				{
					{
						new Book
						{
							Name = "Hobbit",
							Author = "Tolkien",
							Count = 5,
							Genres = new List<string> { "fantasy" },
							Year = 2014
						}
					},
					{
						new Book
						{
							Name = "Lord of the rings",
							Author = "Tolkien",
							Count = 3,
							Genres = new List<string> { "fantasy" },
							Year = 2015
						}
					},
					{
						new Book
						{
							Name = "Kolobok",
							Count = 10,
							Genres = new List<string> { "kids" },
							Year = 2000
						}
					},
					{
						new Book
						{
							Name = "Repka",
							Count = 11,
							Genres = new List<string> { "kids" },
							Year = 2000
						}
					},
					{
						new Book
						{
							Name = "Dyadya Stiopa",
							Author = "Mihalkov",
							Count = 1,
							Genres = new List<string> { "kids" },
							Year = 2001
						}
					}
				});

			DumpBooks(repository.GetBooks());
			#endregion

			#region Task 2
			//2.Найдите книги с количеством экземпляров больше единицы.
			//	a.Покажите в результате только название книги.
			//	b.Отсортируйте книги по названию.
			//	c.Ограничьте количество возвращаемых книг тремя.
			//	d.Подсчитайте количество таких книг.
			Console.WriteLine("Task2");
			var filteredBooksCount = repository.GetBooks(b => b.Count > 1).Count();
			var filteredBooks = repository
										.GetBooks(b => b.Count > 1)
										.Select(b => b.Name)
										.OrderBy(b => b)
										.Take(3);

			Console.WriteLine($"Count: {filteredBooksCount}");
			foreach(var book in filteredBooks)
			{
				Console.WriteLine(book);
			}
			#endregion

			#region Task 3
			//3.Найдите книгу с макимальным / минимальным количеством(count)

			var maxCount = repository.GetBooks().Max(b => b.Count);

			var booksWithMaxCount = repository.GetBooks(b => b.Count == maxCount);

			Console.WriteLine("Task 3");
			DumpBooks(booksWithMaxCount);

			var minCount = repository.GetBooks().Min(b => b.Count);

			var booksWithMinCount = repository.GetBooks(b => b.Count == minCount);
			DumpBooks(booksWithMinCount);

			#endregion

			#region Task4
			var authors = repository.GetBooks(b => !string.IsNullOrWhiteSpace(b.Author)).Select(b => b.Author).Distinct();

			Console.WriteLine("Task 4");
			foreach(var author in authors)
			{
				Console.WriteLine(author);
			}
			#endregion

			#region Task 5
			//5. Выберите книги без авторов.
			Console.WriteLine("Task 5");
			var booksWithoutAuthors = repository.GetBooks(b => string.IsNullOrWhiteSpace(b.Author));

			DumpBooks(booksWithoutAuthors);
			#endregion

			#region Task 6
			//6.Увеличьте количество экземпляров каждой книги на единицу.
			Console.WriteLine("Task 6");
			repository.UpdateBooks(Builders<Book>.Update.Inc(b=> b.Count, 1));

			DumpBooks(repository.GetBooks());
			#endregion

			#region Task 7
			//7. Добавьте дополнительный жанр “favority” всем книгам с жанром “fantasy” 
			//(последующие запуски запроса не должны дублировать жанр “favority”)
			Console.WriteLine("Task 7");
			repository.UpdateBooks(Builders<Book>.Update.AddToSet(b => b.Genres, "favority"), 
									b => b.Genres.Contains("fantasy") && !b.Genres.Contains("favority"));

			DumpBooks(repository.GetBooks());
			#endregion

			#region Task8
			//8. Удалите книги с количеством экземпляров меньше трех.
			Console.WriteLine("Task 8");
			repository.DeleteBooks(b => b.Count < 3);
			DumpBooks(repository.GetBooks());
			#endregion

			#region Task 9
			//9.Удалите все книги.
			repository.DeleteBooks();
			#endregion
		}

		private static void DumpBooks(IEnumerable<Book> books)
		{
			foreach(var book in books)
			{
				Console.WriteLine($"Name: {book.Name} Author: {book.Author} Year: {book.Year} Count: {book.Count} Genres: {string.Join(",", book.Genres)}");
			}
		}
	}
}
