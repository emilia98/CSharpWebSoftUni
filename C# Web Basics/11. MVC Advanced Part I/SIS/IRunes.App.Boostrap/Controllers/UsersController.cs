using IRunes.Data;
using IRunes.Models;
using IRunes.Services;
using MvcFramework;
using MvcFramework.Attributes.Action;
using MvcFramework.Attributes.Http;
using MvcFramework.Results;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace IRunes.App.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController()
        {
            this.userService = new UserService();
        }

        [NonAction]
        private string HashPassword(string password)
        {
            using (SHA256 sha256hash = SHA256.Create())
            {
                return Encoding.UTF8.GetString(sha256hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }

        public ActionResult Login()
        {
            return this.View();
        }

        // [HttpPostAttribute]
        [HttpPost(ActionName = "Login")]
        public ActionResult LoginPost()
        {
            string username = ((IList<string>)this.Request.FormData["username"]).FirstOrDefault();
            string password = ((IList<string>)this.Request.FormData["password"]).FirstOrDefault();

            User userFromDb = this.userService.GetUserByUsernameAndPassword(username, this.HashPassword(password));

            if (userFromDb == null)
            {
                return this.Redirect("/Users/Login");
            }

            this.SignIn(userFromDb.Id, userFromDb.Username, userFromDb.Email);

            return this.Redirect("/");
        }

        public ActionResult Register()
        {
            return this.View();
        }

        [HttpPost(ActionName = "Register")]
        public ActionResult RegisterPost()
        {
            string username = ((IList<string>)this.Request.FormData["username"]).FirstOrDefault();
            string email = ((IList<string>)this.Request.FormData["email"]).FirstOrDefault();
            string password = ((IList<string>)this.Request.FormData["password"]).FirstOrDefault();
            string confirmPassword = ((IList<string>)this.Request.FormData["confirmPassword"]).FirstOrDefault();

            if (password != confirmPassword)
            {
                return this.Redirect("/Users/Register");
            }

            User user = new User
            {
                Username = username,
                Email = email,
                Password = this.HashPassword(password)
            };

            this.userService.CreateUser(user);

            return this.Redirect("/Users/Login");
        }

        public ActionResult Logout()
        {
            this.SignOut();

            return this.Redirect("/");
        }
    }
}
