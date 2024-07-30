using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace api.SOAP.Model
{
    [XmlRoot(ElementName = "OTA_VehResNotifRQ", Namespace = "http://www.opentravel.org/OTA/2003/05", IsNullable = false)]
    public class OTA_VehResNotifRQ
    {
        public OTA_VehResNotifRQ()
        {
            POS = new POS();
            Reservations = new Reservations();
            TimeStamp = DateTime.Now;
            Target = "";
            Version = "";
        }

        [XmlElement(ElementName ="POS")]
        public POS POS { get; set; }
        
        [XmlElement(ElementName = "Reservations")]
        public Reservations Reservations { get; set; }

        [XmlAttribute(AttributeName = "TimeStamp")]
        public DateTime TimeStamp { get; set; }

        [XmlAttribute(AttributeName = "Target")]
        public string Target { get; set; }

        [XmlAttribute(AttributeName = "Version")]
        public string Version { get; set; }
    }
    public class POS
    {
        public POS()
        {
            Source = new POSSource();
        }
        [XmlElement(ElementName ="Source")]
        public POSSource Source{ get; set; }
    }
    public class POSSource
    {

        public POSSource()
        {
            RequestorID = new RequestorID();
        }

        [XmlElement(ElementName ="RequestorID")]
        public RequestorID RequestorID { get; set; }
    }

    public class RequestorID
    {
        public RequestorID()
        {
            Type = 0;
            ID = "";
        }
        [XmlAttribute(AttributeName = "Type")]
        public int Type { get; set; }
        
        [XmlAttribute(AttributeName = "ID")]
        public string ID { get; set; }
    }
    public class Reservations
    {
        public Reservations()
        {
            Reservation = new Reservation();
        }
        [XmlElement(ElementName = "Reservation")]
        public Reservation Reservation { get; set; }
    }
    public class Reservation
    {
        public Reservation()
        {
            ProcessingInfo = new ProcessingInfo();
            BookingSource = new BookingSource();
            VehReservation = new VehReservation();
        }
        [XmlElement(ElementName = "ProcessingInfo")]
        public ProcessingInfo ProcessingInfo { get; set; }
        
        [XmlElement(ElementName = "BookingSource")]
        public BookingSource BookingSource { get; set; }
        
        [XmlElement(ElementName = "VehReservation")]
        public VehReservation VehReservation { get; set; }
    }

    public class ProcessingInfo
    {
        public ProcessingInfo()
        {
            Action = "";
        }
        [XmlAttribute(AttributeName = "Action")]
        public string Action { get; set; }
    }
    public class BookingSource
    {
        public BookingSource()
        {
            SourceDetail = new Source();
        }
        [XmlElement(ElementName = "Source")]
        public Source SourceDetail { get; set; }
    }

    public class Source
    {
        public Source()
        {
            AgentSine = "";
            PseudoCityCode = "";
            RequestorID = new RequestorID();
            BookingChannel = new BookingChannel();
            Address = new Address();
        }
        [XmlAttribute(AttributeName = "AgentSine")]
        public string AgentSine { get; set; }
        
        [XmlAttribute(AttributeName = "PseudoCityCode")]
        public string PseudoCityCode { get; set; }
        
        [XmlElement(ElementName = "RequestorID")]
        public RequestorID RequestorID { get; set; }
        
        [XmlElement(ElementName = "BookingChannel")]
        public BookingChannel BookingChannel { get; set; }

        [XmlElement(ElementName = "Address")]
        public Address Address  { get; set; }
    }

    public class BookingChannel
    {
        public BookingChannel()
        {
            CompanyName = "";
        }

        [XmlElement(ElementName = "CompanyName")]
        public string CompanyName { get; set; }
        
    }

    public class Address
    {
        public Address()
        {
            StreetNmbr = "";
            CityName = "";
            PostalCode = "";
            StateProv = "";
        }
        [XmlElement(ElementName = "StreetNmbr")]
        public string StreetNmbr { get; set; }
        
        [XmlElement(ElementName = "CityName")]
        public string CityName { get; set; }
        
        [XmlElement(ElementName = "PostalCode")]
        public string PostalCode { get; set; }
        
        [XmlElement(ElementName = "StateProv")]
        public string StateProv { get; set; }
    }

    public class VehReservation
    {
        public VehReservation()
        {
            CreateDateTime = DateTime.Now;
            LastModifyDateTime = DateTime.Now;
            Customer = new Customer();
            VehSegmentCore = new VehSegmentCore();
        }
        [XmlAttribute(AttributeName = "CreateDateTime")]
        public DateTime CreateDateTime { get; set; }
        
        [XmlAttribute(AttributeName = "LastModifyDateTime")]
        public DateTime LastModifyDateTime { get; set; }
        
        [XmlElement(ElementName = "Customer")]
        public Customer Customer { get; set; }

        [XmlElement(ElementName = "VehSegmentCore")]
        public VehSegmentCore VehSegmentCore { get; set; }
    }

    public class Customer
    {
        public Customer()
        {
            Primary = new Primary();
        }
        [XmlElement(ElementName = "Primary")]
        public Primary Primary { get; set; }
    }

    public class Primary
    {
        public Primary()
        {
            PersonName = new PersonName();
            Telephone = new Telephone();
            Email = "";
        }
        [XmlElement(ElementName = "PersonName")]
        public PersonName PersonName { get; set; }

        [XmlElement(ElementName = "Telephone")]
        public Telephone Telephone { get; set; }

        [XmlElement(ElementName = "Email")]
        public string Email { get; set; }
    }
    public class PersonName
    {
        public PersonName()
        {
            GivenName = "";
            Surname = "";
        }
        [XmlElement(ElementName = "GivenName")]
        public string GivenName { get; set; }
        
        [XmlElement(ElementName = "Surname")]
        public string Surname { get; set; }
    }

    public class Telephone
    {
        public Telephone()
        {
            PhoneNumber = "";
        }
        [XmlAttribute(AttributeName = "PhoneNumber")]
        public string PhoneNumber { get; set; }
    }

    public class VehSegmentCore
    {
        public VehSegmentCore()
        {
            ConfID1 = new ConfID();
            Vendor = new Vendor();
            VehRentalCore = new VehRentalCore();
            Vehicle = new Vehicle();
            RentalRate = new RentalRate();
            Fees = new Fees();
            TotalCharge = new TotalCharge();
        }
        [XmlElement(ElementName = "ConfID")]
        public ConfID ConfID1 { get; set; }

        [XmlElement(ElementName = "Vendor")]

        public Vendor Vendor { get; set; }

        [XmlElement(ElementName = "VehRentalCore")]

        public VehRentalCore VehRentalCore { get; set; }

        [XmlElement(ElementName = "Vehicle")]

        public Vehicle Vehicle { get; set; }

        [XmlElement(ElementName = "RentalRate")]

        public RentalRate RentalRate { get; set; }

        [XmlElement(ElementName = "Fees")]

        public Fees Fees { get; set; }

        [XmlElement(ElementName = "TotalCharge")]

        public TotalCharge TotalCharge { get; set; }

    }

    public class ConfID
    {
        public ConfID()
        {
            ID = "";
            Type = "";
        }
        [XmlAttribute(AttributeName ="ID")]
        public string ID { get; set; }
        
        [XmlAttribute(AttributeName = "Type")]
        public string Type { get; set; }
    }

    public class Vendor
    {
        public Vendor()
        {
            CompanyShortName = "";
            TravelSector = "";
            Code = "";
            Name = "";
        }
        [XmlAttribute(AttributeName = "CompanyShortName")]
        public string CompanyShortName { get; set; }

        [XmlAttribute(AttributeName = "TravelSector")]
        public string TravelSector { get; set; }

        [XmlAttribute(AttributeName = "Code")]
        public string Code { get; set; }

        [XmlText]
        public string Name { get; set; }

    }

    public class VehRentalCore 
    {
        public VehRentalCore()
        {
            PickUpDateTime = DateTime.Now;
            ReturnDateTime = DateTime.Now;
            PickUpLocation = new Location();
            ReturnLocation = new Location();
        }
        [XmlAttribute(AttributeName ="PickUpDateTime")]
        public DateTime PickUpDateTime { get; set; }
        
        [XmlAttribute(AttributeName = "ReturnDateTime")]
        public DateTime ReturnDateTime { get; set; }

        [XmlElement(ElementName = "PickUpLocation")]

        public Location PickUpLocation { get; set; }

        [XmlElement(ElementName = "ReturnLocation")]

        public Location ReturnLocation { get; set; }

    }

    public class Location
    {
        public Location()
        {
            LocationCode = "";
            CodeContext = "";
        }
        [XmlAttribute(AttributeName = "LocationCode")]
        public string LocationCode { get; set; }

        [XmlAttribute(AttributeName = "CodeContext")]
        public string CodeContext { get; set; }

    }

    public class Vehicle 
    {
        public Vehicle()
        {
            Code = "";
            CodeContext = "";
        }
        [XmlAttribute(AttributeName = "Code")]
        public string Code { get; set; }

        [XmlAttribute(AttributeName = "CodeContext")]

        public string CodeContext { get; set; }
    }

    public class RentalRate
    {
        public RentalRate()
        {
            RateDistance = new RateDistance();
            VehicleCharges = new VehicleCharges();
            RateQualifier = new RateQualifier();
        }
        [XmlElement(ElementName = "RateDistance")]
        public RateDistance RateDistance { get; set; }

        [XmlElement(ElementName = "VehicleCharges")]
        public VehicleCharges VehicleCharges { get; set; }

        [XmlElement(ElementName = "RateQualifier")]
        public RateQualifier RateQualifier { get; set; }

    }

    public class RateDistance
    {
        public RateDistance()
        {
            Unlimited = "";
            DistUnitName = "";
            VehiclePeriodUnitName = "";
        }
        [XmlAttribute(AttributeName = "Unlimited")]
        public string Unlimited { get; set; }

        [XmlAttribute(AttributeName = "DistUnitName")]
        public string DistUnitName { get; set; }

        [XmlAttribute(AttributeName = "VehiclePeriodUnitName")]
        public string VehiclePeriodUnitName { get; set; }
    }

    public class VehicleCharges
    {
        public VehicleCharges()
        {
            VehicleChargesList = new List<VehicleCharge>();
        }
        [XmlElement(ElementName = "VehicleCharge")]
        public List<VehicleCharge> VehicleChargesList { get; set; }

    }

    public class VehicleCharge
    {
        public VehicleCharge()
        {
            CurrencyCode = "";
            Amount = "";
            Description = "";
            IncludedInEstTotalInd = "";
            CalculationList = new List<Calculation>();
        }
        [XmlAttribute(AttributeName = "CurrencyCode")]
        public string CurrencyCode { get; set; }

        [XmlAttribute(AttributeName = "Amount")]
        public string Amount { get; set; }

        [XmlAttribute(AttributeName = "Description")]
        public string Description { get; set; }

        [XmlAttribute(AttributeName ="IncludedInEstTotalInd")]
        public string IncludedInEstTotalInd { get; set; }

        [XmlElement(ElementName = "Calculation")]
        public List<Calculation> CalculationList { get; set; }

    }

    public class Calculation
    {
        public Calculation()
        {
            UnitCharge = "";
            UnitName = "";
            Quantity = "";
            Total = "";
        }
        [XmlAttribute(AttributeName = "UnitCharge")]
        public string UnitCharge { get; set; }

        [XmlAttribute(AttributeName = "UnitName")]
        public string UnitName { get; set; }

        [XmlAttribute(AttributeName = "Quantity")]
        public string Quantity { get; set; }

        [XmlAttribute(AttributeName = "Total")]
        public string Total { get; set; }

    }

    public class RateQualifier
    {
        public RateQualifier()
        {
            RateCatagory = "";
            PromotionCode = "";
            RateQualifierCode = "";
            RatePeriod = "";
        }
        [XmlAttribute(AttributeName = "RateCatagory")]
        public string RateCatagory { get; set; }

        [XmlAttribute(AttributeName = "PromotionCode")]
        public string PromotionCode { get; set; }

        [XmlAttribute(AttributeName = "RateQualifier")]
        public string RateQualifierCode { get; set; }

        [XmlAttribute(AttributeName = "RatePeriod")]
        public string RatePeriod { get; set; }


    }
    public class Fees
    {
        public Fees()
        {
            FeeList = new List<Fee>();
        }
        [XmlElement(ElementName = "Fee")]
        
        public List<Fee> FeeList { get; set; }
    }

    public class Fee 
    {
        public Fee()
        {
            CurrencyCode = "";
            Amount = "";
            Description = "";
            IncludedInEstTotalInd = "";
            Purpose = "";
            Calculation = new Calculation();
        }
        [XmlAttribute(AttributeName = "CurrencyCode")]
        public string CurrencyCode { get; set; }

        [XmlAttribute(AttributeName = "Amount")]
        public string Amount { get; set; }

        [XmlAttribute(AttributeName = "Description")]
        public string Description { get; set; }

        [XmlAttribute(AttributeName = "IncludedInEstTotalInd")]
        public string IncludedInEstTotalInd { get; set; }

        [XmlAttribute(AttributeName = "Purpose")]
        public string Purpose { get; set; }

        [XmlElement(ElementName = "Calculation")]

        public Calculation Calculation { get; set; }


    }

    public class TotalCharge
    {   
        public TotalCharge()
        {
            RateTotalAmount = "";
            EstimatedTotalAmount = "";
            CurrencyCode = "";
        }
        [XmlAttribute(AttributeName = "RateTotalAmount")]
        public string RateTotalAmount { get; set; }

        [XmlAttribute(AttributeName = "EstimatedTotalAmount")]
        public string EstimatedTotalAmount { get; set; }

        [XmlAttribute(AttributeName = "CurrencyCode")]
        public string CurrencyCode { get; set; }

    }
}