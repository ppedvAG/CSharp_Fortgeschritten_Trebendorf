using BookHelper;
using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace GoogleBooksClient
{
    public class Global
    {
        private static UnityContainer _container;
        //Container für Dependency Injection
        public static UnityContainer Container
        {
            get => _container ?? (_container = new UnityContainer());
        }

        private static IBookWebService _webService = null;
        public static IBookWebService WebService
        {
            get
            {
                if (_webService == null)
                {
                    _webService = Container.Resolve<IBookWebService>();
                    _webService.Error += _webService_Error;
                }
                return _webService;
            }

        }
       
        private static IFavoriteManager _favoriteManager;
        public static IFavoriteManager FavoriteManager
        {
            get
            {
                if (_favoriteManager == null)
                {
                    _favoriteManager = Container.Resolve<IFavoriteManager>();
                    _favoriteManager.Error += _webService_Error;
                }
                return _favoriteManager;
            }
        }

        private static void _webService_Error(object sender, string e)
        {
            MessageBox.Show(e);
        }

    }
}
