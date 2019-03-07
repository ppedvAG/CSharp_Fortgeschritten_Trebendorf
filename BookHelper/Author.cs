using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHelper
{
    public class Author : IAuthor
    {
        public string Forename { get; private set; }

        public string Surname { get; private set; }

        public Author(string forename, string surname)
        {
            Forename = forename;
            Surname = surname;
        }

        public override string ToString()
        {
            return $"{Forename} {Surname}";
        }
    }
}
