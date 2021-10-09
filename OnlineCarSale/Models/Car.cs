//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineCarSale.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Car
    {
        public int CId { get; set; }

        [Required(ErrorMessage = "Please enter Company"), MaxLength(50)]
        public string Company { get; set; }

        [Required(ErrorMessage = "Please enter Model"), MaxLength(50)]
        public string Model { get; set; }

        [Required(ErrorMessage = "Please enter Year")]
        public Nullable<int> Year { get; set; }

        [Required(ErrorMessage = "Please enter Price")]
        public Nullable<decimal> Price { get; set; }

        [Required(ErrorMessage = "Please enter Location"), MaxLength(50)]
        public string Location { get; set; }

        [Required(ErrorMessage = "Please enter body type"), MaxLength(50)]
        public string BodyType { get; set; }
        public int SId { get; set; }
    
        public virtual Seller Seller { get; set; }
    }
}