using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ArganaWeedApp.Services
{
    public class RemoteCallException : Exception
    {
        public string UserMessage { get; set; }
        public bool IsReachable { get; set; }

        public RemoteCallException(string message, Exception inner) : base(message, inner)
        {
            UserMessage = "RemoteServerError";
            IsReachable = true;

            if (inner is WebException)
            {
                WebException exception = (WebException)inner;

                if (exception.Status == WebExceptionStatus.ConnectFailure)
                {
                    UserMessage = "ConnectFailure";
                    IsReachable = false;
                }
                else if (exception.Status == WebExceptionStatus.NameResolutionFailure)
                {
                    UserMessage = "NameResolutionFailure";
                    IsReachable = false;
                }
                else if (exception.Status == WebExceptionStatus.ProtocolError)
                {
                    if (inner.Message.Contains("404"))
                    {
                        UserMessage = "RemoteServerNotFound";
                        IsReachable = false;
                    }
                    else if (inner.Message.Contains("401"))
                    {
                        UserMessage = "RemoteServerUnAuthorized";
                        IsReachable = false;
                    }
                }
            }
        }
    }
}
