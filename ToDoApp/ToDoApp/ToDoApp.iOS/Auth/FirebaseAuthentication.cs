using Foundation;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ToDoApp.Auth;
using ToDoApp.Models;

namespace ToDoApp.iOS.Auth
{
    public class FirebaseAuthentication : IFirebaseAuthentication
    {
        public async Task<UserModel> LoginWithEmailAndPassword(string email, string password)
        {
            try
            {
                var firebaseUser = await Firebase.Auth.Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
                var token = await firebaseUser.User.GetIdTokenAsync();
                var user = new UserModel()
                {
                    DisplayName = firebaseUser.User.DisplayName,
                    Email = firebaseUser.User.Email,
                    Token = token
                };
                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<bool> RegisterWithEmailAndPassword(string username, string email, string password)
        {
            try
            {
                var result = await Firebase.Auth.Auth.DefaultInstance.CreateUserAsync(email, password);
                if(result.User != null)
                {
                    var changeRequest = result.User.ProfileChangeRequest();
                    changeRequest.DisplayName = username;
                    await changeRequest.CommitChangesAsync();
                }
                return result.User != null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public Task<bool> ForgetPassword(string email)
        {
            throw new NotImplementedException();
        }

        public string GetUsername()
        {
            var user = Firebase.Auth.Auth.DefaultInstance.CurrentUser;
            return user?.DisplayName;
        }

        public string GetUserId()
        {
            var user = Firebase.Auth.Auth.DefaultInstance.CurrentUser;
            return user?.Uid;
        }


        public bool IsLoggedIn()
        {
            var user = Firebase.Auth.Auth.DefaultInstance.CurrentUser;
            return user != null;
        }

        public bool LogOut()
        {
            try
            {
                NSError error;
                return Firebase.Auth.Auth.DefaultInstance.SignOut(out error);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
    }
}