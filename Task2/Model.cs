using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PagesApp
{
    public class Model
    {
        public class BloggingContext : DbContext
        {
            public DbSet<Page> Pages { get; set; }
            public DbSet<NavLink> NavLinks { get; set; }
            public DbSet<RelatedPage> RelatedPages { get; set; }
            
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                

                modelBuilder.Entity<Page>()
                .Property(p => p.AddedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");


                modelBuilder.Entity<Page>()
                    .Property(p => p.UrlName)
                    .IsRequired();


                modelBuilder.Entity<Page>()
                    .Property(p => p.Description)
                    .HasMaxLength(300);


                modelBuilder.Entity<NavLink>()
                    .HasOne(nl => nl.Page)
                    .WithMany(n => n.NavLinks)
                    .HasForeignKey(nl => nl.ParentLinkId);


                modelBuilder.Entity<RelatedPage>().HasKey(r => new { r.Page1Id, r.Page2Id });
            }
            
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("Filename=./PagesDB.db");
            }
        }

        public class Page
        {
            public int PageId { get; set; }
            public string UrlName { get; set; }
            public string Title { get; set; }
            
            public string Description { get; set; }
            public string Content { get; set; }
            public DateTime AddedDate { get; set; }

            public List<NavLink> NavLinks { get; set; }

            public override string ToString()
            {

                return $"PageID: {PageId}, UrlName: {UrlName}, Title: {Title}, Description: {Description}, Content:{Content}, AddedDate: {AddedDate}";
            }
        }

        public class NavLink
        {
            public int Id { get; set; }
            public string Title { get; set; }

            public Page Page { get; set; }
            public int ParentLinkId { get; set; }

            public RelatedPage RelatedPage { get; set; }
            public int RelatedPageId { get; set; }



            public string Position { get; set; }
            public override string ToString()
            {
                return $"NavLink: {Id}, Title: {Title}, ParentLinkID: {ParentLinkId}, RelatedPageId: {RelatedPageId}, Position: {Position}";
            }

        }

        public class RelatedPage
        {
            public int RPId { get; set; }
            public Page Page1 { get; set; }
            public int Page1Id { get; set; }
            public Page Page2 { get; set; }
            public int Page2Id { get; set; }
            public override string ToString()
            {
                return $"Page1: { Page1Id}, Page2: {Page2Id}";
            }
        }
    }
}
