using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_e_commerce.Infrastructure.Config
{
    public class RoleConfigration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole { 
                    Id="admin-id",Name="Admin",NormalizedName="ADMIN",ConcurrencyStamp="admin"
                },
                new IdentityRole
                {
                    Id = "customer-id",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER",
                    ConcurrencyStamp= "customer"
                }
               );
        }
    }
}
