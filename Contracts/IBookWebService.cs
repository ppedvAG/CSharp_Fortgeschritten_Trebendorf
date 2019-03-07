using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBookWebService
    {
       event EventHandler<string> Error;

       List<IBook> SearchBooks(string searchTerm);
    }
}
