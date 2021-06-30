using System;
using System.Threading.Tasks;

namespace ncapp.Services
{
    public interface IHttpService
    {
        Task<string> Ping(Uri uri);
    }
}