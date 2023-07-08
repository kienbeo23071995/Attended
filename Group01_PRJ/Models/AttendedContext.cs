using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Group01_PRJ.Models
{
    public partial class AttendedContext : DbContext
    {
        public AttendedContext()
        {
        }

        public AttendedContext(DbContextOptions<AttendedContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attended> Attendeds { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Slot> Slots { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserClass> UserClasses { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;database=prn_project;Integrated security=true;TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Attended>(entity =>
            {
                entity.HasKey(e => new { e.Roomid, e.Slotid, e.Date, e.Userid })
                    .HasName("PK__attended__1DB5DB1EF5C0E9BB");

                entity.ToTable("attended");

                entity.Property(e => e.Roomid).HasColumnName("roomid");

                entity.Property(e => e.Slotid).HasColumnName("slotid");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Attendeds)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_attended");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.Attendeds)
                    .HasForeignKey(d => new { d.Roomid, d.Slotid, d.Date })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("session_attended");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("class");

                entity.HasIndex(e => e.Id, "class_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(2000)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("course");

                entity.HasIndex(e => e.Id, "course_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(2000)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("group");

                entity.HasIndex(e => e.Id, "group_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(2000)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("room");

                entity.HasIndex(e => e.Id, "room_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(2000)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasKey(e => new { e.Roomid, e.Slotid, e.Date })
                    .HasName("PK__session__38D9610559C79631");

                entity.ToTable("session");

                entity.Property(e => e.Roomid).HasColumnName("roomid");

                entity.Property(e => e.Slotid).HasColumnName("slotid");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Classid).HasColumnName("classid");

                entity.Property(e => e.Courseid).HasColumnName("courseid");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.Classid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("class_session");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.Courseid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("couse_session");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.Roomid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("room_session");

                entity.HasOne(d => d.Slot)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.Slotid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("slot_session");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_session_teacher");
            });

            modelBuilder.Entity<Slot>(entity =>
            {
                entity.ToTable("slot");

                entity.HasIndex(e => e.Id, "slot_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(2000)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Email, "UQ__user__AB6E6164D426EC89")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "UQ__user__F3DBC5721DB0DC46")
                    .IsUnique();

                entity.HasIndex(e => e.Id, "user_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("fullname");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.IsSuper).HasColumnName("is_super");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<UserClass>(entity =>
            {
                entity.HasKey(e => new { e.Classid, e.Userid, e.Courseid })
                    .HasName("PK__user_cla__D1E48868D28A0AB2");

                entity.ToTable("user_class");

                entity.Property(e => e.Classid).HasColumnName("classid");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Courseid).HasColumnName("courseid");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.UserClasses)
                    .HasForeignKey(d => d.Classid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_class_class");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.UserClasses)
                    .HasForeignKey(d => d.Courseid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_class_course");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserClasses)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_user_room");
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.HasKey(e => new { e.Groupid, e.Userid })
                    .HasName("PK__user_gro__E47E14A028FD2CD4");

                entity.ToTable("user_group");

                entity.Property(e => e.Groupid).HasColumnName("groupid");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.Groupid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("group_user_group");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_user_group");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
