using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmIndustryNetwork.Entities;
namespace FilmIndustryNetwork.Interfaces
{
    public interface IPersonRepository
    {
        Person Get(string id);
        void Add(Person person);
        void Delete(string id);
    }
}
