using AutoShop.Domain.Entities;
using AutoShop.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Domain
{
    public class AppEFContext :  IdentityDbContext<AppUser, AppRole, long, IdentityUserClaim<long>,
                                            AppUserRole, IdentityUserLogin<long>,
                                            IdentityRoleClaim<long>, IdentityUserToken<long>>
    {
        public AppEFContext(DbContextOptions<AppEFContext> options)
            :base(options)
        {

        }

        public DbSet<Car> Cars { get; set; }
    }
}
