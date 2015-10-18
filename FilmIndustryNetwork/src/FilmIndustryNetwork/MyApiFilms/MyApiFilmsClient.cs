using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using FilmIndustryNetwork.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FilmIndustryNetwork.MyApiFilms
{
    public class MyApiFilmsClient
    {
        private HttpWebRequest request;
        private HttpWebResponse response;

        public string GetDataAsJson(string arg, DataSetType type)
        {
            if (arg == null) throw new ArgumentNullException(nameof(arg));
            var url = "";
            if (type == DataSetType.Person)
                url = MyApiFilmsUrlGenerator.CreateImdbPersonUrl(arg);
            else
                url = MyApiFilmsUrlGenerator.CreateImdbMovieUrl(arg);
            request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;

            response = (HttpWebResponse) request.GetResponse();
            
            var streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);

            var json = streamReader.ReadToEnd();
            
            if (json.Contains("Error"))
                throw new NoMyApiFilmsResponseException(url);

            return json;
        }

        public TObject GetDataAsObject<TObject>(string arg, DataSetType type)
        {
            var json = GetDataAsJson(arg, type);
            
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new JsonCamelCaseContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };

            var obj = JsonConvert.DeserializeObject<TObject>(json, jsonSettings);

            return obj;
        }
    }

    public enum DataSetType
    {
        Movie,
        Person
    }
}
