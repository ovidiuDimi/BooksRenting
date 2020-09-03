using System;
using System.Collections.Generic;
using System.Text;
using BooksRenting.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BooksRenting.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        internal readonly object NewAuthors;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Renting> Rentings { get; set; }
        public object Title { get; internal set; }
    }
}
