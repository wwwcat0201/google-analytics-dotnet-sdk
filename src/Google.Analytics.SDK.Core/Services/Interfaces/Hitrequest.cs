﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Google.Analytics.SDK.Core.Helper;

namespace Google.Analytics.SDK.Core.Services.Interfaces
{

    


    public class Hitrequest : MustInitialize<Hit>, IRequest
    {
        public HttpClient Client { get; }
        public string Parms { get; }
        public Hit RequestHit { get; }
        public string RequestType { get; private set; }

        public Hitrequest(Hit requestHit) : base(requestHit)
        {
            RequestHit = requestHit;
            Client = HttpClientFactory.CreateClient();
            RequestType = HttpClientRequestType.Get;
            Parms = requestHit.GetRequest();

        }

        public async Task<IResult> ExecuteCollectAsync()
        {
            var results = await ExecuteAsync(GoogleAnalyticsEndpoints.Collect);

            return new CollectResult(results);
        }

        public async Task<IResult> ExecuteDebugAsync()
        {
            var results =  await ExecuteAsync(GoogleAnalyticsEndpoints.Debug);

            return new DebugResult(results);
        }

        private async Task<string> ExecuteAsync(string type)
        {
            if (HttpClientRequestType.Post.Equals(RequestType))
            {
                var stringContent = new StringContent("");  // TODO Fix post.
                return ""; // await Client.PostAsync(type, stringContent);
                
            }
            try
            {
                var hold = GoogleAnalyticsEndpoints.Host + type + "?" + Parms;
                return  await Client.GetStringAsync(type + "?" + Parms);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        async Task<string> IRequest.ExecuteAsync(string type)
        {
            var response = string.Empty;
            if (GoogleAnalyticsRequestType.Collect.Equals(type, StringComparison.OrdinalIgnoreCase))
                response = await Client.GetStringAsync(GoogleAnalyticsEndpoints.Collect);
            if (GoogleAnalyticsRequestType.Batch.Equals(type, StringComparison.OrdinalIgnoreCase))
                response = await Client.GetStringAsync(GoogleAnalyticsEndpoints.Batch);
            if (GoogleAnalyticsRequestType.Debug.Equals(type, StringComparison.OrdinalIgnoreCase))
                response = await Client.GetStringAsync(GoogleAnalyticsEndpoints.Debug);

            return response;
        }
        /// <summary>
        /// Request hits will e sent as HTTP POST when posible.
        /// </summary>
        public void SetRequestTypePost()
        {
            RequestType = HttpClientRequestType.Post;
        }

        /// <summary>
        /// Request hits will be send as HTTP GET when posible
        /// </summary>
        public void SetRequestTypeGet()
        {
            RequestType = HttpClientRequestType.Get;
        }
    }
}