public class CarClass
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? Id { get; set; }
    public static readonly List<CarClass> CarClasses = new()
    {
        new() { Name = "CCAR", Description = "Compact", Id = 7 },
        new() { Name = "MVAR", Description = "Minivan", Id = 9 },
        new() { Name = "ECAR", Description = "Economy", Id = 10},
        new() { Name = "SCAR", Description = "Standard", Id = 11},
        new() { Name = "FCAR", Description = "Full Size", Id = 12 },
        new() { Name = "EFAR", Description = "SUV", Id = 13},
        new() { Name = "CKAR", Description = "Cargo Van", Id = 14 },
        new() { Name = "IFAR", Description = "SUV Midsize", Id = 15 },
        new() { Name = "FFAR", Description = "SUV Fullsize 8 Seater", Id = 16 },
        new() { Name = "SFAR", Description = "SUV Standard SFAR", Id = 17 }
    };
}