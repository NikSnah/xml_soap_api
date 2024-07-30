using Microsoft.EntityFrameworkCore;
using api.Model;

namespace api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<ReservationDB> Reservations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<ReservationDB>().ToTable("to_Bookings");

            modelBuilder.Entity<ReservationDB>(entity =>
            {
                entity.HasKey(e => e.book_id);
                entity.Property(e => e.book_id).HasColumnName("book_id").ValueGeneratedOnAdd(); // book_id is generated on add

                entity.Property(e => e.name).HasColumnName("name");
                entity.Property(e => e.fam_name).HasColumnName("fam_name");
                entity.Property(e => e.address_1).HasColumnName("address_1");
                entity.Property(e => e.address_2).HasColumnName("address_2");
                entity.Property(e => e.CityID).HasColumnName("CityID");
                entity.Property(e => e.pcode).HasColumnName("pcode");
                entity.Property(e => e.StateProvinceID).HasColumnName("StateProvinceID");
                entity.Property(e => e.CountryID).HasColumnName("CountryID");
                entity.Property(e => e.email).HasColumnName("email");
                entity.Property(e => e.cp_code).HasColumnName("cp_code");
                entity.Property(e => e.cell_phone).HasColumnName("cell_phone");
                entity.Property(e => e.DL_number).HasColumnName("DL_number");
                entity.Property(e => e.DL_province).HasColumnName("DL_province");
                entity.Property(e => e.DL_country).HasColumnName("DL_country");
                entity.Property(e => e.DL_dt_expiry).HasColumnName("DL_dt_expiry");
                entity.Property(e => e.birth_year).HasColumnName("birth_year");
                entity.Property(e => e.cdw_type).HasColumnName("cdw_type");
                entity.Property(e => e.cdw_desc).HasColumnName("cdw_desc");
                entity.Property(e => e.cdw_policy).HasColumnName("cdw_policy");
                entity.Property(e => e.cdw_province).HasColumnName("cdw_province");
                entity.Property(e => e.cdw_expiry).HasColumnName("cdw_expiry");
                entity.Property(e => e.cdw_addinfo).HasColumnName("cdw_addinfo");
                entity.Property(e => e.Renter2_Name).HasColumnName("Renter2_Name");
                entity.Property(e => e.Renter2_Relation).HasColumnName("Renter2_Relation");
                entity.Property(e => e.Renter2_DL_number).HasColumnName("Renter2_DL_number");
                entity.Property(e => e.Renter2_DL_dt_expiry).HasColumnName("Renter2_DL_dt_expiry");
                entity.Property(e => e.Renter2_DL_province).HasColumnName("Renter2_DL_province");
                entity.Property(e => e.Renter2_DL_country).HasColumnName("Renter2_DL_country");
                entity.Property(e => e.Renter2_birth_year).HasColumnName("Renter2_birth_year");
                entity.Property(e => e.car_class).HasColumnName("car_class");
                entity.Property(e => e.location_id).HasColumnName("location_id");
                entity.Property(e => e.location_code).HasColumnName("location_code");
                entity.Property(e => e.location_comment).HasColumnName("location_comment");
                entity.Property(e => e.location2_id).HasColumnName("location2_id");
                entity.Property(e => e.location2_code).HasColumnName("location2_code");
                entity.Property(e => e.location2_comment).HasColumnName("location2_comment");
                entity.Property(e => e.bk_comments).HasColumnName("bk_comments");
                entity.Property(e => e.pickup_offsite).HasColumnName("pickup_offsite");
                entity.Property(e => e.dropoff_offsite).HasColumnName("dropoff_offsite");
                entity.Property(e => e.tdt_car_out).HasColumnName("tdt_car_out");
                entity.Property(e => e.tdt_car_in).HasColumnName("tdt_car_in");
                entity.Property(e => e.rental_days).HasColumnName("rental_days");
                entity.Property(e => e.total_hst).HasColumnName("total_hst");
                entity.Property(e => e.total_charge).HasColumnName("total_charge");
                entity.Property(e => e.booksource_id).HasColumnName("booksource_id");
                entity.Property(e => e.bookref).HasColumnName("bookref");
                entity.Property(e => e.paid_amount).HasColumnName("paid_amount");
                entity.Property(e => e.flight_no).HasColumnName("flight_no");
                entity.Property(e => e.dob_date).HasColumnName("dob_date");
                entity.Property(e => e.dob_date_2).HasColumnName("dob_date_2");
                entity.Property(e => e.imgs_verified).HasColumnName("imgs_verified");
                
            });
        }
    }
}

