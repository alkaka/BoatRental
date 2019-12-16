using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BoatRental.Models;


namespace BoatRental.Data
{
    public class BoatRentalContext : DbContext
    {
        public BoatRentalContext(DbContextOptions<BoatRentalContext> options) : base(options)
        {

        }
        public DbSet<Boat> Boat { get; set; }
        public DbSet<BoatType> BoatType { get; set; }

    }
}

