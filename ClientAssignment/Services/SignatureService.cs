using BackendCommon;
using ClientAssignment.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            var response = await this._restService.GetAsync($"signatures/render?signature_id={id}", cancellationToken);
            this.EnsureSuccess(response);
            var dataObj = JObject.Parse(response);
            var html = dataObj["html"].Value<string>();
            return html;
        }
    }
}
