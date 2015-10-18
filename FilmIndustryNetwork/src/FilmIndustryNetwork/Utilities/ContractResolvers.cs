using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace FilmIndustryNetwork.Utilities
{
    public class JsonCamelCaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            if (propertyName == "IMDBid")
                return "IMDBId";
            return propertyName.Substring(0, 1).ToUpper() + propertyName.Substring(1);
        }
    }
}
