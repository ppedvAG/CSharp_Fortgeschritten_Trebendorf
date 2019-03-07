using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHelper
{
    public class Book : IBook
    {
       

        public string ID { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public List<IAuthor> Authors { get; private set; }

        public bool IsFavorite { get; set; }

        public string ImageURL { get; private set; }


        public string PreviewURL { get; private set; }

        public Book(string iD, string name, string description, List<IAuthor> authors, string imageURL, string previewURL, bool isFavorite = false)
        {
            ID = iD;
            Name = name;
            Description = description;
            Authors = authors;
            IsFavorite = isFavorite;
            ImageURL = imageURL;
            PreviewURL = previewURL;
        }
    }
}
