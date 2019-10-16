using System;
using System.Net;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace BackendCommon
{
    public interface IRestService
    {
        string BaseAddress { get; }
        CookieContainer Cookies { get; set; }
        HttpRequestHeaders RequestHeaders { get; }
        TimeSpan Timeout { get; set; }

        Task<string> DeleteAsync(string link);
        Task<string> DeleteAsync(string link, CancellationToken cancellationToken);
        Task<string> GetAsync(string link);
        Task<string> GetAsync(string link, CancellationToken cancellationToken);
        Task<string> PostAsync(string link, string jsonData);
        Task<string> PostAsync(string link, string jsonData, CancellationToken cancellationToken);
        Task<string> PutAsync(string link, string jsonData);
        Task<string> PutAsync(string link, string jsonData, CancellationToken cancellationToken);
    }
}