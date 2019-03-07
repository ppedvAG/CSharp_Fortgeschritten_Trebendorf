using Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHelper
{
    public class FavoriteManager : MockFavoriteManager
    {
        public const string File_Name = "MyFavorites.mf";
        private JsonSerializerSettings _settings;

        public FavoriteManager()
        {
            _settings = new JsonSerializerSettings();
            _settings.TypeNameHandling = TypeNameHandling.Objects;


            if(File.Exists(File_Name))
            {
                //Deserialisierung: string (XML/JSON) -> .NET Objekt
                string favoritesAsJson = File.ReadAllText(File_Name);
                base._favoriteBooks =  new List<IBook>(JsonConvert.DeserializeObject<List<Book>>(favoritesAsJson, _settings));
            }
            else {
                base._favoriteBooks = new List<IBook>();
            }
        }

        public override void SaveFavorites()
        {
            //Serialisierung: .NET-Objekt -> string (XML/JSON)
            string favoritesAsJson = JsonConvert.SerializeObject(base._favoriteBooks,_settings);
            File.WriteAllText(File_Name, favoritesAsJson);

            base.SaveFavorites();
        }
    }
}
