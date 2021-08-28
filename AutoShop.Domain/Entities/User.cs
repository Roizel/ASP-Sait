using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Domain.Entities
{
    [Table("tbluser")]
    public class User
    {
        [Key]
        public int id { get; set; }

        [Required, StringLength(100)]
        public string username { get; set; }
        [Required, StringLength(100)]
        public string userpassword { get; set; }
        [Required, StringLength(100)]
        public string useremail { get; set; }
    }
}
