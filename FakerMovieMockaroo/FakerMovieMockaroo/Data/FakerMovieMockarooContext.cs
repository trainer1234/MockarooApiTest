using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FakerMovieMockaroo.Models
{
    public class FakerMovieMockarooContext : DbContext
    {
        public FakerMovieMockarooContext (DbContextOptions<FakerMovieMockarooContext> options)
            : base(options)
        {
        }

        public DbSet<FakerMovieMockaroo.Models.Movie> Movie { get; set; }
    }
}
