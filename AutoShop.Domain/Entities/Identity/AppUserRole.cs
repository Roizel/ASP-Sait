using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Domain.Entities.Identity
{
    public class AppUserRole : IdentityUserRole<long>
    {
        public virtual AppRole Role { get; set; }
        public virtual AppUser User { get; set; }
    }
}
