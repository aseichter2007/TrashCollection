using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrashCollection.Models
{
    public class Employee
    {
        [ForeignKey("User")]
        public int Id { get; set; }
        public User User { get; set; }
        [ForeignKey]
    }
}
