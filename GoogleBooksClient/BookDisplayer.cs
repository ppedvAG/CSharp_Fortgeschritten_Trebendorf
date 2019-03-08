using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Contracts;

namespace GoogleBooksClient
{
    public partial class BookDisplayer : UserControl
    {
        public IBook CurrentBook { get; private set; }

        public BookDisplayer(IBook currentBook)
        {
            InitializeComponent();

            CurrentBook = currentBook;

            checkBoxFavorit.Checked = currentBook.IsFavorite;

            //EventHandler erst registrieren, wenn der Startwert bereits für die Checkbox gesetzt wurde!
            checkBoxFavorit.CheckedChanged += CheckBoxFavorit_CheckedChanged;

            string description = string.Empty;
            if(currentBook.Description != null)
            {
                description = currentBook.Description.Substring(0, Math.Min(currentBook.Description.Length, 50));
            }


            #region ohne string.Join
            //string authors = string.Empty;
            //currentBook.Authors.ForEach(a => authors += $"{a.Forename} {a.Surname}, ");
            #endregion

            labelDescription.Text = $"{currentBook.Name}\n{description}\n{string.Join(", ", currentBook.Authors)}";

            foreach (var plugin in Global.Plugins)
            {
                plugin.AddFurtherBookItems(panelBookItems, CurrentBook);
            }
        }

        private void CheckBoxFavorit_CheckedChanged(object sender, EventArgs e)
        {
            CurrentBook.IsFavorite = checkBoxFavorit.Checked;
            if(CurrentBook.IsFavorite)
            {
                Global.FavoriteManager.SetAsFavorite(CurrentBook);
            }
            else
            {
                Global.FavoriteManager.RemoveAsFavorite(CurrentBook);
            }
        }
    }
}