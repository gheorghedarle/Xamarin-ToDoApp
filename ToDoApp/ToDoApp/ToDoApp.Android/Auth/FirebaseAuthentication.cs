using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Auth;

namespace ToDoApp.Droid.Auth
{
    public class FirebaseAuthentication : IFirebaseAuthentication
    {
        public async Task<string> LoginWithEmailAndPassword(string email, string password)
        {
            try
            {
                var user = await Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var token = user.User.GetIdToken(false);
                return token.Result.ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public async Task<bool> RegisterWithEmailAndPassword(string email, string password)
        {
            try
            {
                var result = await Firebase.Auth.FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
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