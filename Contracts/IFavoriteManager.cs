using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IFavoriteManager
    {
        /// <summary>
        /// Füge Buch als Favorit hinzu
        /// </summary>
        /// <param name="book">True falls erfolgreich</param>
        /// <returns></returns>
        bool SetAsFavorite(IBook book);
        bool RemoveAsFavorite(IBook book);
        List<IBook> GetFavorites();
        bool CheckIsFavorite(IBook book);

        event EventHandler<string> Error;
    }
}
