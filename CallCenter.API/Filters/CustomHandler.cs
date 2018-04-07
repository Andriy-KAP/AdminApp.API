using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CallCenter.API.Filters
{
    public class CustomHandler: DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken cancellationToken)
        {
            if(request.Method != HttpMethod.Post)
            {
                TaskCompletionSource<HttpResponseMessage> tcs = new TaskCompletionSource<HttpResponseMessage>();
                tcs.SetResult(new HttpResponseMessage(HttpStatusCode.BadRequest)
                { ReasonPhrase = "Empty body not allowed for POST." });
                return tcs.Task;
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}