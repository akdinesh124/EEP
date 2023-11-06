using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EEP.Models;

namespace EEP.Data
{
    public class EEPContext : DbContext
    {
        public EEPContext()
        {
        }

        public EEPContext(DbContextOptions<EEPContext> options)
            : base(options)
        {
        }
       

        public DbSet<EEP.Models.Employee> Employee { get; set; } = default!;
    }
}
