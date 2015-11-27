using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmIndustryNetwork
{
    public class AppSettings
    {
        public string MyApiFilmsToken { get; set; }
        public string Neo4jConnection { get; set; }
        public string Neo4jUserName { get; set; }
        public string Neo4jPassword { get; set; }
    }
}
