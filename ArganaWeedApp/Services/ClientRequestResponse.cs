using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArganaWeedApp.Services
{
    public class ClientRequestResponse<TRequest, TResponse, TItem>
        where TRequest : BaseRequest
        where TResponse : BaseResponse<TItem>, new()
    {
        private readonly TRequest request;
        private readonly string target;

        public ClientRequestResponse(string target)
        {
            this.target = target;
        }

        public ClientRequestResponse(TRequest request, string target)
        {
            this.request = request;
            this.target = target;
        }

        public async Task<TResponse> ReceivePost()
        {
            TResponse res = null;
            try
            {
                string raw = ServiceSerializer<TRequest>.Serialize(request);
                HttpService service = new HttpService();
                string result = await service.PostAsync(target, raw);
                res = ServiceSerializer<TResponse>.Deserialize(result);
            }
            catch (Exception ex)
            {
                throw new RemoteCallException($"Error while Calling server {target}", ex);
            }
            return res;
        }

        public async Task<TResponse> ReceiveGet()
        {
            TResponse res = null;
            try
            {
                HttpService service = new HttpService();
                string result = await service.GetAsync(target);
                res = ServiceSerializer<TResponse>.Deserialize(result);
            }
            catch (Exception ex)
            {
                throw new RemoteCallException($"Error while Calling server {target}", ex);
            }
            return res;
        }
    }
}














