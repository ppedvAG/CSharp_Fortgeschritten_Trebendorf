using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBookPlugin
    {
        /// <summary>
        /// Erweitere BookDisplayer
        /// </summary>
        /// <param name="panel">Bei Windows Forms wird FlowLayoutPanel übergeben</param>
        /// <param name="book">Das anzuzeigende Buch</param>
        void AddFurtherBookItems(object panel, IBook book);
    }
}
