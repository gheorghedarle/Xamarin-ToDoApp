using System.Threading.Tasks;

namespace ToDoApp.Auth
{
    public interface IFirebaseAuthentication
    {
        Task<string> LoginWithEmailAndPassword(string email, string password);
        Task<bool> RegisterWithEmailAndPassword(string email, string password);
        bool SignOut();
        bool SignIn();
    }
}
