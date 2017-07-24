using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net;

namespace StudentWeb.Controllers
{
    public class BaseController : Controller
    {
        public readonly string baseUrl = "http://localhost:9247/api";

        public RestClient CreateClient()
        {
            var client = new RestClient(baseUrl);
            client.CookieContainer = new CookieContainer();
            return client;
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}