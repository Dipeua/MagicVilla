﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.Dto;

public class VillaDTO
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(30)]
    public string? Name { get; set; }
    public int Occupancy { get; set; }
    public string Details { get; set; }
    [Required]
    public string Rate { get; set; }
    public string Sqft { get; set; }
    public string ImageUrl { get; set; }
    public string Amenity { get; set; }
}
