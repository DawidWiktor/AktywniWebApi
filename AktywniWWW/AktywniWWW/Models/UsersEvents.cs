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
    
    public partial class UsersEvents
    {
        public int UserEventID { get; set; }
        public int EventID { get; set; }
        public int UserID { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
    
        public virtual Events Events { get; set; }
        public virtual Users Users { get; set; }
    }
}