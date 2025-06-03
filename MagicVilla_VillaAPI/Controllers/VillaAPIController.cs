using MagicVilla_VillaAPI.Data;
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
        return VillaStore.VillaList;
    }

    [HttpGet("{requestId:int}")]
    public ActionResult<VillaDTO> GetVilla(int requestId)
    {
        if (requestId <= 0)
            return BadRequest("Invalid ID");
        var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == requestId);
        if (villa == null) return NotFound();
        return Ok(villa);
    }
}