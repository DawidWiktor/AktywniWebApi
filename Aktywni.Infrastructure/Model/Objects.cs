using System;
using System.Collections.Generic;

namespace Aktywni.Infrastructure.Model
{
    public partial class Objects
    {
        public Objects()
        {
            Events = new HashSet<Events>();
            ObjectComments = new HashSet<ObjectComments>();
        }

        public int ObjectId { get; set; }
        public int Administrator { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public int? Rating { get; set; }
        public int? NumOfRating { get; set; }
        public string GeographicalCoordinates { get; set; }

        public Users AdministratorNavigation { get; set; }
        public ICollection<Events> Events { get; set; }
        public ICollection<ObjectComments> ObjectComments { get; set; }
    }
}
