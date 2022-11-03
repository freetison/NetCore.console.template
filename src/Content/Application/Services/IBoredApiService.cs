using System.Threading.Tasks;

namespace ncapp.Services
{
    public interface IBoredApiService
    {
        Task<string> GetData();
    }
}