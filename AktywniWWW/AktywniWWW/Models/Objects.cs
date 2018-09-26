//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AktywniWWW.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Objects
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Objects()
        {
            this.Events = new HashSet<Events>();
            this.ObjectComments = new HashSet<ObjectComments>();
        }
    
        public int ObjectID { get; set; }
        public int Administrator { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public Nullable<int> Rating { get; set; }
        public Nullable<int> NumOfRating { get; set; }
        public string GeographicalCoordinates { get; set; }
        public string Visitability { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Events> Events { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ObjectComments> ObjectComments { get; set; }
        public virtual Users Users { get; set; }
    }
}