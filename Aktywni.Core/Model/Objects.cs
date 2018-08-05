using System;
using System.Collections.Generic;

namespace Aktywni.Core.Model
{
    public partial class Objects
    {
        public int ObjectId { get; protected set; }
        public int Administrator { get; protected set; }
        public string Name { get; protected set; }
        public string City { get; protected set; }
        public string Street { get; protected set; }
        public string PostCode { get; protected set; }
        public int? Rating { get; protected set; }
        public int? NumOfRating { get; protected set; }
        public string GeographicalCoordinates { get; protected set; }
        public string Visitability { get; protected set; }
        
        public Users AdministratorNavigation { get; set; }
        public ICollection<Events> Events { get; set; }
        public ICollection<ObjectComments> ObjectComments { get; set; }


        public Objects()
        {
            Events = new HashSet<Events>();
            ObjectComments = new HashSet<ObjectComments>();
        }

        public Objects(int administrator, string name, string city, string street, string postCode, string geographicalCoordinates)
        {
            Administrator = administrator;
            Name = name;
            City = city;
            Street = street;
            PostCode = postCode;
            GeographicalCoordinates = geographicalCoordinates;
        }

        public void SetAdministrator(int administrator)
        {
            Administrator = administrator;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Nazwa obiektu nie może być pusta.");
            else
                Name = name;
        }

        public void SetCity(string city)
        {
            if (string.IsNullOrEmpty(city))
                throw new Exception("Miasto obiektu nie może być puste.");
            else
                City = city;
        }

        public void SetStreet(string street)
        {
            if (string.IsNullOrEmpty(street))
                throw new Exception("Ulica obiektu nie może być puste.");
            else
                Street = street;
        }

        public void SetPostCode(string postCode)
        {
            if (string.IsNullOrEmpty(postCode))
                throw new Exception("Kod pocztowy obiektu nie może być puste.");
            else
                PostCode = postCode;
        }

        public void SetRating(int rate)
        {
            if (rate < 0 || rate > 10)
                throw new Exception("Ocena nie może być mniejsza od 0 i większa od 10.");
            else
                Rating = rate;
            NumOfRating++;
        }

        public void SetNumOfRating(int amount)
        {
            if (amount < 0)
                throw new Exception("Liczba ocen nie może być niższa od 0");
            else
                NumOfRating = amount;
        }
        public void SetGeographicalCoordinates(string geographicalCoordinates)
        {
            if (string.IsNullOrEmpty(geographicalCoordinates))
                throw new Exception("Współrzędne obiektu nie może być puste.");
            else
                GeographicalCoordinates = geographicalCoordinates;
        }

    }
}
