using System.Threading;
using System.Threading.Tasks;

namespace FilmIndustryNetwork.Utilities
{
    public interface IDataCollector
    {
        void Run();

        Task RunAsync();

        Thread RunInParallel();
    }
}