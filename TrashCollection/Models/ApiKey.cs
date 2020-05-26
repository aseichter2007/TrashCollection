using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrashCollection.Models
{
    public class ApiKey
    {
        [Key]
        public int Id { get; set; }
        public string Key { get; set; }

    }
}
