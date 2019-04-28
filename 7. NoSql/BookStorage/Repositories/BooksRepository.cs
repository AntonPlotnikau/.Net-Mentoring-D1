using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using BookStorage.Interface.Interfaces;
using BookStorage.Interface.Models;
using MongoDB.Driver;

namespace BookStorage.Repositories
{
	public class BooksRepository : IBooksRepository
	{
		private readonly MongoClient mongoClient;
		private readonly IMongoCollection<Book> bookCollection;

		public BooksRepository(string connection, string databaseName, string collectionName)
		{
			this.mongoClient = new MongoClient(connection);
			var database = mongoClient.GetDatabase(databaseName);
			this.bookCollection = database.GetCollection<Book>(collectionName);
		}

		public void AddBook(Book book)
		{
			bookCollection.InsertOne(book);
		}

		public void AddBooks(IEnumerable<Book> books)
		{
			bookCollection.InsertMany(books);
		}

		public void DeleteBooks()
		{
			bookCollection.DeleteMany(Builders<Book>.Filter.Empty);
		}

		public void DeleteBooks(Expression<Func<Book, bool>> filter)
		{
			bookCollection.DeleteMany(filter);
		}

		public IEnumerable<Book> GetBooks()
		{
			return bookCollection.AsQueryable<Book>();
		}

		public IEnumerable<Book> GetBooks(Func<Book, bool> predicate)
		{
			return bookCollection.AsQueryable<Book>().Where(predicate);
		}

		public IEnumerable<TResult> Select<TResult>(Func<Book, TResult> selector)
		{
			return bookCollection.AsQueryable<Book>().Select(selector);
		}

		public void UpdateBooks(UpdateDefinition<Book> updateDefinition, Expression<Func<Book, bool>> filter)
		{
			bookCollection.UpdateMany(filter, updateDefinition);
		}

		public void UpdateBooks(UpdateDefinition<Book> updateDefinition)
		{
			bookCollection.UpdateMany(Builders<Book>.Filter.Empty, updateDefinition);
		}
	}
}
