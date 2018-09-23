using AktywniWWW.DTO;
using AktywniWWW.Manager.User;
using AktywniWWW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AktywniWWW.Controllers
{

    public class ProfileController : Controller
    {
        public ActionResult Index()
        {
            Users user = UserManager.GetUser(User.Identity.Name);
            List<Abonaments> listAbonaments = UserManager.GetAbonaments(user.UserID);
            ProfileDTO profile = UserManager.FillToProfileDTO(user, listAbonaments);
            ViewBag.IsActive = profile.LastAbonament.DateEnd > DateTime.Now;
            return View(profile);
        }
    }
}