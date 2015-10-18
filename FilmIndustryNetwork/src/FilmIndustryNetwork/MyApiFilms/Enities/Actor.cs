using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmIndustryNetwork.MyApiFilms.Enities
{
    public class Actor
    {
        public string ActorId { get; set; }
        public string ActorName { get; set; }
        public string Character { get; set; }
        public bool Main { get; set; }
        public string UrlCharacter { get; set; }
        public string UrlPhoto { get; set; }
        public string UrlProfile { get; set; }
    }
}
