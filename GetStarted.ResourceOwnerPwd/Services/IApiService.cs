using System.Threading.Tasks;
using GetStarted.ResourceOwnerPwd.Models;

namespace GetStarted.ResourceOwnerPwd.Services
{
    public interface IApiService
    {
        Task<UserModel> GetUser();
    }
}