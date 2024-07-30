namespace api.Model
{
    public class ReservationDB
    {
        public int? book_id { get; set; }
        public string? name { get; set; }
        public string? fam_name  { get; set; }
        public string? address_1 { get; set; }
        public string? address_2 { get; set; }
        public int? CityID { get; set; }
        public string? pcode { get; set; }
        public int? StateProvinceID { get; set; }
        public int? CountryID { get; set; }
        public string? email { get; set; }
        public string? cp_code { get; set; }
        public string? cell_phone { get; set; }
        public string?  DL_number { get; set; }
        public int? DL_province { get; set; }
        public int? DL_country { get; set; }
        public DateTime? DL_dt_expiry { get; set; }
        public int? birth_year { get; set; }
        public string? cdw_type { get; set; }
        public string? cdw_desc { get; set; }
        public string? cdw_policy { get; set; }
        public int? cdw_province { get; set; }
        public DateTime? cdw_expiry { get; set; }
        public string? cdw_addinfo { get; set; }
        public string? Renter2_Name { get; set; }
        public string? Renter2_Relation { get; set; }
        public string? Renter2_DL_number { get; set; }
        public DateTime? Renter2_DL_dt_expiry { get; set; }
        public int? Renter2_DL_province { get; set; }
        public int? Renter2_DL_country { get; set; }
        public int? Renter2_birth_year { get; set; }
        public int? car_class { get; set; }
        public int? location_id { get; set; }
        public string? location_code { get; set; }
        public string? location_comment { get; set; }
        public int? location2_id { get; set; }
        public string? location2_code { get; set; }
        public string? location2_comment { get; set; }
        public string? bk_comments { get; set; }
        public bool? pickup_offsite { get; set; }
        public bool? dropoff_offsite { get; set; }
        public DateTime? tdt_car_out { get; set; }
        public DateTime? tdt_car_in { get; set; }
        public int? rental_days { get; set; }
        public decimal? total_hst { get; set; }
        public decimal? total_charge { get; set; }
        public int? booksource_id { get; set; }
        public string? bookref { get; set; }
        public decimal? paid_amount { get; set; }
        public string? flight_no { get; set; }
        public DateTime? dob_date { get; set; }
        public DateTime? dob_date_2 { get; set; }
        public bool? imgs_verified { get; set; }
        public int? Status { get; set; }
        
    }
}