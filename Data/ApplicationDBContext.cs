using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinShark.Models;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Data
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Stock> Stock {get; set;}
        public DbSet<Comment> Comment {get; set;}
    }
}