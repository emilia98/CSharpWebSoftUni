using HTTP.Requests;
using HTTP.Responses;
using IRunes.Data;
using IRunes.Models;
using MvcFramework;
using MvcFramework.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace IRunes.App.Controllers
{
    public class UsersController : Controller
    {
        private string HashPassword(string password)
        {
            using (SHA256 sha256hash = SHA256.Create())
            {
                return Encoding.UTF8.GetString(sha256hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }

        public IHttpResponse Login(IHttpRequest httpRequest)
        {
            return this.View();
        }

        // [HttpPostAttribute]
        [HttpPost(ActionName = "Login")]
        public IHttpResponse LoginPost(IHttpRequest httpRequest)
        {
            using(var context = new RunesDbContext())
            {
                string username = ((IList<string>)httpRequest.FormData["username"]).FirstOrDefault();
                string password = ((IList<string>)httpRequest.FormData["password"]).FirstOrDefault();

                User userFromDb = context.Users.FirstOrDefault(user =>
                    (user.Username == username || user.Email == username)
                    &&
                    user.Password == this.HashPassword(password));

                if(userFromDb == null)
                {
                    return this.Redirect("/Users/Login");
                }

                this.SignIn(httpRequest, userFromDb.Id, userFromDb.Username, userFromDb.Email);
            }

            return this.Redirect("/");
        }

        public IHttpResponse Register(IHttpRequest httpRequest)
        {
            return this.View();
        }

        [HttpPost(ActionName = "Register")]
        public IHttpResponse RegisterPost(IHttpRequest httpRequest)
        {
            using (var context = new RunesDbContext())
            {
                string username = ((IList<string>)httpRequest.FormData["username"]).FirstOrDefault();
                string email = ((IList<string>)httpRequest.FormData["email"]).FirstOrDefault();
                string password = ((IList<string>)httpRequest.FormData["password"]).FirstOrDefault();
                string confirmPassword = ((IList<string>)httpRequest.FormData["confirmPassword"]).FirstOrDefault();

                if(password != confirmPassword)
                {
                    return this.Redirect("/Users/Register");
                }

                User user = new User()
                {
                    Username = username,
                    Email = email,
                    Password = this.HashPassword(password)
                };

                context.Users.Add(user);
                context.SaveChanges();
            }

            return this.Redirect("/Users/Login");
        }

        public IHttpResponse Logout(IHttpRequest httpRequest)
        {
            this.SignOut(httpRequest);

            return this.Redirect("/");
        }
    }
}
