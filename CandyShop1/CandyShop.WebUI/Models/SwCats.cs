//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CandyShop.WebUI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SwCats
    {
        public int Id { get; set; }
        public int IdS { get; set; }
        public int IdC { get; set; }
    
        public virtual Categories Categories { get; set; }
        public virtual Sweets Sweets { get; set; }
    }
}
