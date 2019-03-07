using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBook
    {
        string ID { get; }
        string Name { get; }
        string Description { get; }
        List<IAuthor> Authors { get; }
        bool IsFavorite { get; set; }
        string ImageURL { get; }
        string PreviewURL { get; }
    }

}
