using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmIndustryNetwork.Interfaces;
using Microsoft.Framework.OptionsModel;
using Neo4jClient;
using Neo4jClient.Cypher;

namespace FilmIndustryNetwork.Services
{
    public class NetworkContext : IContext
    {
        private readonly IGraphClient _db;
        public NetworkContext(IOptions<AppSettings> options)
        {
            var appSettings = options.Options;
            _db = new GraphClient(new Uri(appSettings.Neo4jConnection), appSettings.Neo4jUserName,
                appSettings.Neo4jPassword);
            _db.Connect();
        }

        public ICypherFluentQuery PersonCreate()
        {
            return _db.Cypher.Create("(person:Person {newPerson})");
        }

        public ICypherFluentQuery MovieCreate()
        {
            return _db.Cypher.Create("(movie:Movie {newMovie})");
        }

        public ICypherFluentQuery PersonMatch()
        {
            return Match("(person:Person)");
        }

        public ICypherFluentQuery MovieMatch()
        {
            return Match("(movie:Movie)");
        }

        public ICypherFluentQuery Match(params string[] matchText)
        {
            return _db.Cypher.Match(matchText);
        }

        public IRawGraphClient RawClient()
        {
            return (IRawGraphClient) _db;
        }
    }
}
