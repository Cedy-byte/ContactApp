//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SqlDatabase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Contact
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Username { get; set; }
    
        public virtual User User { get; set; }
    }
}
