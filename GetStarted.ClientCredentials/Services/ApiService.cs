﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GetStarted.ClientCredentials.Models;
using IdentityModel.Client;
using Newtonsoft.Json;

namespace GetStarted.ClientCredentials.Services
{
    public class ApiService: IApiService
    {
        public async Task<UserModel> GetUser()
        {
            var token = GetToken().Result;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync("http://localhost:65014/users/withAuth").ConfigureAwait(false);
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return JsonConvert.DeserializeObject<UserModel>(result);
            }
        }

        private async Task<string> GetToken()
        {
            var address = "http://localhost:53189/connect/token";
            var client = new TokenClient(address, "getstartedclientcredentials", "secret");

            var response = await client.RequestClientCredentialsAsync("ApiScope").ConfigureAwait(false);
            if (response.IsError)
            {
                return String.Empty;
            }

            return response.AccessToken;
        }
    }
}