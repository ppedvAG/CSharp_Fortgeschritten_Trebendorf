using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBookWebService
    {
       event EventHandler<string> Error;

       Task<List<IBook>> SearchBooks(string searchTerm,CancellationToken token, Action<int> progressCallBack);
    }
}
