using System.Collections.Generic;
using FilmIndustryNetwork.Utilities;
using FilmIndustryNetwork.Entities;


namespace FilmIndustryNetwork.Interfaces
{
    public interface IPersonService
    {
        Person CreatePerson(string Id, string Name, string BirthName, string Bio, string PlaceOfBirt, string DateOfBirth, string UrlPhoto, bool NeedsApiLookup);
        Person UpdatePerson(string Id, string Name, string BirthName, string Bio, string PlaceOfBirt, string DateOfBirth, string UrlPhoto);
        Person GetPerson(string Id);
        void DeletePerson(string Id);

    }
}
