using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Aktywni.Infrastructure.Model
{
    public partial class AktywniDBContext : DbContext
    {
        public virtual DbSet<Disciplines> Disciplines { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<Friends> Friends { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<MessageUser> MessageUser { get; set; }
        public virtual DbSet<ObjectComments> ObjectComments { get; set; }
        public virtual DbSet<Objects> Objects { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=LAPTOP-0DV9EH2D\SQLEXPRESS;Database=AktywniDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Disciplines>(entity =>
            {
                entity.HasKey(e => e.DisciplineId);

                entity.Property(e => e.DisciplineId).HasColumnName("DisciplineID");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("nchar(100)");
            });

            modelBuilder.Entity<Events>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DisciplineId).HasColumnName("DisciplineID");

                entity.Property(e => e.ObjectId).HasColumnName("ObjectID");

                entity.Property(e => e.Visibility).HasColumnType("nchar(10)");

                entity.Property(e => e.WhoCreatedId).HasColumnName("WhoCreatedID");

                entity.HasOne(d => d.AdminNavigation)
                    .WithMany(p => p.EventsAdminNavigation)
                    .HasForeignKey(d => d.Admin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Events_Users1");

                entity.HasOne(d => d.Discipline)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.DisciplineId)
                    .HasConstraintName("FK_Events_Disciplines");

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.ObjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Events_Objects");

                entity.HasOne(d => d.WhoCreated)
                    .WithMany(p => p.EventsWhoCreated)
                    .HasForeignKey(d => d.WhoCreatedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Events_Users");
            });

            modelBuilder.Entity<Friends>(entity =>
            {
                entity.HasKey(e => e.FriendId);

                entity.Property(e => e.FriendId).HasColumnName("FriendID");

                entity.HasOne(d => d.FriendFromNavigation)
                    .WithMany(p => p.FriendsFriendFromNavigation)
                    .HasForeignKey(d => d.FriendFrom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Friends_Users");

                entity.HasOne(d => d.FriendToNavigation)
                    .WithMany(p => p.FriendsFriendToNavigation)
                    .HasForeignKey(d => d.FriendTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Friends_Users1");
            });

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.HasKey(e => e.MessageId);

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.Date).HasColumnType("datetime");
            });

            modelBuilder.Entity<MessageUser>(entity =>
            {
                entity.Property(e => e.MessageUserId).HasColumnName("MessageUserID");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.MessageUser)
                    .HasForeignKey(d => d.MessageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MessageUser_Messages");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MessageUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MessageUser_Users");
            });

            modelBuilder.Entity<ObjectComments>(entity =>
            {
                entity.HasKey(e => e.CommentId);

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.ObjectId).HasColumnName("ObjectID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.ObjectComments)
                    .HasForeignKey(d => d.ObjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObjectComments_Objects");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ObjectComments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObjectComments_Users");
            });

            modelBuilder.Entity<Objects>(entity =>
            {
                entity.HasKey(e => e.ObjectId);

                entity.Property(e => e.ObjectId).HasColumnName("ObjectID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnType("nchar(50)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.NumOfRating).HasDefaultValueSql("((0))");

                entity.Property(e => e.PostCode)
                    .IsRequired()
                    .HasColumnType("nchar(10)");

                entity.Property(e => e.Rating).HasDefaultValueSql("((0))");

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasColumnType("nchar(10)");

                entity.HasOne(d => d.AdministratorNavigation)
                    .WithMany(p => p.Objects)
                    .HasForeignKey(d => d.Administrator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Objects_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.City).HasColumnType("nchar(50)");

                entity.Property(e => e.DateLastActive).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnType("nchar(50)");

                entity.Property(e => e.Name).HasColumnType("nchar(50)");

                entity.Property(e => e.NumOfRating).HasDefaultValueSql("((0))");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("nchar(50)");

                entity.Property(e => e.Rating).HasDefaultValueSql("((0))");

                entity.Property(e => e.Role).HasColumnType("nchar(20)");

                entity.Property(e => e.Surname).HasColumnType("nchar(50)");
            });
        }
    }
}
