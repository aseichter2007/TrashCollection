using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrashCollection.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Address line one:")]

        public string AddressLineOne { get; set; }
        [Display(Name = "Address line two:")]

        public string AddressLineTwo { get; set; }
        [Display(Name = "City:")]

        public string City { get; set; }
        [Display(Name = "State:")]

        public string State { get; set; }
        [Display(Name = "Zip Code:")]

        public string ZipCode { get; set; }
        public string Coordinate { get; set; }
    }
}
