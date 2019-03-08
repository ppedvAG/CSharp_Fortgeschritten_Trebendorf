using BookHelper;
using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace GoogleBooksClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Bootstrapping
            //Testmodus
#if !DEBUG
            //Testmodus
            Global.Container.RegisterType<IBookWebService, MockWebService>();
            Global.Container.RegisterType<IFavoriteManager, MockFavoriteManager>();
#else
            //Live-Modus
            Global.Container.RegisterType<IBookWebService, BooksWebService>();
            Global.Container.RegisterType<IFavoriteManager, FavoriteManager>();
#endif


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
