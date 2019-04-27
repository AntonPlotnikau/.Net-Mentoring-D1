using BookStorage.Interface.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BookStorage.Interface.Interfaces
{
	public interface IBooksRepository
	{
		void AddBook(Book book);

		void AddBooks(IEnumerable<Book> books);

		IEnumerable<Book> GetBooks();

		IEnumerable<Book> GetBooks(Func<Book, bool> predicate);

		void UpdateBooks(UpdateDefinition<Book> updateDefinition, Expression<Func<Book, bool>> filter);

		void UpdateBooks(UpdateDefinition<Book> updateDefinition);

		void DeleteBooks();

		void DeleteBooks(Expression<Func<Book, bool>> filter);

		IEnumerable<TResult> Select<TResult>(Func<Book, TResult> selector);
	}
}
