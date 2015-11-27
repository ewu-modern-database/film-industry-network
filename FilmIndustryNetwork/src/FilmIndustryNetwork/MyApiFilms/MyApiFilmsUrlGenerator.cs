using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmIndustryNetwork.MyApiFilms
{
    public class MyApiFilmsUrlGenerator
    {
        private static readonly string _baseUrl = "http://api.myapifilms.com/";
        private static readonly string _token = "token=";
        private static readonly string _name = "name=";
        private static readonly string _format = "format=json";
        private static readonly string _filmography = "filmography=";
        private static readonly string _limit = "limit=1";
        private static readonly string _lang = "language=en-us";
        private static readonly string _exactFilter = "exactFilter=0";
        private static readonly string _bornDied = "bornDied=0";
        private static readonly string _bornAndDied = "bornAndDied=0";
        private static readonly string _starSign = "starSign=0";
        private static readonly string _uniqueName = "uniqueName=1";
        private static readonly string _actorActress = "actorActress=";
        private static readonly string _actorTrivia = "actorTrivia=";
        private static readonly string _actorPhotos = "actorPhotos=0";
        private static readonly string _actorVideos = "actorVideos=0";
        private static readonly string _salary = "salary=0";
        private static readonly string _spouses = "spouses=1";
        private static readonly string _tradeMark = "tradeMark=1";
        private static readonly string _personalQuotes = "personalQuotes=";
        private static readonly string _starMeter = "starMeter=1";
        private static readonly string _title = "title=";
        private static readonly string _aka = "aka=0";
        private static readonly string _business = "business=0";
        private static readonly string _seasons = "seasons=0";
        private static readonly string _seasonYear = "seasonYear=1";
        private static readonly string _filter = "filter=2";
        private static readonly string _technical = "technical=0";
        private static readonly string _forceYear = "forceYear=0";
        private static readonly string _actors = "actors=1";
        private static readonly string _biography = "biography=0";
        private static readonly string _trailers = "trailers=0";
        private static readonly string _movieTrivia = "movieTrivia=1";
        private static readonly string _awards = "awards=0";
        private static readonly string _moviePhotos = "moviePhotos=0";
        private static readonly string _movieVideos = "movieVideos=0";
        private static readonly string _similarMovies = "similarMovies=0";
        private static readonly string _adultSearch = "adultSearch=0";

        public static string CreateImdbMovieUrl(string title, string token)
        {
            return
                $"{_baseUrl}imdb/idIMDB?{_title}{title}&{_token}{token}&{_format}&{_lang}&{_aka}&{_business}&{_seasons}&{_seasonYear}&" +
                $"{_technical}&{_filter}&{_exactFilter}&{_limit}&{_forceYear}&{_trailers}&{_movieTrivia}&{_awards}&{_moviePhotos}&" +
                $"{_movieVideos}&{_actors}&{_biography}&{_uniqueName}&{_filmography}0&{_bornAndDied}&{_starSign}&{_actorActress}0&" +
                $"{_actorTrivia}0&{_similarMovies}&{_adultSearch}";
        }

        public static string CreateImdbPersonUrl(string name, string token)
        {
            return
                $"{_baseUrl}imdb/idIMDB?{_name}{name}&{_token}{token}&{_format}&{_lang}&{_filmography}1&{_exactFilter}&{_limit}&{_starSign}&" +
                $"{_uniqueName}&{_actorActress}1&{_actorTrivia}1&{_actorPhotos}&{_actorVideos}&{_salary}&{_spouses}&{_tradeMark}&" +
                $"{_personalQuotes}1&{_starMeter}";
        }
    }
}
