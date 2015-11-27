using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using FilmIndustryNetwork.Utilities;
using Microsoft.Framework.WebEncoders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FilmIndustryNetwork.MyApiFilms
{
    public class MyApiFilmsClient
    {
        private HttpWebRequest request;
        private HttpWebResponse response;
        private readonly string _token;

        public MyApiFilmsClient(string token)
        {
            _token = token;
        }

        public string GetDataAsJson(string arg, DataSetType type)
        {
            if (arg == null) throw new ArgumentNullException(nameof(arg));
            arg = WebUtility.UrlEncode(arg.Replace(" ", "+"));
            var url = "";
            url = type == DataSetType.Person
                ? MyApiFilmsUrlGenerator.CreateImdbPersonUrl(arg, _token)
                : MyApiFilmsUrlGenerator.CreateImdbMovieUrl(arg, _token);

            var json = "";

            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 10000;
                request.Method = WebRequestMethods.Http.Get;
                response = (HttpWebResponse)request.GetResponse();
                var streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                json = streamReader.ReadToEnd();
            }
            catch (Exception e)
            {
                if (e.Message == "The operation has timed out")
                    throw new MyApiFilmsTimeoutException(url);
                return null;
            }

            if (json.Contains("\"error\":"))
                throw new NoMyApiFilmsResponseException(url);

            return json;
        }

        public TObject GetDataAsObject<TObject>(string arg, DataSetType type)
        {
            var json = GetDataAsJson(arg, type);

            if (json == null)
                return default(TObject); // return null

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
