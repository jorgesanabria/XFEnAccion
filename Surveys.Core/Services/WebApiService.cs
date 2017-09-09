using Surveys.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Surveys.Entities;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using Surveys.Core.Models;

namespace Surveys.Core.Services
{
    public class WebApiService : IWebApiService
    {
        private readonly HttpClient client;
        public WebApiService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(Literals.WebApiServiceBaseAddress)
            };
        }
        public async Task<IEnumerable<Team>> GetTeamsAsync()
        {
            IEnumerable<Team> result = null;
            try
            {
                var teams = await client.GetStringAsync("/api/teams");
                if (!string.IsNullOrWhiteSpace(teams))
                {
                    result = JsonConvert.DeserializeObject<IEnumerable<Team>>(teams);
                    return result;
                }
                return result;
            }
            catch (WebException e)
            {
                throw e;
            }
        }

        public async Task<bool> LoginAsync(string userName, string password)
        {
            var encodedUserName = WebUtility.UrlEncode(userName);
            var encodePassword = WebUtility.UrlEncode(password);
            var content = new StringContent($"grant_type=password&username={encodedUserName}&password={encodePassword}", Encoding.UTF8, "application/x-www-form-urlencoded");
            var uri = new Uri($"{Literals.WebApiServiceBaseAddress}token");
            using (var response = client.PostAsync(uri.ToString(), content).Result)
            {
                var value = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var token = JsonConvert.DeserializeObject<TokenResponseModel>(value);
                    var tokenString = token.AccessToken;
                    if (!client.DefaultRequestHeaders.Contains("Authorization"))
                    {
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer" + tokenString);
                        return true;
                    }
                }
                return false;
            }
        }

        public async Task<bool> SaveSurveysAsync(IEnumerable<Survey> surveys)
        {
            var content = new StringContent(JsonConvert.SerializeObject(surveys), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/surveys", content);
            return response.IsSuccessStatusCode;
        }
    }
}
