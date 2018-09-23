using AktywniWWW.Commands.Home;
using AktywniWWW.Manager.User;
using AktywniWWW.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace AktywniWWW.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(SignInCommand command)
        {
            var user = UserManager.GetUser(command.Login);
            if (user == null || !CryptoHelper.Crypto.VerifyHashedPassword(user.Password,command.Password))
            {
                ModelState.AddModelError("", "Błędny login lub hasło");
                return View("Index");
            }

            FormsAuthentication.SetAuthCookie(command.Login, false);
            return RedirectToAction("Index", "Profile");
        }

        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}