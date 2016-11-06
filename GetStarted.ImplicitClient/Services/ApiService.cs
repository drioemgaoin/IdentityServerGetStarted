using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GetStarted.ImplicitClient.Models;
using Newtonsoft.Json;

namespace GetStarted.ImplicitClient.Services
{
    public interface IApiService
    {
        Task<User> GetUser(string accessToken);
    }

    public class ApiService: IApiService
    {
        public async Task<User> GetUser(string accessToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await client.GetAsync("http://localhost:65014/users/withAuth").ConfigureAwait(false);
                var body = await GetResultAsync(response).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<User>(body);
            }
        }

        private static async Task<string> GetResultAsync(HttpResponseMessage httpResponseMessage)
        {
            return await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }
}