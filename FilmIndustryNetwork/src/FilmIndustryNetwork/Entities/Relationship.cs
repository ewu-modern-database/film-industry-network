using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmIndustryNetwork.Entities
{
    /// <summary>
    /// Class is not mapped to database
    /// </summary>
    public class Relationship
    {
        public string MovieId { get; set; }
        public string RelationType { get; set; }
        public string PersonName { get; set; }
    }
}
