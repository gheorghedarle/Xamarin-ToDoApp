using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Auth
{
    public interface IFirebaseAuthentication
    {
        Task<UserModel> LoginWithEmailAndPassword(string email, string password);
        Task<bool> RegisterWithEmailAndPassword(string email, string password);
        bool SignOut();
        bool SignIn();
    }
}
