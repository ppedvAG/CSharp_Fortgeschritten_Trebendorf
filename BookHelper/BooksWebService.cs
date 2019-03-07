using Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookHelper
{
    public class BooksWebService : IBookWebService
    {

        #region Das verbirgt sich hinter dem Event-Schlüsselwort
        //private EventHandler<string> _error;
        //public event EventHandler<string> Error
        //{
        //    add
        //    {
        //        _error += value;
        //    }
        //    remove
        //    {
        //        _error -= value;
        //    }
        //}
        #endregion

        public event EventHandler<string> Error;

        public List<IBook> SearchBooks(string searchTerm)
        {

            if(string.IsNullOrWhiteSpace(searchTerm))
            {
                //Code von außen aufgerufen werden
                Error?.Invoke(this, "Suchbegriff darf nicht leer sein!");
                return new List<IBook>();
            }

            try
            {
                //https://developers.google.com/apis-explorer/#p/books/v1/books.volumes.list

                HttpClient client = new HttpClient();
                //TODO: Asynchron abrufen
                string json = client.GetStringAsync($"https://www.googleapis.com/books/v1/volumes?q={searchTerm.Replace(" ", "+")}").Result;
                GoogleBooksAPIResult apiResult = JsonConvert.DeserializeObject<GoogleBooksAPIResult>(json);

                List<IBook> books = new List<IBook>();

                //Wandle Buchtreffer in Book-Instanzen um
                foreach (var item in apiResult.items)
                {
                    string ID = item?.id;
                    string title = item?.volumeInfo?.title;
                    string description = item?.volumeInfo?.description;
                    string imageURL = item?.volumeInfo?.imageLinks?.smallThumbnail;
                    string previewLink = item?.volumeInfo?.previewLink;

                    List<IAuthor> authors = new List<IAuthor>();
                    if (item?.volumeInfo?.authors != null)
                    {

                        foreach (var author in item.volumeInfo.authors)
                        {
                            string[] authorParts = author.Split(' ');
                            string forename = authorParts[0];
                            string surname = authorParts.Length > 0 ? authorParts[1] : string.Empty;

                            authors.Add(new Author(forename, surname));
                        }
                    }

                    Book newBook = new Book(ID, title, description, authors, imageURL, previewLink);
                    books.Add(newBook);
                }

                return books;
            }
            catch (Exception exp)
            {
                Error?.Invoke(this, exp.Message);
                return new List<IBook>();
            }
        }
    }
}
