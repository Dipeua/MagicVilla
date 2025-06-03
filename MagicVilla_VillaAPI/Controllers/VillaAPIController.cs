using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers;

[ApiController]
// [Route("api/[controller]")]
[Route("api/VillaAPI")]
public class VillaAPIController : Controller
{
    [HttpGet]
    public IEnumerable<VillaDTO> GetVillas()
    {
        return new List<VillaDTO>
        {
            new VillaDTO { Id = 1, Name = "Villa 1" },
            new VillaDTO { Id = 2, Name = "Villa 2" },
            new VillaDTO { Id = 3, Name = "Villa 3" }
        };
    }
}