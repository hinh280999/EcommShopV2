using System.Threading.Tasks;

namespace ECommShop.Business.Business.Interface
{
    public interface IAuthRepository
    {
        Task<string> AuthenticateUser(string Username, string password);
    }
}
