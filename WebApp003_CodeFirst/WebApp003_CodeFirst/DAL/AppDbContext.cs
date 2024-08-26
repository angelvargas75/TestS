using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp003_CodeFirst.Models;

namespace WebApp003_CodeFirst.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("conStr")
        {

        }

        public DbSet<Sala> Salas { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
    }
}