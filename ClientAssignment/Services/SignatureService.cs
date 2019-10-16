using System.Threading;
using System.Threading.Tasks;
using BackendCommon;
using ClientAssignment.Interfaces;
using Newtonsoft.Json.Linq;

namespace ClientAssignment.Services
{
    public class SignatureService : ServiceBase, ISignatureService
    {
        private readonly IRestService _restService;
        public SignatureService(IRestService restService)
        {
            this._restService = restService;
        }

        public async Task<string> GetHtmlById(string id, CancellationToken cancellationToken)
        {
            string response = await this._restService.GetAsync($"signatures/render?signature_id={id}", cancellationToken);
            this.EnsureSuccess(response);
            var dataObj = JObject.Parse(response);
            string html = dataObj["html"].Value<string>();
            return html;
        }
    }
}
