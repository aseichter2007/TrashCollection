using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrashCollection.Models
{
    public class Weekday
    {
        [Key]
        public int Id { get; set; }
        public string day { get; set; }
    }
}
