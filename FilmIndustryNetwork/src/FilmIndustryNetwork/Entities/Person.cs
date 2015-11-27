using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FilmIndustryNetwork.Entities
{
    public class Person
    {
        #region Properties Stored in Node
        public string Id { get; set; }
        public string Name { get; set; }
        public string BirthName { get; set; }
        public string Bio { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string UrlPhoto { get; set; }
        /// <summary>
        /// This is a flag so that we know that this node
        /// needs to be looked up still for info
        /// </summary>
        public bool NeedsApiLookup { get; set; }
        #endregion
    }
}
