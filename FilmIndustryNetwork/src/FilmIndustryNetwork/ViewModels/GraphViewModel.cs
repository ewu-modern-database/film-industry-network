using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmIndustryNetwork.Entities.Graph;

namespace FilmIndustryNetwork.ViewModels
{
    public class GraphViewModel
    {
        public GraphViewModel() { }

        public GraphViewModel(IEnumerable<MixedResult> results)
        {
            Nodes = new List<Node>();
            Links = new List<Link>();

            Links = results.Select(r => r.Relationships.Select(rel => new Link
                                   {
                                       Source = Convert.ToString(rel.StartNodeReference.Id),
                                       Target = Convert.ToString(rel.EndNodeReference.Id),
                                       Type = rel.TypeKey
                                   }).ToList())
                           .ToList()
                           .SelectMany(x => x.Select(l => l))
                           .ToList();

            ((List<Node>) Nodes).AddRange(
                results.SelectMany(r => r.Nodes.Select(node => Cast(node.Data, node.Reference.Id.ToString()))));
            
            var dict = new Dictionary<string, Node>();
            foreach (var node in Nodes)
            {
                if (!dict.ContainsKey(node.Id))
                    dict.Add(node.Id, node);
            }
            Nodes = dict.Values.ToList();
        }

        public ICollection<Node> Nodes { get; set; }
        public ICollection<Link> Links { get; set; }

        private Node Cast(MixedData data, string id)
        {
            if (data.Name == null)
            {
                return new MovieNode
                {
                    Countries = data.Countries,
                    FilmingLocations = data.FilmingLocations,
                    Genres = data.Genres,
                    Id = id,
                    Languages = data.Languages,
                    Plot = data.Plot,
                    Rated = data.Rated,
                    Rating = data.Rating,
                    Title = data.Title,
                    Year = data.Year
                };
            }
            else
            {
                return new PersonNode
                {
                    Bio = data.Bio,
                    BirthName = data.BirthName,
                    DateOfBirth = data.DateOfBirth,
                    Id = id,
                    Name = data.Name,
                    PlaceOfBirth = data.PlaceOfBirth,
                    UrlPhoto = data.UrlPhoto
                };
            }
        }
    }

    /// <summary>
    /// this is just to make the returned json cleaner
    /// </summary>
    public abstract class Node
    {
        public string Id { get; set; }
    }

    public class MovieNode : Node
    {
        public string Title { get; set; }
        public string Plot { get; set; }
        public string Rated { get; set; }
        public string Rating { get; set; }
        public List<string> Genres { get; set; }
        public List<string> FilmingLocations { get; set; }
        public List<string> Countries { get; set; }
        public List<string> Languages { get; set; }
        public string Year { get; set; }
    }

    public class PersonNode : Node
    {
        public string Name { get; set; }
        public string BirthName { get; set; }
        public string Bio { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string UrlPhoto { get; set; }
    }

    public class Link
    {
        public string Source { get; set; }
        public string Target { get; set; }
        public string Property => "\"is type\"";
        public string Type { get; set; }
    }
}
