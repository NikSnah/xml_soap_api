using System.Windows.Markup;
using api.SOAP.Model;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using System.Security.Cryptography.X509Certificates;

using static api.SOAP.Model.SOAP1_1RequestEnvelope;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using System.Xml;
using System.Data;
using api.Data;

using api.Model;

using System.IO;
using Microsoft.EntityFrameworkCore;

namespace api.SOAP.Controllers;


[SOAPControllerAttributes(SOAPVersion.v1_1)]

public class OTA_Res : SOAPControllerBase
{    
    

    private DataTable OTAReservations = new();
    private readonly ApplicationDbContext _context;

    private readonly ILogger<OTA_Res> _logger;

    public OTA_Res(ILogger<OTA_Res> logger, IWebHostEnvironment env, ApplicationDbContext context) : base(logger, env)
    {
        //start the logger
        _logger = logger;
        _context = context;

        // Create the DataTable
        OTAReservations.Columns.Add("bookingReference", typeof(string));
        OTAReservations.Columns.Add("firstName", typeof(string));
        OTAReservations.Columns.Add("lastName", typeof(string));
        OTAReservations.Columns.Add("phone", typeof(string));
        OTAReservations.Columns.Add("email", typeof(string));
        OTAReservations.Columns.Add("pickUpLocation", typeof(string));
        OTAReservations.Columns.Add("dropOffLocation", typeof(string));
        OTAReservations.Columns.Add("pickUpDate", typeof(DateTime));
        OTAReservations.Columns.Add("dropOffDate", typeof(DateTime));
        OTAReservations.Columns.Add("vehicle", typeof(string));
        OTAReservations.Columns.Add("totalCharge", typeof(decimal));
        OTAReservations.Columns.Add("rentalDays", typeof(int));
        // OTAReservations.Columns.Add("CDW", typeof(string)); TBD, ACE dev's are in process of developing
    }

    [HttpPost]
    [Consumes("text/xml")]
    public async Task<IActionResult> Post()
    {
        try {
            // Check if the request content type is text/xml if anything else, return 
            if (Request.ContentType != "text/xml" || Request.ContentType == null || Request.ContentType == ""  )
            {
                // Return a SOAP fault response
                return BadRequest(CreateSOAPResponseEnvelope("Client Error", "Invalid content type. Expected text/xml.", "0"));
            }

            // Initialize the request xml structure to deserialize
            OTA_VehResNotifRQ? otaRequest = null;
            using (StreamReader reader = new(Request.Body))
            { 
                // Turn into a XML document 
                var body = await reader.ReadToEndAsync();
                XmlDocument xmlDocument = new();
                xmlDocument.LoadXml(body);


                // get into the body of the SOAP request
                var soapBody = xmlDocument.GetElementsByTagName("SOAP-ENV:Body")[0];

                // Check if it is null 
                if (soapBody is null)
                {
                    return BadRequest(CreateSOAPResponseEnvelope("Error", "Invalid SOAP Structure", "0"));
                }

                // Select th inner body part of the XML within the SOAP request
                var soapBodyString = soapBody?.InnerXml;

                if (soapBodyString is null || soapBodyString == "")
                {
                    return BadRequest(CreateSOAPResponseEnvelope("Error", "Invalid SOAP Structure", "0"));
                }    

                // Serialize the inner body xml into the structure of the OTA_VehResNotifRQ structure
                XmlSerializer serializer = new(typeof(OTA_VehResNotifRQ), "http://www.opentravel.org/OTA/2003/05");

                using StringReader stringReader = new(soapBodyString);
                otaRequest = serializer.Deserialize(stringReader) as OTA_VehResNotifRQ;
            }

            // Make sure the request of the deserialized is not empty and actual inner body was serialized
            if (otaRequest is null)
            {
                return BadRequest(CreateSOAPResponseEnvelope("Error", "Invalid OTA_VehResNotifRQ Structure", "0"));
            } 
            else 
            {
                // The action of the request which could be of the following 
                var action = otaRequest.Reservations?.Reservation?.ProcessingInfo?.Action;
                var bookRef = otaRequest.Reservations?.Reservation?.VehReservation?.VehSegmentCore?.ConfID1?.ID;

                if (action is null || action == "")
                {
                    return BadRequest(CreateSOAPResponseEnvelope("Error", "No Action Specified", bookRef is null? "0" : bookRef));
                } 
                else if (action == "Add")
                {
                    // Proceed with "Add" request to create a new booking using the booking ref number

                    // SQL Query....
                    return await AddReservation(otaRequest, bookRef is null? "0" : bookRef);

                } 
                else if (action == "AddModify")
                {
                    // Proceed with "AddModify" request for the booking using the booking ref number 

                    // SQL Query....
                    return await AddModifyReservation(otaRequest, bookRef is null? "0" : bookRef);
                }
                else if (action == "Cancel")
                {
                    // Proceed with "Cancel" request for the booking using the booking ref number 

                    // SQL Query....
                    return await CancelReservation(bookRef is null? "0" : bookRef);
                }
                else // Means Action is unknown
                {
                    return BadRequest(CreateSOAPResponseEnvelope("Error", "Not supported action", bookRef is null? "0" : bookRef));
                }

                // If we did not incounter any errors return success response
                return Ok(CreateSOAPResponseEnvelope("Success", "Reservation Recieved Successfully", bookRef is null? "0" : bookRef));
            }

        } catch (Exception e)
        {
            return BadRequest(CreateSOAPResponseEnvelope("Error", e.Message , "0"));

        }
    }

    private async Task<IActionResult> AddReservation(OTA_VehResNotifRQ otaRequest, string bookRef)
    {
        try 
        {
            // Check if a reservation with the same booking reference number already exists
            var existingReservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.bookref == bookRef);

            if (existingReservation != null)
            {
                return BadRequest(CreateSOAPResponseEnvelope("Error", "Reservation with the same booking reference already exists.", bookRef));
            }

            // Create a new row in the DataTable
            DataRow row = OTAReservations.NewRow();

            row["bookingReference"] = bookRef;
            var firstName = row["firstName"] = otaRequest.Reservations?.Reservation?.VehReservation?.Customer?.Primary?.PersonName?.GivenName;
            var  lastName = row["lastName"] = otaRequest.Reservations?.Reservation?.VehReservation?.Customer?.Primary?.PersonName?.Surname;
            var Phone = row["phone"] = otaRequest.Reservations?.Reservation?.VehReservation?.Customer?.Primary?.Telephone?.PhoneNumber;
            var email_found = row["email"]  = otaRequest.Reservations?.Reservation?.VehReservation?.Customer?.Primary?.Email;
            var pickUpLocation = row["pickUpLocation"]  = otaRequest.Reservations?.Reservation?.VehReservation?.VehSegmentCore?.VehRentalCore?.PickUpLocation?.LocationCode;
            var dropOffLocation = row["dropOffLocation"] = otaRequest.Reservations?.Reservation?.VehReservation?.VehSegmentCore?.VehRentalCore?.ReturnLocation?.LocationCode;
            DateTime pickUpDate = otaRequest.Reservations?.Reservation?.VehReservation?.VehSegmentCore?.VehRentalCore?.PickUpDateTime as DateTime? ?? DateTime.Now;
            row["pickUpDate"] = pickUpDate;
            var dropOffDate = otaRequest.Reservations?.Reservation?.VehReservation?.VehSegmentCore?.VehRentalCore?.ReturnDateTime as DateTime? ?? DateTime.Now;
            row["dropOffDate"] = dropOffDate;
            var vehicle = row["vehicle"] = otaRequest.Reservations?.Reservation?.VehReservation?.VehSegmentCore?.Vehicle?.Code;

            // Calculating the total charge
            var totalChargeString = otaRequest.Reservations?.Reservation?.VehReservation?.VehSegmentCore?.TotalCharge?.EstimatedTotalAmount;
            decimal totalCharge = decimal.Parse(totalChargeString ?? "0");
            row["totalCharge"] = totalCharge;

            //Calculating the rental days
            TimeSpan difference = dropOffDate - pickUpDate;
            double differenceTotalDays = difference.TotalDays;
            row["rentalDays"] =  (int)Math.Ceiling(differenceTotalDays);

            // Add the row to the DataTable
            OTAReservations.Rows.Add(row);
            var rowIndex = OTAReservations.Rows.IndexOf(row);


            var reservation = new ReservationDB
            {
                // Variables from the DataTable
                bookref = bookRef is null? "" : bookRef,
                name = firstName == null ? "" : (string) firstName,
                fam_name = lastName == null ? "" : (string) lastName,
                cell_phone = Phone == null ? "" : (string) Phone,
                email = email_found == null ? "" : (string) email_found,
                car_class =  CarClass.CarClasses.Find(x => x.Name ==  vehicle?.ToString())?.Id ?? 7,
                location_id = LocationClass.LocationClasses.Find(x => x.LocationCode == pickUpLocation?.ToString())?.Id ?? 1,
                location_code =  LocationClass.LocationClasses.Find(x => x.LocationCode == pickUpLocation?.ToString())?.LocationCode ?? "YYZ",
                location_comment = LocationClass.LocationClasses.Find(x => x.LocationCode == pickUpLocation?.ToString())?.LocationName ?? "Toronto Airport",
                location2_id = LocationClass.LocationClasses.Find(x => x.LocationCode == pickUpLocation?.ToString())?.Id ?? 1,
                location2_code = LocationClass.LocationClasses.Find(x => x.LocationCode == pickUpLocation?.ToString())?.LocationCode ?? "YYZ",
                location2_comment = LocationClass.LocationClasses.Find(x => x.LocationCode == pickUpLocation?.ToString())?.LocationName ?? "Toronto Airport",
                tdt_car_out = pickUpDate,
                tdt_car_in = dropOffDate,
                rental_days = (int) row["rentalDays"],
                total_charge = totalCharge,

                // Default variables 
                cp_code = "1",
                CityID = 49, // Default Toronto
                StateProvinceID = 1, // Default Ontario
                CountryID = 39, // Default Canada
                DL_province = 47,
                DL_country = 39,
                cdw_province = 47,
                Renter2_DL_province = 47,
                Renter2_DL_country = 39,
                pickup_offsite = false,
                dropoff_offsite = false,
                total_hst = 0,
                imgs_verified = false,
                paid_amount = 0,
                booksource_id = 23, // Default Ace
                Status = 0, // Default Active

                //Empty Variables 
                address_1 = null,
                address_2 = null,
                pcode = null,
                DL_number = null,
                DL_dt_expiry = null,
                birth_year = null,
                cdw_type = null,
                cdw_desc = null,
                cdw_policy = null,
                cdw_expiry = null,
                cdw_addinfo = null,
                Renter2_Name = null,
                Renter2_Relation = null,
                Renter2_DL_number = null,
                Renter2_DL_dt_expiry = null,
                Renter2_birth_year = null,
                bk_comments = null,
                dob_date = null,
                dob_date_2 = null,  
                flight_no = null,    
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        } 
        catch (Exception e)
        {
            return BadRequest(CreateSOAPResponseEnvelope("Error", e.Message , "0"));
        }

        return Ok(CreateSOAPResponseEnvelope("Success", "Reservation Recieved Successfully", bookRef is null? "0" : bookRef));
    }

    private async Task<IActionResult> AddModifyReservation(OTA_VehResNotifRQ otaRequest, string bookRef)
    {
        try
        {

            var reservation = await _context.Reservations
                    .FirstOrDefaultAsync(r => r.bookref == bookRef);

            if (reservation is null)
            {
                return BadRequest(CreateSOAPResponseEnvelope("Error", "Reservation not found", bookRef is null? "0" : bookRef));
            }

            var firstName = otaRequest.Reservations?.Reservation?.VehReservation?.Customer?.Primary?.PersonName?.GivenName;
            var  lastName = otaRequest.Reservations?.Reservation?.VehReservation?.Customer?.Primary?.PersonName?.Surname;
            var Phone = otaRequest.Reservations?.Reservation?.VehReservation?.Customer?.Primary?.Telephone?.PhoneNumber;
            var email_found = otaRequest.Reservations?.Reservation?.VehReservation?.Customer?.Primary?.Email;
            var pickUpLocation = otaRequest.Reservations?.Reservation?.VehReservation?.VehSegmentCore?.VehRentalCore?.PickUpLocation?.LocationCode;
            var dropOffLocation  = otaRequest.Reservations?.Reservation?.VehReservation?.VehSegmentCore?.VehRentalCore?.ReturnLocation?.LocationCode;
            DateTime pickUpDate = otaRequest.Reservations?.Reservation?.VehReservation?.VehSegmentCore?.VehRentalCore?.PickUpDateTime as DateTime? ?? DateTime.Now;
            var dropOffDate = otaRequest.Reservations?.Reservation?.VehReservation?.VehSegmentCore?.VehRentalCore?.ReturnDateTime as DateTime? ?? DateTime.Now;
            var vehicle = otaRequest.Reservations?.Reservation?.VehReservation?.VehSegmentCore?.Vehicle?.Code;

            // Calculating the total charge
            var totalChargeString = otaRequest.Reservations?.Reservation?.VehReservation?.VehSegmentCore?.TotalCharge?.EstimatedTotalAmount;
            decimal totalCharge = decimal.Parse(totalChargeString ?? "0");

            //Calculating the rental days
            TimeSpan difference = dropOffDate - pickUpDate;
            double differenceTotalDays = difference.TotalDays;
            int totalRentalDays = (int)Math.Ceiling(differenceTotalDays);

            // Variables from the DataTable
            reservation.name = firstName == null ? "" : (string) firstName;
            reservation.fam_name = lastName == null ? "" : (string) lastName;
            reservation.cell_phone = Phone == null ? "" : (string) Phone;
            reservation.email = email_found == null ? "" : (string) email_found;
            reservation.car_class =  CarClass.CarClasses.Find(x => x.Name ==  vehicle?.ToString())?.Id ?? 7;
            reservation.location_id = LocationClass.LocationClasses.Find(x => x.LocationCode == pickUpLocation?.ToString())?.Id ?? 1;
            reservation.location_code =  LocationClass.LocationClasses.Find(x => x.LocationCode == pickUpLocation?.ToString())?.LocationCode ?? "YYZ";
            reservation.location_comment = LocationClass.LocationClasses.Find(x => x.LocationCode == pickUpLocation?.ToString())?.LocationName ?? "Toronto Airport";
            reservation.location2_id = LocationClass.LocationClasses.Find(x => x.LocationCode == pickUpLocation?.ToString())?.Id ?? 1;
            reservation.location2_code = LocationClass.LocationClasses.Find(x => x.LocationCode == pickUpLocation?.ToString())?.LocationCode ?? "YYZ";
            reservation.location2_comment = LocationClass.LocationClasses.Find(x => x.LocationCode == pickUpLocation?.ToString())?.LocationName ?? "Toronto Airport";
            reservation.tdt_car_out = pickUpDate;
            reservation.tdt_car_in = dropOffDate;
            reservation.rental_days = totalRentalDays;
            reservation.total_charge = totalCharge;
            reservation.Status = 0; // Default Active

            // Everything else stays the same 

        
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return BadRequest(CreateSOAPResponseEnvelope("Error", e.Message , "0"));
        }
        return Ok(CreateSOAPResponseEnvelope("Success", "Reservation updated successfully", bookRef));
    }

    private async Task<IActionResult> CancelReservation(string bookRef)
    {
        try 
        {
            var reservation = await _context.Reservations
            .FirstOrDefaultAsync(r => r.bookref == bookRef);

             if (reservation is null)
            {
                return BadRequest(CreateSOAPResponseEnvelope("Error", "Reservation not found", bookRef is null? "0" : bookRef));
            }

            reservation.Status = -1; // Cancelled

            await _context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return BadRequest(CreateSOAPResponseEnvelope("Error", e.Message , "0"));
        }
        return Ok(CreateSOAPResponseEnvelope("Success", "Reservation cancelled successfully", bookRef));

    }

        
}
