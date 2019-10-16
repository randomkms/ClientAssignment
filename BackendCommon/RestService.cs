using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackendCommon
{
    public class RestService : IRestService
    {
        private HttpClientHandler clientHandler;
        private HttpClient client;

        public RestService(string restUrl)
        {
            this.InitializeRestServise(restUrl);
        }

        public HttpRequestHeaders RequestHeaders => this.client.DefaultRequestHeaders;

        public string BaseAddress
        {
            get
            {
                return this.client.BaseAddress.OriginalString;
            }
        }

        public CookieContainer Cookies
        {
            get
            {
                return this.clientHandler.CookieContainer;
            }

            set
            {
                this.InitializeRestServise(this.BaseAddress);
                this.clientHandler.CookieContainer = value;
            }
        }

        public TimeSpan Timeout
        {
            get
            {
                return this.client.Timeout;
            }
            set
            {
                this.client.Timeout = value;
            }
        }

        public Task<string> GetAsync(string link)
        {
            return this.GetAsync(link, CancellationToken.None);
        }

        public async Task<string> GetAsync(string link, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;
            try
            {
                response = await this.client.GetAsync(link, cancellationToken);
                Debug.WriteLine("GetAsync link = {0}; Status code = {1}", link, response.StatusCode);
            }
            catch (TaskCanceledException cancelledEx)
            {
                throw cancelledEx;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR {0}", ex.Message);
            }

            this.EnsureSuccess(response);
            return await response.Content.ReadAsStringAsync();
        }

        public Task<string> PostAsync(string link, string jsonData)
        {
            return this.PostAsync(link, jsonData, CancellationToken.None);
        }

        public async Task<string> PostAsync(string link, string jsonData, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;
            try
            {
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                response = await this.client.PostAsync(link, content, cancellationToken);
                Debug.WriteLine("PostAsync link = {0}; Status code = {1}", link, response.StatusCode);
            }
            catch (TaskCanceledException cancelledEx)
            {
                throw cancelledEx;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR {0}", ex.Message);
            }

            this.EnsureSuccess(response);
            return await response.Content.ReadAsStringAsync();
        }

        public Task<string> PutAsync(string link, string jsonData)
        {
            return this.PutAsync(link, jsonData, CancellationToken.None);
        }

        public async Task<string> PutAsync(string link, string jsonData, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;
            try
            {
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                response = await this.client.PutAsync(link, content, cancellationToken);
                Debug.WriteLine("PutAsync link = {0}; Status code = {1}", link, response.StatusCode);
            }
            catch (TaskCanceledException cancelledEx)
            {
                throw cancelledEx;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR {0}", ex.Message);
            }

            this.EnsureSuccess(response);
            return await response.Content.ReadAsStringAsync();
        }

        public Task<string> DeleteAsync(string link)
        {
            return this.DeleteAsync(link, CancellationToken.None);
        }

        public async Task<string> DeleteAsync(string link, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;
            try
            {
                response = await this.client.DeleteAsync(link, cancellationToken);
                Debug.WriteLine("DeleteAsync link = {0}; Status code = {1}", link, response.StatusCode);
            }
            catch (TaskCanceledException cancelledEx)
            {
                throw cancelledEx;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR {0}", ex.Message);
            }

            this.EnsureSuccess(response);
            return await response.Content.ReadAsStringAsync();
        }

        private void InitializeRestServise(string restUrl)
        {
            this.clientHandler = new HttpClientHandler();
            this.client = new HttpClient(this.clientHandler) { BaseAddress = new Uri(restUrl) };
        }

        private void EnsureSuccess(HttpResponseMessage response)
        {
            HttpStatusCode statusCode;
            string errorMessage;
            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    return;
                }

                statusCode = response.StatusCode;
                errorMessage = response.ReasonPhrase;
            }
            else
            {
                statusCode = HttpStatusCode.RequestTimeout;
                errorMessage = "No connection";
            }

            throw new RestException(statusCode, errorMessage);
        }
    }
}
