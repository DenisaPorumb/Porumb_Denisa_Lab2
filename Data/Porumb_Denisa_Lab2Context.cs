using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Porumb_Denisa_Lab2.Models;

namespace Porumb_Denisa_Lab2.Data
{
    public class Porumb_Denisa_Lab2Context : DbContext
    {
        public Porumb_Denisa_Lab2Context (DbContextOptions<Porumb_Denisa_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Porumb_Denisa_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Porumb_Denisa_Lab2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<Porumb_Denisa_Lab2.Models.Author> Author { get; set; } = default!;
        public DbSet<Porumb_Denisa_Lab2.Models.Category> Category { get; set; } = default!;
        public DbSet<Porumb_Denisa_Lab2.Models.Member> Member { get; set; } = default!;
        public DbSet<Porumb_Denisa_Lab2.Models.Borrowing> Borrowing { get; set; } = default!;
    }
}
