using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI.Data;

public static class VillaStore
{
    public static List<VillaDTO> VillaList = new()
    {
        new VillaDTO { Id = 1, Name = "Pool View"},
        new VillaDTO { Id = 2, Name = "Beach View"},
        new VillaDTO { Id = 3, Name = "Mountain View"}
    };
}
