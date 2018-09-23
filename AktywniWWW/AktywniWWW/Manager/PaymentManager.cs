using AktywniWWW.Manager.User;
using AktywniWWW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AktywniWWW.Manager
{
    public class PaymentManager
    {
        public static int GetLastIdOrder()
        {
            using (AktywniDBEntities db = new AktywniDBEntities())
            {
                var abonament = db.Abonaments.OrderByDescending(x => x.OrderId).FirstOrDefault();
                if (abonament == null)
                    return 0;
                else
                    return (int)abonament.OrderId;
            }
        }

        public static Abonaments GetLastAbonament(string login)
        {
            using (AktywniDBEntities db = new AktywniDBEntities())
            {
                return db.Abonaments.OrderByDescending(x => x.OrderId).FirstOrDefault(x => x.Users.Login == login);
            }
        }

        public static void CheckIsValidAbonament(string login)
        {
            using (AktywniDBEntities db = new AktywniDBEntities())
            {
                Abonaments abonament = db.Abonaments.OrderByDescending(x => x.OrderId).FirstOrDefault(x => x.Users.Login == login);
                if (abonament == null)
                    return;

                Users user = UserManager.GetUser(login);
                if(abonament.DateEnd < DateTime.Now && user.Role == "biznes")
                {
                    user.Role = "uzytkownik";
                    db.SaveChanges();
                }
            }
        }
    }
}