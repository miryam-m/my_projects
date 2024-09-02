using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.models
{
    public partial class flyForYouContext : DbContext
    {
        public flyForYouContext()
        {
        }

        public flyForYouContext(DbContextOptions<flyForYouContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BookingDetailsTbl> BookingDetailsTbls { get; set; } = null!;
        public virtual DbSet<BookingTbl> BookingTbls { get; set; } = null!;
        public virtual DbSet<FlightDetailsTbl> FlightDetailsTbls { get; set; } = null!;
        public virtual DbSet<FlightTbl> FlightTbls { get; set; } = null!;
        public virtual DbSet<PassengersTbl> PassengersTbls { get; set; } = null!;
        public virtual DbSet<PaymentTbl> PaymentTbls { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#pragma warning disable CS1030 // #warning directive
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= DESKTOP-OP3HLHL;Database= flyForYou;Trusted_Connection=True;TrustServerCertificate=True");
#pragma warning restore CS1030 // #warning directive
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingDetailsTbl>(entity =>
            {
                entity.HasKey(e => e.BookingDetailsId)
                    .HasName("PK__BookingD__9F00F132511288B4");

                entity.ToTable("BookingDetails_tbl");

                entity.Property(e => e.BookingDetailsId).HasColumnName("bookingDetailsId");

                entity.Property(e => e.BookingId).HasColumnName("bookingId");

                entity.Property(e => e.NumOfSeats).HasColumnName("numOfSeats");

                entity.Property(e => e.SpecialService).HasColumnName("specialService");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.BookingDetailsTbls)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK__BookingDe__booki__30F848ED");
            });

            modelBuilder.Entity<BookingTbl>(entity =>
            {
                entity.HasKey(e => e.BookingId)
                    .HasName("PK__Booking___C6D03BCDD9D002B8");

                entity.ToTable("Booking_tbl");

                entity.Property(e => e.BookingId).HasColumnName("bookingId");

                entity.Property(e => e.BookingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("bookingDate");

                entity.Property(e => e.FlightId).HasColumnName("flightId");

                entity.Property(e => e.PassengerId).HasColumnName("passengerId");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("totalPrice");

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.BookingTbls)
                    .HasForeignKey(d => d.FlightId)
                    .HasConstraintName("FK__Booking_t__fligh__2E1BDC42");

                entity.HasOne(d => d.Passenger)
                    .WithMany(p => p.BookingTbls)
                    .HasForeignKey(d => d.PassengerId)
                    .HasConstraintName("FK__Booking_t__passe__2D27B809");
            });

            modelBuilder.Entity<FlightDetailsTbl>(entity =>
            {
                entity.HasKey(e => e.FlightDetailsId)
                    .HasName("PK__FlightDe__9D69E3FB40EE656C");

                entity.ToTable("FlightDetails_tbl");

                entity.Property(e => e.FlightDetailsId).HasColumnName("flightDetailsId");

                entity.Property(e => e.FlightId).HasColumnName("flightId");

                entity.Property(e => e.FlightTime).HasColumnName("flightTime");

                entity.Property(e => e.NumOfAvailableSeats).HasColumnName("numOfAvailableSeats");

                entity.Property(e => e.NumOfPassengers).HasColumnName("numOfPassengers");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.FlightDetailsTbls)
                    .HasForeignKey(d => d.FlightId)
                    .HasConstraintName("FK__FlightDet__fligh__2A4B4B5E");
            });

            modelBuilder.Entity<FlightTbl>(entity =>
            {
                entity.HasKey(e => e.FlightId)
                    .HasName("PK__Flight_t__0E0186425441C466");

                entity.ToTable("Flight_tbl");

                entity.Property(e => e.FlightId).HasColumnName("flightId");

                entity.Property(e => e.Company)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("company");

                entity.Property(e => e.Destination)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("destination");

                entity.Property(e => e.FlightDate)
                    .HasColumnType("datetime")
                    .HasColumnName("flightDate");

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("source");
            });

            modelBuilder.Entity<PassengersTbl>(entity =>
            {
                entity.HasKey(e => e.PassengersId)
                    .HasName("PK__Passenge__B2373BBACC43151E");

                entity.ToTable("Passengers_tbl");

                entity.HasIndex(e => e.Id, "UQ__Passenge__3213E83EF7D8A4EF")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Passenge__AB6E616402FD7E39")
                    .IsUnique();

                entity.Property(e => e.PassengersId).HasColumnName("passengersId");

                entity.Property(e => e.Adress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("adress");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lastName");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<PaymentTbl>(entity =>
            {
                entity.HasKey(e => e.PaymentId)
                    .HasName("PK__Payment___A0D9EFC6D122240F");

                entity.ToTable("Payment_tbl");

                entity.Property(e => e.PaymentId).HasColumnName("paymentId");

                entity.Property(e => e.CardNumber)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("cardNumber");

                entity.Property(e => e.Cvv)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("cvv");

                entity.Property(e => e.NumOfPayments).HasColumnName("numOfPayments");

                entity.Property(e => e.OwnerId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ownerId");

                entity.Property(e => e.ValidityDate)
                    .HasColumnType("date")
                    .HasColumnName("validityDate");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
