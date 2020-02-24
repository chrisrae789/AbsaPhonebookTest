using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Persistance.Context
{
    public partial class AbsaPhonebookContext : DbContext
    {
        internal AbsaPhonebookContext()
        {
        }

        public AbsaPhonebookContext(DbContextOptions<AbsaPhonebookContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Phonebook> Phonebook { get; set; }
        public virtual DbSet<PhonebookEntry> PhonebookEntry { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phonebook>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("phonebookid")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PhonebookEntry>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("phonebookentryid")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("phonebookentryid");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phonenumber")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Phonebook)
                    .WithMany(p => p.PhonebookEntries)
                    .HasForeignKey(d => d.PhonebookId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_phonebook_phonebookentry_phonebookid");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
