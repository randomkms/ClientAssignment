using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BackendCommon;
using ClientAssignment.Interfaces;
using ClientAssignment.Models;
using Newtonsoft.Json.Linq;

namespace ClientAssignment.Services
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly IRestService _restService;
        public UserService(IRestService restService)
        {
            this._restService = restService;
        }

        public async Task<User> GetById(string id, CancellationToken cancellationToken)
        {
            string response = await this._restService.GetAsync($"users/get?uid={id}&without_signatures=true", cancellationToken);
            this.EnsureSuccess(response);
            var dataObj = JObject.Parse(response);
            var user = new User
            {
                Id = id,
                Email = dataObj["user"]["email"].Value<string>()
            };

            var signaturePairs = dataObj["signatures"].Children();
            var signatures = new List<Signature>(signaturePairs.Count());
            foreach (var signaturePair in signaturePairs)
            {
                var signatureObj = signaturePair.First();
                var signature = new Signature
                {
                    Id = signatureObj["id"].Value<string>(),
                    Title = signatureObj["title"].Value<string>()
                };
                signatures.Add(signature);
            }

            user.Signatures = new ObservableCollection<Signature>(signatures);
            return user;
        }
    }
}
