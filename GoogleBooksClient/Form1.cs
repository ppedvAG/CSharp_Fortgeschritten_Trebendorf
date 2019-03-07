using Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoogleBooksClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ObservableCollection<IBook> _lastResult;
        private ObservableCollection<IBook> LastResult
        {
            get => _lastResult;
            set
            {
                _lastResult = value;
                UpdatePanel();
                _lastResult.CollectionChanged += _lastResult_CollectionChanged;
            }
        }

        private void _lastResult_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdatePanel();
        }

        private void button_search_click(object sender, EventArgs e)
        {
            //Kopierkonstruktor
            var bookResults = new ObservableCollection<IBook>(Global.WebService.SearchBooks(tbSearchTerm.Text));

            #region für Linq-Puristen
            //bookResults.ToList().ForEach(b => b.IsFavorite = Global.FavoriteManager.CheckIsFavorite(b));
            #endregion

            foreach (var book in bookResults)
            {
                book.IsFavorite = Global.FavoriteManager.CheckIsFavorite(book); 
            }

            LastResult = bookResults;
        }

        private void UpdatePanel()
        {
            panelBookResults.Controls.Clear();
            foreach (var book in _lastResult)
            {
                BookDisplayer bookDisplayer = new BookDisplayer(book);
                panelBookResults.Controls.Add(bookDisplayer);
            }
        }

        private void button_favorite_click(object sender, EventArgs e)
        {
            LastResult = new ObservableCollection<IBook>(Global.FavoriteManager.GetFavorites());
        }
    }
}
