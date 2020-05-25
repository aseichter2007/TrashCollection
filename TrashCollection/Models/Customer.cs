using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrashCollection.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        [ForeignKey("Weekday")]
        public int DayId { get; set; }
        public Weekday Weekday { get; set; }       
        public string FirstName { get; set; }
        public string LastName { get; set; }       
        public string SuspendStart { get; set; }
        public string SuspendEnd { get; set; }
        public string Balance { get; set; }
    }
}
