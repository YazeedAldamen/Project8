//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace universityERP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class studentCours
    {
        public int Id { get; set; }
        public Nullable<int> courseId { get; set; }
        public Nullable<int> studentId { get; set; }
        public Nullable<bool> isPaid { get; set; }
    
        public virtual Cours Cours { get; set; }
        public virtual Student Student { get; set; }
    }
}
