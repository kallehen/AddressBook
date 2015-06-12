using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Models
{
    public class LocationModels
    {
        public int Id { get; set; }
        [Display(Name = "Location name")]
        public string LocationName { get; set; }
        [Display(Name = "Location address")]
        public string LocationPlace { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
