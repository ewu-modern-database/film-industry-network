using System;
using System.Collections.Generic;
using System.Linq;
using FilmIndustryNetwork.Entities;
using FilmIndustryNetwork.Interfaces;
using FilmIndustryNetwork.Utilities;
using Movie = FilmIndustryNetwork.MyApiFilms.Enities.Movie;
using Neo4jClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace FilmIndustryNetwork.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepo;

        public Person CreatePerson(string Id, string Name, string BirthName, string Bio, string PlaceOfBirth, string DateOfBirth, string UrlPhoto, bool NeedsApiLookup)
        {
            if (Name == null)
            {
                throw new ArgumentNullException(nameof(Name));
            }

         

            var person = new Person
            {
                Id = Id,
                Name = Name,
                BirthName = BirthName,
                Bio = Bio,
                PlaceOfBirth = PlaceOfBirth,
                DateOfBirth = DateOfBirth,
                UrlPhoto = UrlPhoto,

              
            };
            _personRepo.Add(person);
            return person;
        }

        public Person UpdatePerson(string Id, string Name, string BirthName, string Bio, string PlaceOfBirt, string DateOfBirth, string UrlPhoto)
        {

        }

        public Person GetPerson(string Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            return _personRepo.Get(Id);
        }

        public void DeletePerson(string Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            _personRepo.Delete(Id);
        }

    }


}
