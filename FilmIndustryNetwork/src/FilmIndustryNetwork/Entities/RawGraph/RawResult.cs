using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neo4jClient;

namespace FilmIndustryNetwork.Entities.RawGraph
{
    public class RawResult
    {
        public IEnumerable<Node<Data>> Nodes { get; set; }
        public IEnumerable<RelationshipInstance<object>> Relationships { get; set; }
    }

    public class Data
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Plot { get; set; }
        public string Rated { get; set; }
        public string Rating { get; set; }
        public List<string> Genres { get; set; }
        public List<string> FilmingLocations { get; set; }
        public List<string> Countries { get; set; }
        public List<string> Languages { get; set; }
        public string Year { get; set; }
        public string Name { get; set; }
        public string BirthName { get; set; }
        public string Bio { get; set; }
        public string PlaceOfBirth { get; set; }
        public string DateOfBirth { get; set; }
        public string UrlPhoto { get; set; }
    }
}
