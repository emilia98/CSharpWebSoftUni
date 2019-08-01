using System;
using System.Collections.Generic;
using HTTP.Common;
using HTTP.Enums;
using HTTP.Exceptions;
using HTTP.Requests;
using HTTP.Responses;

namespace WebServer.Routing
{
    public class ServerRoutingTable : IServerRoutingTable
    {
        private readonly Dictionary<HttpRequestMethod, Dictionary<string, Func<IHttpRequest, IHttpResponse>>> routes;

        public ServerRoutingTable()
        {
            this.routes = new Dictionary<HttpRequestMethod, Dictionary<string, Func<IHttpRequest, IHttpResponse>>>
            {
                // <Method, <Path, Func>>
                [HttpRequestMethod.GET] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
                [HttpRequestMethod.POST] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
                [HttpRequestMethod.PUT] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
                [HttpRequestMethod.DELETE] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
            };
        }

        public void Add(HttpRequestMethod method, string path, Func<IHttpRequest, IHttpResponse> func)
        {
            CoreValidator.ThrowIfNull(method, nameof(method));
            CoreValidator.ThrowIfNullOrEmpty(path, nameof(path));
            CoreValidator.ThrowIfNull(func, nameof(func));

            bool? isContained = this.Contains(method, path);

            if (isContained == null)
            {
                throw new BadRequestException(String.Format("Method {0} is not supported.", method));
            }

            if (isContained.Value)
            {
                return;
            }

            this.routes[method].Add(path, func);
        }

        public bool? Contains(HttpRequestMethod requestMethod, string path)
        {
            CoreValidator.ThrowIfNull(requestMethod, nameof(requestMethod));
            CoreValidator.ThrowIfNullOrEmpty(path, nameof(path));

            
            if(!this.routes.ContainsKey(requestMethod))
            {
                return null;
            }

            return this.routes[requestMethod].ContainsKey(path);
        }

        public Func<IHttpRequest, IHttpResponse> Get(HttpRequestMethod requestMethod, string path)
        {
            CoreValidator.ThrowIfNull(requestMethod, nameof(requestMethod));
            CoreValidator.ThrowIfNullOrEmpty(path, nameof(path));

             bool? isContained = this.Contains(requestMethod, path);

            if (isContained == null)
            {
                throw new BadRequestException(String.Format("Method {0} is not supported.", requestMethod));
            }

            if (!isContained.Value)
            {
                return null;
            }


            return this.routes[requestMethod][path];
        }
    }
}
