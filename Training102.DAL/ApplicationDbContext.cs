using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Training102.DAL
{
    public class ApplicationDbContext : IdentityDbContext<User,IdentityRole<int>,int>
    {

        public ApplicationDbContext(DbContextOptions contextOption) :base (contextOption)
        {
            
        }

     
    }
}
