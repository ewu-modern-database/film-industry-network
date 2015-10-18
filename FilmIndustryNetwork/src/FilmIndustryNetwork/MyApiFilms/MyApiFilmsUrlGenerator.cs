using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmIndustryNetwork.MyApiFilms
{
    public class MyApiFilmsUrlGenerator
    {
        private static readonly string _baseUrl = "http://www.myapifilms.com/";
        private static readonly string _name = "name=";
        private static readonly string _format = "format=JSON";
        private static readonly string _fimlography = "filmography=1";
        private static readonly string _limit = "limit=1";
        private static readonly string _lang = "lang=en-us";
        private static readonly string _exactFilter = "exactFilter=0";
        private static readonly string _bornDied = "bornDied=";
        private static readonly string _starSign = "starSign=";
        private static readonly string _uniqueName = "uniqueName=1";
        private static readonly string _actorActress = "actorActress=";
        private static readonly string _actorTrivia = "actorTrivia=";
        private static readonly string _actorPhotos = "actorPhotos=N";
        private static readonly string _actorVideos = "actorVideos=N";
        private static readonly string _salary = "salary=0";
        private static readonly string _spouses = "spouses=1";
        private static readonly string _tradeMark = "tradeMark=1";
        private static readonly string _personalQuotes = "personalQuotes=";
        private static readonly string _starMeter = "starMeter=1";
        private static readonly string _title = "title=";
        private static readonly string _aka = "aka=0";
        private static readonly string _business = "business=0";
        private static readonly string _seasons = "seasons=0";
        private static readonly string _seasonYear = "seasonYear=0";
        private static readonly string _filter = "filter=N";
        private static readonly string _technical = "technical=0";
        private static readonly string _forceYear = "forceYear=0";
        private static readonly string _actors = "actors=S";
        private static readonly string _biography = "biography=0";
        private static readonly string _trailer = "trailer=0";
        private static readonly string _filmography = "filmography=";
        private static readonly string _movieTrivia = "movieTrivia=1";
        private static readonly string _awards = "awards=0";
        private static readonly string _moviePhotos = "moviePhotos=N";
        private static readonly string _movieVideos = "movieVideos=N";
        private static readonly string _similarMovies = "similarMovies=0";
        private static readonly string _adultSearch = "adultSearch=0";

        public static string CreateImdbMovieUrl(string title)
        {
            return
                $"{_baseUrl}imdb?{_title}{title}&{_format}&{_aka}&{_business}&{_seasons}&{_seasonYear}&{_technical}&{_filter}&{_exactFilter}&" +
                $"{_limit}&{_forceYear}&{_actors}&{_biography}&{_trailer}&{_uniqueName}&{_filmography}0&{_bornDied}0&{_starSign}0&" +
                $"{_actorActress}0&{_actorTrivia}0&{_movieTrivia}&{_awards}&{_moviePhotos}&{_movieVideos}&{_similarMovies}&{_adultSearch}";
        }

        public static string CreateImdbPersonUrl(string name)
        {
            return
                $"{_baseUrl}imdb?{_name}{name}&{_format}&{_filmography}1&{_limit}&{_lang}&{_exactFilter}&{_bornDied}1&{_starSign}1&" +
                $"{_uniqueName}&{_actorActress}1&{_actorTrivia}1&{_actorPhotos}&{_actorVideos}&{_salary}&{_spouses}&{_tradeMark}&" +
                $"{_personalQuotes}1&{_starMeter}";
        }
    }
}
