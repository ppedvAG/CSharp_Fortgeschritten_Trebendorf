using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookHelper;
using Contracts;

namespace GoogleBooksClient
{
    public class MockWebService : IBookWebService
    {
        public event EventHandler<string> Error;

        public async Task<List<IBook>> SearchBooks(string searchTerm, CancellationToken token, Action<int> progressCallBack)
        {
            List<IAuthor> authors = new List<IAuthor>()
            {
                new Author("J R", "Rowling"),
                new Author("Daniel", "Default")
            };

            //Dieser Aufruf ist notwendig, damit mindestens ein await in der Methoide enthalten ist
            //ansonsten wird kein Task-Objekt zurückgegebenm wie es im Interface gefordert ist
            await Task.Delay(0);

            return new List<IBook>()
            {
                new Book("1234", "TestBuch", "TEstDescription", authors, "https://timedotcom.files.wordpress.com/2014/07/301386_full1.jpg", "https://books.google.de/books?id=swJWQ5qq804C&hl=de&source=gbs_similarbooks"),
                new Book("4321", "TestBuch2", "TEstDescription2", authors, "https://timedotcom.files.wordpress.com/2014/07/301386_full1.jpg", "https://books.google.de/books?id=swJWQ5qq804C&hl=de&source=gbs_similarbooks")
            };
        }
    }
}