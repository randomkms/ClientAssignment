using ClientAssignment.Models;
using Newtonsoft.Json;

namespace ClientAssignment.Services
{
    public abstract class ServiceBase
    {
        protected void EnsureSuccess(string response)
        {
            ApiException apiException = null;
            try
            {
                apiException = JsonConvert.DeserializeObject<ApiException>(response, new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error });
            }
            catch
            {
            }

            if (apiException != null)
            {
                throw apiException;
            }
        }
    }
}
