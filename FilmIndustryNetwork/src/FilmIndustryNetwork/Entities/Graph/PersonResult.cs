using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neo4jClient;

namespace FilmIndustryNetwork.Entities.Graph
{
    public class PersonResult
    {
        public IEnumerable<Node<Person>> PersonNodes { get; set; }
        public IEnumerable<Node<Movie>> MovieNodes { get; set; }
        public IEnumerable<RelationshipInstance<object>> Relationships { get; set; }
    }
}
