using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neo4jClient;

namespace FilmIndustryNetwork.Entities.Graph
{
    public class MixedResult
    {
        public IEnumerable<Node<MixedData>> Nodes { get; set; }
        public IEnumerable<RelationshipInstance<object>> Relationships { get; set; }
    }
}
