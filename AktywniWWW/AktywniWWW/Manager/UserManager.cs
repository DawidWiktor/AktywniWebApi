using AktywniWWW.DTO;
using AktywniWWW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AktywniWWW.Manager.User
{
    public class UserManager
    {
        public static bool CheckIfExistsUser(string login)
        {
            using (AktywniDBEntities db = new AktywniDBEntities())
            {
                return db.Users.Any(x => x.Login == login);
            }
        }

        public static int GetUserId(string login)
        {
            using (AktywniDBEntities db = new AktywniDBEntities())
            {
                return db.Users.FirstOrDefault(x => x.Login == login).UserID;
            }
        }

        public static Users GetUser(string login)
        {
            using (AktywniDBEntities db = new AktywniDBEntities())
            {
                return db.Users.FirstOrDefault(x => x.Login == login);
            }
        }
        
        public static List<Abonaments> GetAbonaments(int userId)
        {
            using (AktywniDBEntities db = new AktywniDBEntities())
            {
                return db.Abonaments.Where(x => x.UserId == userId).ToList();
            }
        }

        public static ProfileDTO FillToProfileDTO(Users user, List<Abonaments> abonaments)
        {
            ProfileDTO profile = new ProfileDTO();
            profile.Login = user.Login;
            profile.Name = user.Name;
            profile.Surname = user.Surname;
            profile.Role = user.Role;

            profile.ListAbonaments = new List<Abonament>();
            foreach(var item in abonaments)
            {
                profile.ListAbonaments.Add(new Abonament { DateStart = item.DateStart, DateEnd = item.DateEnd,
                                                            Price = item.Price, OrderId = (int)item.OrderId});
            }
            profile.ListAbonaments = profile.ListAbonaments.OrderByDescending(x => x.DateEnd).ToList();
            profile.LastAbonament = profile.ListAbonaments.FirstOrDefault();

            return profile;
        }
    }
}