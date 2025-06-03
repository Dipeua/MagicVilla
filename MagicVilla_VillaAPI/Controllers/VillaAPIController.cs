using MagicVilla_VillaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers;

[ApiController]
[Route("api/VillaAPI")]
public class VillaAPIController : Controller
{
    [HttpGet]
    public IEnumerable<Villa> GetVillas()
    {
        return new List<Villa>
        {
            new Villa { Id = 1, Name = "Villa 1" },
            new Villa { Id = 2, Name = "Villa 2" },
            new Villa { Id = 3, Name = "Villa 3" }
        };
    }
}