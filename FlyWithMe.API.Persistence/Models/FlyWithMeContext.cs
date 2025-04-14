using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FlyWithMe.API.Persistence.Models;

public partial class FlyWithMeContext : DbContext
{
    public FlyWithMeContext()
    {
    }

    public FlyWithMeContext(DbContextOptions<FlyWithMeContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        this.Database.SetCommandTimeout(240);
    }

    public virtual DbSet<Userchatdetail> Userchatdetails { get; set; }

    public virtual DbSet<Usermaster> Usermasters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Userchatdetail>(entity =>
        {
            entity.HasKey(e => e.Chatid).HasName("userchatdetails_pkey");

            entity.ToTable("userchatdetails");

            entity.Property(e => e.Chatid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("chatid");
            entity.Property(e => e.Createddate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Userchatrequest)
                .HasColumnType("character varying")
                .HasColumnName("userchatrequest");
            entity.Property(e => e.Userchatresponse)
                .HasColumnType("character varying")
                .HasColumnName("userchatresponse");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Userchatdetails)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("userchatdetails_userid_fkey");
        });

        modelBuilder.Entity<Usermaster>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("usermaster_pkey");

            entity.ToTable("usermaster");

            entity.Property(e => e.Userid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("userid");
            entity.Property(e => e.Createdon)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdon");
            entity.Property(e => e.Emailid)
                .HasMaxLength(200)
                .HasColumnName("emailid");
            entity.Property(e => e.Firstname)
                .HasMaxLength(200)
                .HasColumnName("firstname");
            entity.Property(e => e.Fullname)
                .HasMaxLength(200)
                .HasColumnName("fullname");
            entity.Property(e => e.Lastlogindate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastlogindate");
            entity.Property(e => e.Lastname)
                .HasMaxLength(200)
                .HasColumnName("lastname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
