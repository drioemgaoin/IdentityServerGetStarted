using System.Threading.Tasks;
using GetStarted.ClientCredentials.Models;

namespace GetStarted.ClientCredentials.Services
{
    public interface IApiService
    {
        Task<UserModel> GetUser();
    }
}