using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PagesApp;

namespace Task2.Migrations
{
    [DbContext(typeof(Model.BloggingContext))]
    [Migration("20160805135116_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("PagesApp.Model+NavLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ParentLinkId");

                    b.Property<string>("Position");

                    b.Property<int>("RelatedPageId");

                    b.Property<int?>("RelatedPagePage1Id");

                    b.Property<int?>("RelatedPagePage2Id");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("ParentLinkId");

                    b.HasIndex("RelatedPagePage1Id", "RelatedPagePage2Id");

                    b.ToTable("NavLinks");
                });

            modelBuilder.Entity("PagesApp.Model+Page", b =>
                {
                    b.Property<int>("PageId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Content");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 300);

                    b.Property<string>("Title");

                    b.Property<string>("UrlName")
                        .IsRequired();

                    b.HasKey("PageId");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("PagesApp.Model+RelatedPage", b =>
                {
                    b.Property<int>("Page1Id");

                    b.Property<int>("Page2Id");

                    b.Property<int>("RPId");

                    b.HasKey("Page1Id", "Page2Id");

                    b.HasIndex("Page1Id");

                    b.HasIndex("Page2Id");

                    b.ToTable("RelatedPages");
                });

            modelBuilder.Entity("PagesApp.Model+NavLink", b =>
                {
                    b.HasOne("PagesApp.Model+Page", "Page")
                        .WithMany("NavLinks")
                        .HasForeignKey("ParentLinkId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PagesApp.Model+RelatedPage", "RelatedPage")
                        .WithMany()
                        .HasForeignKey("RelatedPagePage1Id", "RelatedPagePage2Id");
                });

            modelBuilder.Entity("PagesApp.Model+RelatedPage", b =>
                {
                    b.HasOne("PagesApp.Model+Page", "Page1")
                        .WithMany()
                        .HasForeignKey("Page1Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PagesApp.Model+Page", "Page2")
                        .WithMany()
                        .HasForeignKey("Page2Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
