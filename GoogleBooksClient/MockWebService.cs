using System;
using System.Collections.Generic;
using BookHelper;
using Contracts;

namespace GoogleBooksClient
{
    public class MockWebService : IBookWebService
    {
        public event EventHandler<string> Error;

        public List<IBook> SearchBooks(string searchTerm)
        {
            List<IAuthor> authors = new List<IAuthor>()
            {
                new Author("J R", "Rowling"),
                new Author("Daniel", "Default")
            };

            return new List<IBook>()
            {
                new Book("1234", "TestBuch", "TEstDescription", authors, "https://timedotcom.files.wordpress.com/2014/07/301386_full1.jpg", "https://books.google.de/books?id=swJWQ5qq804C&hl=de&source=gbs_similarbooks"),
                new Book("4321", "TestBuch2", "TEstDescription2", authors, "https://timedotcom.files.wordpress.com/2014/07/301386_full1.jpg", "https://books.google.de/books?id=swJWQ5qq804C&hl=de&source=gbs_similarbooks")
            };
        }
    }
}