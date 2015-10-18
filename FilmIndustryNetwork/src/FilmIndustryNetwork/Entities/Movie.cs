using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FilmIndustryNetwork.Entities
{
    public class Movie
    {
        #region Properties Stored in Node
        public string Title { get; set; }
        public string Plot { get; set; }
        public string Rated { get; set; }
        public string Rating { get; set; }
        public List<string> Genres { get; set; } 
        public List<string> FilmingLocations { get; set; }
        public List<string> Countries { get; set; } 
        public List<string> Languages { get; set; }
        public string Year { get; set; }
        /// <summary>
        /// This is a flag so that we know that this node
        /// needs to be looked up still for info
        /// </summary>
        public bool NeedsApiLookup { get; set; }
        #endregion
    }
}
