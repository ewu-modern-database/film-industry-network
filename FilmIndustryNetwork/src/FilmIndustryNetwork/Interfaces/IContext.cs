using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neo4jClient;
using Neo4jClient.Cypher;

namespace FilmIndustryNetwork.Interfaces
{
    public interface IContext
    {
        void RunRawQuery(string query);

        ICypherFluentQuery PersonCreate();

        ICypherFluentQuery MovieCreate();

        ICypherFluentQuery PersonMatch();

        ICypherFluentQuery PersonMatchWithRelationships();

        ICypherFluentQuery MovieMatch();

        ICypherFluentQuery MovieMatchWithRelationships();

        ICypherFluentQuery Match(params string[] matchText);

        ICypherFluentQuery MovieMerge();

        ICypherFluentQuery PersonMerge();

        IRawGraphClient RawClient();
    }
}
