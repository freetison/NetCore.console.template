using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ncapp.Services
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> Ping();
    }
}