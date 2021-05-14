using System;
using System.Threading.Tasks;
using ToDoApp.Auth;
using Firebase.Auth;
using ToDoApp.Models;
using Android.Gms.Extensions;

namespace ToDoApp.Droid.Auth
{
    public class FirebaseAuthentication : IFirebaseAuthentication
    {
        public async Task<UserModel> LoginWithEmailAndPassword(string email, string password)
        {
            try
            {
                var firebaseUser = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var token = await firebaseUser.User.GetIdToken(false).AsAsync<GetTokenResult>();
                var user = new UserModel()
                {
                    DisplayName = firebaseUser.User.DisplayName,
                    Email = firebaseUser.User.Email,
                    Token = token.Token
                };
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> RegisterWithEmailAndPassword(string email, string password)
        {
            try
            {
                var result = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                var userProfileBuilder = new UserProfileChangeRequest.Builder();
                userProfileBuilder.SetDisplayName("Ghita");
                await result.User.UpdateProfileAsync(userProfileBuilder.Build());
                return result.User != null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SignIn()
        {
            var user = Firebase.Auth.FirebaseAuth.Instance.CurrentUser;
            return user != null;
        }

        public bool SignOut()
        {
            try
            {
                Firebase.Auth.FirebaseAuth.Instance.SignOut();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}