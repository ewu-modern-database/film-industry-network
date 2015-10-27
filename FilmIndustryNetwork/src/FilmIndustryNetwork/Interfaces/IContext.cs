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
        ICypherFluentQuery PersonCreate();

        ICypherFluentQuery MovieCreate();

        ICypherFluentQuery PersonMatch();

        ICypherFluentQuery MovieMatch();

        ICypherFluentQuery Match(params string[] matchText);

        IRawGraphClient RawClient();
    }
}
