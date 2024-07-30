public class LocationClass
{
    public string? LocationCode { get; set; }
    public string? LocationName { get; set; }
    public int? Id { get; set; }
    public static readonly List<LocationClass> LocationClasses = new()
    {
        new() { LocationCode = "YYZ", LocationName = "Toronto Airport", Id = 1 },
        new() { LocationCode = "RHL", LocationName = "Richmond Hill", Id = 2 },
        new() { LocationCode = "AOF", LocationName = "2829 Derry Rd East", Id = 3},
        new() { LocationCode = "TOR", LocationName = "Toronto Downtown", Id = 4},
        new() { LocationCode = "MSG", LocationName = "Mississauga City", Id = 5 },
        new() { LocationCode = "MAR", LocationName = "Markham City Rentals", Id = 6},
        new() { LocationCode = "PRS", LocationName = "6305 Northam Dr", Id = 7 },
        new() { LocationCode = "AAD", LocationName = "Airport Area Pickup", Id = 8 },
    };
}