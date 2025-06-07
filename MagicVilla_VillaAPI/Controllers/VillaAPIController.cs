using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers;

[ApiController]
// [Route("api/[controller]")]
[Route("api/VillaAPI")]
public class VillaAPIController : Controller
{
    private readonly ApplicationDbContext _db;
    public VillaAPIController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<VillaDTO>> GetVillas()
    {
        return Ok(_db.Villas.ToList());
    }

    [HttpGet("{requestId:int}", Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VillaDTO> GetVilla(int requestId)
    {
        if (requestId <= 0)
        {
            return BadRequest();
        }
        var villa = _db.Villas.FirstOrDefault(v => v.Id == requestId);
        if (villa == null) return NotFound();
        return Ok(villa);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO requestVilla)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(requestVilla);
        }
        
        if(_db.Villas.FirstOrDefault(v => v.Name.ToLower() == requestVilla.Name.ToLower()) != null)
        {
            ModelState.AddModelError("CustomError", "Villa already exists!");
            return BadRequest(ModelState);
        }

        if (requestVilla == null)
        {
            return BadRequest(requestVilla);
        }

        if (requestVilla.Id > 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        requestVilla.Id = _db.Villas.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
        _db.Villas.Add(requestVilla);
        _db.SaveChanges();
        return CreatedAtRoute("GetVilla", new { requestId = requestVilla.Id }, requestVilla);
    }

    [HttpDelete("{requestId:int}", Name ="DeleteVilla")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteVilla(int requestId)
    {
        if (requestId <= 0)
        {
            return BadRequest("Invalid ID");
        }
        var villa = _db.Villas.FirstOrDefault(v => v.Id == requestId);
        if (villa == null)
        {
            return NotFound();
        }
        _db.Villas.Remove(villa);
        _db.SaveChanges();
        return NoContent();
    }

    [HttpPut("{requestId:int}", Name = "Update villa")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateVilla(int requestId, [FromBody] VillaDTO requestVilla)
    {
        if (requestId <= 0 || requestVilla == null || (requestId != requestVilla.Id))
        {
            return BadRequest("Invalid ID");
        }
        var villa = _db.Villas.FirstOrDefault(v => v.Id == requestId);
        if (villa == null)
        {
            return NotFound();
        }

        villa.Id = requestVilla.Id;
        villa.Name = requestVilla.Name;
        villa.Occupancy = requestVilla.Occupancy;
        villa.Sqft = requestVilla.Sqft;
        villa.Rate = requestVilla.Rate;
        villa.Details = requestVilla.Details;
        villa.ImageUrl = requestVilla.ImageUrl;
        villa.Amenity = requestVilla.Amenity;
        _db.Villas.Update(villa);
        _db.SaveChanges();
        // Return NoContent to indicate successful update without returning the updated object
        return NoContent();
    }

    [HttpPatch("{requestId:int}", Name = "UpdatePartialvilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdatePartialvilla(int requestId, JsonPatchDocument<VillaDTO> patchDTO)
    {
        if(requestId == 0 || patchDTO == null)
        {
            return BadRequest();
        }
        var villa = _db.Villas.FirstOrDefault(v => v.Id == requestId);
        if (villa == null) return NotFound();

        patchDTO.ApplyTo(villa, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return NoContent();
    }
}