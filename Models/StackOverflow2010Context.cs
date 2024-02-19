using System;
using System.Collections.Generic;
using Elfie.Serialization;
using System.Drawing;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CahootSOOA.Models;

public partial class StackOverflow2010Context : DbContext
{
    public StackOverflow2010Context()
    {
    }

    public StackOverflow2010Context(DbContextOptions<StackOverflow2010Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Badge> Badges { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<LinkType> LinkTypes { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostLink> PostLinks { get; set; }

    public virtual DbSet<PostType> PostTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vote> Votes { get; set; }

    public virtual DbSet<VoteType> VoteTypes { get; set; }

    public virtual DbSet<VwPostsSummary> VwPostsSummaries { get; set; }

    public virtual DbSet<VwPostsSummaryabc> VwPostsSummaryabcs { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source = Nisarg; Initial Catalog = StackOverflow2010; Integrated Security = True; Trust Server Certificate = True;", options => options.CommandTimeout(180)); // Sets the timeout to 180 seconds

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Badge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Badges__Id");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(40);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Comments__Id");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Text).HasMaxLength(700);
        });

        modelBuilder.Entity<LinkType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_LinkTypes__Id");

            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Posts__Id");

            entity.Property(e => e.ClosedDate).HasColumnType("datetime");
            entity.Property(e => e.CommunityOwnedDate).HasColumnType("datetime");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.LastActivityDate).HasColumnType("datetime");
            entity.Property(e => e.LastEditDate).HasColumnType("datetime");
            entity.Property(e => e.LastEditorDisplayName).HasMaxLength(40);
            entity.Property(e => e.Tags).HasMaxLength(150);
            entity.Property(e => e.Title).HasMaxLength(250);
        });

        modelBuilder.Entity<PostLink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PostLinks__Id");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<PostType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PostTypes__Id");

            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Users_Id");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DisplayName).HasMaxLength(40);
            entity.Property(e => e.EmailHash).HasMaxLength(40);
            entity.Property(e => e.LastAccessDate).HasColumnType("datetime");
            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.WebsiteUrl).HasMaxLength(200);
        });

        modelBuilder.Entity<Vote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Votes__Id");

            entity.HasIndex(e => e.PostId, "<Name of Missing Index, sysname,>");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<VoteType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_VoteType__Id");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwPostsSummary>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_PostsSummary");

            entity.Property(e => e.Bage)
                .HasMaxLength(40)
                .HasColumnName("bage");
            entity.Property(e => e.PostId).HasColumnName("postId");
            entity.Property(e => e.Title).HasMaxLength(250);
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.UserName)
                .HasMaxLength(40)
                .HasColumnName("userName");
        });

        modelBuilder.Entity<VwPostsSummaryabc>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_PostsSummaryabc");

            entity.Property(e => e.Answers).HasColumnName("answers");
            entity.Property(e => e.Bage)
                .HasMaxLength(40)
                .HasColumnName("bage");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.PostId).HasColumnName("postId");
            entity.Property(e => e.Reputation).HasColumnName("reputation");
            entity.Property(e => e.Title).HasMaxLength(250);
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserName)
                .HasMaxLength(40)
                .HasColumnName("User_Name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
