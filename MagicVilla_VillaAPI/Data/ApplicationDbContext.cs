using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<VillaDTO> Villas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VillaDTO>().HasData(
            new VillaDTO
            {
                Id = 1,
                Name = "Royal Villa",
                Occupancy = 4,
                Details = "A luxurious villa with stunning views.",
                Rate = "200",
                Sqft = "1500",
                ImageUrl = "https://example.com/royal-villa.jpg",
                Amenity = "Pool, WiFi, Breakfast"
            },
            new VillaDTO
            {
                Id = 2,
                Name = "Beachfront Villa",
                Occupancy = 6,
                Details = "A beautiful villa right on the beach.",
                Rate = "300",
                Sqft = "2000",
                ImageUrl = "https://example.com/beachfront-villa.jpg",
                Amenity = "Pool, WiFi, Breakfast, Spa"
            },
            new VillaDTO
            {
                Id = 3,
                Name = "Mountain Retreat",
                Occupancy = 2,
                Details = "A cozy villa in the mountains.",
                Rate = "150",
                Sqft = "800",
                ImageUrl = "https://example.com/mountain-retreat.jpg",
                Amenity = "WiFi, Breakfast"
            }
        );
    }
}
