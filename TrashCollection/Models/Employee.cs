﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrashCollection.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        [Display(Name = "First name:")]

        public string FirstName { get; set; }
        [Display(Name = "Last name:")]

        public string LastName { get; set; }  

        

    }
}
