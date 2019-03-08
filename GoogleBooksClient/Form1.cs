using Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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

        const string Button_Search_Running_Text = "Suche abbrechen";
        const string Button_Search_Not_Running_Text = "Suche";

        private bool _searchIsRunning = false;
        public bool SearchIsRunning
        {
            get { return _searchIsRunning; }
            set
            {
                _searchIsRunning = value;
                if (value)
                {
                    _cts = new CancellationTokenSource();
                    progressBar.Visible = true;
                    buttonSearch.Text = Button_Search_Running_Text;
                }
                else
                {
                    _cts?.Cancel();
                    progressBar.Visible = false;
                    buttonSearch.Text = Button_Search_Not_Running_Text;
                }
                
            }
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

        CancellationTokenSource _cts;




        public async void SearchBooks(CancellationToken token)
        {
            var bookResults =
                   new ObservableCollection<IBook>(
                       await Global.WebService.SearchBooks(
                           tbSearchTerm.Text,
                           token,
                           percentage => progressBar.Value = percentage));


            if (token.IsCancellationRequested)
                return;

            //nach dem Anstoßen der Suche die Progressbar einblenden
            //Button umbennen in "Suche stoppen"
            //wenn Suche fertig ist, Button wieder in "Suche" umbennen und ProgressBar ausblenden
            //wenn während der Suche der button geklickt wird: Suche abbrechen

            #region für Linq-Puristen
            //bookResults.ToList().ForEach(b => b.IsFavorite = Global.FavoriteManager.CheckIsFavorite(b));
            #endregion

            foreach (var book in bookResults)
            {
                book.IsFavorite = Global.FavoriteManager.CheckIsFavorite(book);
            }

            LastResult = bookResults;
            SearchIsRunning = false;
        }

        private void button_search_click(object sender, EventArgs e)
        {
            if(!SearchIsRunning)
            {
                SearchIsRunning = true;
                progressBar.Value = 10;

                SearchBooks(_cts.Token);
            }
            else
            {
                SearchIsRunning = false;
            }
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

        private void button_plugin_click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Assemblies|*.dll";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string filename = dialog.FileName;
                Assembly pluginAssembly = Assembly.LoadFrom(filename);
                Type[] typesInAssembly = pluginAssembly.GetTypes();
                foreach (var type in typesInAssembly)
                {
                    Type[] interfaces = type.GetInterfaces();
                    foreach (var @interface in interfaces)
                    {
                        if (@interface == typeof(IBookPlugin))
                        {
                            IBookPlugin plugin = (IBookPlugin)Activator.CreateInstance(type);
                            Global.Plugins.Add(plugin);
                            MessageBox.Show($"Plugin {type.Name} wurde gefunden und installiert!");
                        }
                    }
                }
            }
        }
    }
}
