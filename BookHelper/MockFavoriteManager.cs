using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;

namespace BookHelper
{
    public class MockFavoriteManager : IFavoriteManager
    {

        public event EventHandler<string> Error;

        protected List<IBook> _favoriteBooks = null;

        public MockFavoriteManager()
        {
            _favoriteBooks = new List<IBook>();
        }

        public bool CheckIsFavorite(IBook book)
        {
            return _favoriteBooks.Any(b => b.ID == book.ID);
        }

        public List<IBook> GetFavorites()
        {
            return _favoriteBooks;
        }

        public bool RemoveAsFavorite(IBook book)
        {
            IBook bookToRemove = _favoriteBooks.SingleOrDefault(b => b.ID == book.ID);
            if(bookToRemove == null)
            {
                Error?.Invoke(this, "Buch kann nicht als Favorit entfernt werden! Wenden Sie sich an den Support!");
                return false;
            }

            _favoriteBooks.Remove(book);
            SaveFavorites();
            return true;
        }

        public virtual void SaveFavorites()
        {
           
        }

        public bool SetAsFavorite(IBook book)
        {
            if(_favoriteBooks.Any(b => b.ID == book.ID))
            {
                Error?.Invoke(this, "Buch ist bereits ein Favorit! Wenden Sie sich an den Support!");
                return false;
            }
            _favoriteBooks.Add(book);
            SaveFavorites();
            return true;
        }
    }
}